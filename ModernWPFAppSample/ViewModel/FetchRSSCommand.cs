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

        public event EventHandler CanExecuteChanged;

        public FetchRSSCommand(RSSViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            var contents = await Task.Run(() => FetchRssAsync());
            foreach (var content in contents)
            {
                _vm.Items.Add(content);
            }
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
