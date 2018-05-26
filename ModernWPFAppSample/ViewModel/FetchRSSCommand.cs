using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace ModernWPFAppSample.ViewModel
{
    class FetchRSSCommand : ICommand
    {
        private RSSViewModel _vm;

        private bool _fetching = false;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public FetchRSSCommand(RSSViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return !_fetching;
        }

        public async void Execute(object parameter)
        {
            _fetching = true;
            RaiseCanExecuteChanged();
            var contents = await Task.Run(() => FetchRssAsync());
            foreach (var content in contents)
            {
                _vm.Items.Add(content);
            }
            _fetching = false;
            RaiseCanExecuteChanged();
        }

        private Task<List<RSSViewModel.RSSContent>> FetchRssAsync()
        {
            using (var reader = XmlReader.Create(_vm.Url))
            {
                var feed = SyndicationFeed.Load(reader);
                _vm.Title = feed.Title.Text;
                _vm.Description = feed.Description.Text;
                _vm.LastUpdatedTime = feed.LastUpdatedTime.DateTime;

                var result = (from f in feed.Items
                              select new RSSViewModel.RSSContent()
                              {
                                  Title = f.Title.Text,
                                  Summary = f.Summary.Text,
                                  PubDate = f.PublishDate.DateTime,
                                  Link = f.Id
                              }).ToList();
                return Task.FromResult(result);
            }
        }
    }
}
