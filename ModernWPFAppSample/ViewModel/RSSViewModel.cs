using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModernWPFAppSample.ViewModel
{
    class RSSViewModel : INotifyPropertyChanged
    {
        public string Url { get; set; }
        private string _title;
        public string Title { get { return _title; } set { _title = value; NotifyPropertyChanged(nameof(Title)); } }
        private string _description;
        public string Description { get { return _description; } set { _description = value; NotifyPropertyChanged(nameof(Description)); } }
        private DateTime _lastUpdatedTime;
        public DateTime LastUpdatedTime { get { return _lastUpdatedTime; } set { _lastUpdatedTime = value; NotifyPropertyChanged(nameof(LastUpdatedTime)); } }
        public class RSSContent
        {
            public string Title { get; set; }
            public string Summary { get; set; }
            public DateTime PubDate { get; set; }
            public string link { get; set; }
        }

        private ObservableCollection<RSSContent> _items;
        public ObservableCollection<RSSContent> Items { get { return _items ?? (_items = new ObservableCollection<RSSContent>()); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private GetRSSCommand _getCommand;
        public GetRSSCommand GetCommand
        {
            get
            {
                return _getCommand ?? (_getCommand = new GetRSSCommand(this));
            }
        }
    }
}
