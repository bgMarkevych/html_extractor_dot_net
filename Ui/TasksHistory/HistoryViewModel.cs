using html_exctractor.Common;
using html_exctractor.Model;
using html_exctractor.Service;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace html_exctractor.Ui.TasksHistory
{
    public class HistoryViewModel : ViewModelBase
    {
        public ObservableCollection<History> Histories { get; set; }
        public ICommand HistoryClickCommand { get; set; }
        public ICommand ClearHistoryCommand { get; set; }
        public bool IsHistoriesEmpty
        {
            get
            {
                if (Histories == null)
                {
                    return false;
                }
                return Histories.Count == 0;
            }
        }

        protected override bool ShowGlobalLoader => true;

        private DownloadWorker downloadWorker = new DownloadWorker();
        private FileSystemWatcher watcher = new FileSystemWatcher();
        public HistoryViewModel()
        {
            HistoryClickCommand = new RelayCommand(historyClick);
            ClearHistoryCommand = new RelayCommand(clearHistory);
        }

        public async void PageLoaded()
        {
            Loading = true;
            try
            {
                Histories = new ObservableCollection<History>(await FirebaseManager.GetTasksHistory());
                NotifyProperty(nameof(Histories));
                NotifyProperty(nameof(IsHistoriesEmpty));
            }
            catch (Exception e)
            {
                OnError(e);
            }
            Loading = false;

            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            watcher.Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;

            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            foreach (History h in Histories)
            {
                h.NotifyChangeFileStructure();
            }
        }

        private void historyClick(object value)
        {
            var item = value as History;
            if (item.LoocalFileExists)
            {
                Utils.TryOpenFile(item.LocalPath);
                return;
            }

            downloadWorker.RunWorkerAsync(item.Url);
        }

        private async void clearHistory(object obj)
        {
            Loading = true;
            try
            {
                await FirebaseManager.ClearHistory();
                Histories.Clear();
                NotifyProperty(nameof(IsHistoriesEmpty));
            }
            catch (Exception e)
            {
                OnError(e);
            }
            Loading = false;
        }

    }
}
