using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using html_exctractor.Common;
using html_exctractor.Model;
using html_exctractor.Respository;
using Notification.Wpf;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace html_exctractor.Service
{
    class RuleJobWorker : BackgroundWorker
    {

        public Rule Rule { private get; set; }
        private JobObserver observer;
        public JobObserver Observer
        {
            get => observer;
            set
            {
                observer = value;
                if (value == null) return;

                if (IsBusy)
                {
                    observer.OnRuleJobRunning(Rule?.Key);
                    return;
                }
                observer.OnRuleJobCompleted(Rule?.Key);
            }
        }
        private HtmlParser parser = new HtmlParser();
        private HttpClient client = new HttpClient();
        private NotificationManager notificationManager = App.NotificationManager;
        private FirebaseManager firebaseManager;

        public RuleJobWorker(FirebaseManager firebaseManager)
        {
            this.firebaseManager = firebaseManager;
            DoWork += RuleJobWorker_DoWork;
            RunWorkerCompleted += RuleJobWorker_RunWorkerCompleted;
        }

        private async void RuleJobWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            notificationManager.Show(null, Utils.GetStringResource("rule_worker_state_start"), NotificationType.Notification);

            Rule parametrRule = e.Argument as Rule;
            Observer?.OnRuleJobRunning(parametrRule.Key);

            IWorkbook workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet(Utils.GetStringResource("page_word"));

            var initialPage = await loadHtml(parametrRule, "1");

            var pages = 0;
            var paginationClassResult = initialPage.GetElementsByClassName(parametrRule.RulePagesCountClass)[0];
            foreach (INode element in paginationClassResult.ChildNodes)
            {
                try
                {
                    pages = int.Parse(element.TextContent);
                }
                catch { }
            }

            int rowCounter = 0;
            int prevCounter = 1;
            bool isNewField = false;
            var titlesRow = sheet.CreateRow(rowCounter);
            for (int h = 0; h < parametrRule.Fields.Count; h++)
            {
                Field field = parametrRule.Fields[h];
                titlesRow.CreateCell(h).SetCellValue(field.Value);
            }
            int prevPage = 0;
            for (int i = 0; i < pages + 1; i++)
            {
                var nextPage = await loadHtml(parametrRule, i.ToString());
                for (int h = 0; h < parametrRule.Fields.Count; h++)
                {
                    Field field = parametrRule.Fields[h];

                    var elements = nextPage.GetElementsByClassName(field.Value);

                    for (int j = 0; j < elements.Length; j++)
                    {
                        if (isNewField)
                        {
                            int temp = prevCounter;
                            prevCounter = rowCounter;
                            rowCounter = temp;
                            isNewField = false;
                        }
                        else
                        {
                            rowCounter++;
                        }
                        var row = sheet.GetRow(rowCounter) ?? sheet.CreateRow(rowCounter);
                        row.CreateCell(h)
                            .SetCellValue(elements[j].TextContent);
                    }
                    isNewField = true;
                }
                notificationManager.Show(parametrRule.Name, i + 1 + " " + Utils.GetStringResource("rule_worker_state_page_ready"), NotificationType.Information);
            }

            saveDocument(workbook, parametrRule, e);
        }

        private void RuleJobWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Error != null)
            {
                notificationManager.Show(Rule.Name, e.Error.Message, NotificationType.Error);
                Observer?.OnRuleJobError(e.Error.Message);
                return;
            }
            if (e.Cancelled)
            {
                Observer?.OnRuleJobError(Utils.GetStringResource("rule_worker_canceled_error"));
                return;
            }

            Observer?.OnRuleJobCompleted(Rule.Key);

            Rule = null;
        }

        private async Task<IHtmlDocument> loadHtml(Rule parametrRule, string page)
        {
            var finalUrl = parametrRule.GeneralUrl.Replace("{page}", page);
            var responseMessage = await client.GetAsync(finalUrl);
            var pageHtml = await responseMessage.Content.ReadAsStringAsync();
            return await parser.ParseDocumentAsync(pageHtml);
        }

        private void saveDocument(IWorkbook workbook, Rule parametrRule, DoWorkEventArgs e)
        {
            try
            {
                var destinationFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var filePath = Path.Combine(destinationFolder, parametrRule.GetFinalDocumentName());
                FileStream sw = File.Create(filePath);
                workbook.Write(sw);
                sw.Close();

                firebaseManager.UploadFile(filePath, parametrRule.Name);

                e.Result = workbook;
                notificationManager.Show(parametrRule.Name, Utils.GetStringResource("rule_worker_state_success"), NotificationType.Success, onClick: () =>
                {
                    Utils.TryOpenFile(filePath);
                });

            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }
    }

    public class RuleJobWorkerWrapper
    {

        private RuleJobWorker ruleJobWorker;

        public JobObserver JobObserver
        {
            private get => ruleJobWorker.Observer;
            set => ruleJobWorker.Observer = value;
        }

        public RuleJobWorkerWrapper(FirebaseManager firebaseManager)
        {
            ruleJobWorker = new RuleJobWorker(firebaseManager);
        }

        public void Execute(Rule rule)
        {
            ruleJobWorker.Rule = rule;
            ruleJobWorker.RunWorkerAsync(rule);
        }

        public void Cancel()
        {
            ruleJobWorker.CancelAsync();
            JobObserver = null;
        }

    }

    public interface JobObserver
    {
        void OnRuleJobRunning(string key);
        void OnRuleJobCompleted(string key);
        void OnRuleJobError(string error);
    }

    public class DownloadWorker : BackgroundWorker
    {

        private NotificationManager notificationManager = App.NotificationManager;
        public DownloadWorker()
        {
            DoWork += DownloadWorker_DoWork;
            RunWorkerCompleted += DownloadWorker_RunWorkerCompleted;
        }

        private void DownloadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                notificationManager.Show(null, e.Error.Message, NotificationType.Error);
                return;
            }
            var filePath = e.Result as string;
            notificationManager.Show(null, Path.GetFileNameWithoutExtension(filePath) + " " + Utils.GetStringResource("download_worker_success_message"), NotificationType.Success, onClick: () => Utils.TryOpenFile(e.Result as string));
        }

        private async void DownloadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            notificationManager.Show(null, Utils.GetStringResource("download_worker_start_message"), NotificationType.Information);
            WebClient webClient = new WebClient();
            var filePath = e.Argument as string;
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fullPath = Path.Combine(folderPath, Path.GetFileName(filePath));
            fullPath = fullPath.Remove(fullPath.IndexOf("?"));
            await webClient.DownloadFileTaskAsync(e.Argument as string, fullPath);
            e.Result = Path.Combine(folderPath, Path.GetFileName(filePath));
        }
    }
}
