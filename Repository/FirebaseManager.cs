using Firebase.Auth;
using Firebase.Database;
using Firebase.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util;
using html_exctractor.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace html_exctractor.Respository
{
    public class FirebaseManager
    {

        private static string RULES_CHILD = "rules";
        private static string HISTORY_CHILD = "history";
        public static string API_KEY = "AIzaSyAttkcUTtH9ze44HVNbX5kObkBKfQoPh8Y";

        private FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(API_KEY));
        private FirebaseClient client = new FirebaseClient("https://html-extractor-default-rtdb.asia-southeast1.firebasedatabase.app/");
        private FirebaseStorage storageClient = new FirebaseStorage("html-extractor.appspot.com");

        public async Task<string> Login(string email, string password)
        {
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            return auth.User.Email;
        }
        public async Task<string> Registration(string email, string password)
        {
            var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            return auth.User.Email;
        }

        public async Task<string> Google()
        {
            var initializer = new GoogleAuthorizationCodeFlow.Initializer
            {
                Scopes = new[] { "email", "profile" },
                ClientSecrets = new ClientSecrets { ClientId = "111234912313-l9aqmsrga183h3dnr90c4dad3ffotmlr.apps.googleusercontent.com", ClientSecret = "OOBmRORPQ1MgEr4Y3MSKPTeA" },
            };
            var flow = new GoogleAuthorizationCodeFlow(initializer);

            var result = await new AuthorizationCodeInstalledApp(flow, new LocalServerCodeReceiver()).AuthorizeAsync("user", CancellationToken.None);

            if (result.Token.IsExpired(SystemClock.Default))
            {
                await result.RefreshTokenAsync(CancellationToken.None);
            }
            var auth = await authProvider.SignInWithOAuthAsync(FirebaseAuthType.Google, result.Token.AccessToken);

            return auth.User.Email;
        }

        public Task ForgotPassword(string email)
        {
            return authProvider.SendPasswordResetEmailAsync(email);
        }

        public async Task<List<Rule>> GetRules()
        {
            var rules = await client
                .Child(RULES_CHILD)
                .OnceAsync<Rule>();
            return grabItemsFromResponse(rules);
        }

        public async Task<string> CreateRule(Rule rule)
        {
            var result = await client
                .Child(RULES_CHILD)
                .PostAsync(JsonConvert.SerializeObject(rule));
            return result.Object;
        }

        public Task UpdateRule(Rule rule)
        {
            return client
                .Child(buildChildPath(RULES_CHILD, rule.Key))
                .PutAsync(JsonConvert.SerializeObject(rule));
        }

        public Task DeleteRule(Rule rule)
        {
            return client
                .Child(buildChildPath(RULES_CHILD, rule.Key))
                .DeleteAsync();
        }

        public async void UploadFile(string path, string ruleName)
        {
            var stream = File.Open(path, FileMode.Open);
            var url = await storageClient.Child(Path.GetFileName(path)).PutAsync(stream);

            var history = new History(ruleName, DateTime.Now.ToShortDateString(), url, path);
            await client
                .Child(HISTORY_CHILD)
                .PostAsync(JsonConvert.SerializeObject(history));
        }

        public async Task<List<History>> GetTasksHistory()
        {
            var result = await client.Child(HISTORY_CHILD)
                .OnceAsync<History>();
            return grabItemsFromResponse(result);
        }

        public async Task<bool> ClearHistory()
        {
            var histories = await GetTasksHistory();
            if (histories.Count == 0) return false;

            foreach (History h in histories)
            {
                await storageClient
                    .Child(h.DocumentName)
                    .DeleteAsync();
            }
            await client.Child(HISTORY_CHILD)
                .DeleteAsync();
            return true;
        }

        private string buildChildPath(params string[] queries)
        {
            var fullPath = "";
            for (int i = 0; i < queries.Length; i++)
            {
                fullPath += queries[i] + "/";
            }
            return fullPath;
        }

        private List<R> grabItemsFromResponse<R>(IReadOnlyCollection<FirebaseObject<R>> source) where R : FirebaseKeyObject
        {
            var mapedRules = new List<R>();
            foreach (FirebaseObject<R> r in source)
            {
                r.Object.Key = r.Key;
                mapedRules.Add(r.Object);
            }
            return mapedRules;
        }

    }
}
