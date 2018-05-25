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
            await FetchRSSFeedAsync();
        }

        private async Task FetchRSSFeedAsync()
        {
            var result = await Task.Run(() => FetchRSS());
            foreach (var item in result)
            {
                _vm.Items.Add(item);
            }
        }

        private Task<List<RSSViewModel.RSSContent>> FetchRSS()
        {
            var result = new List<RSSViewModel.RSSContent>();
            using (var reader = XmlReader.Create(_vm.Url))
            {
                var feed = SyndicationFeed.Load(reader);
                _vm.Title = feed.Title.Text;
                _vm.Description = feed.Description.Text;
                _vm.LastUpdatedTime = feed.LastUpdatedTime.DateTime;

                foreach (var item in feed.Items)
                {
                    result.Add(new RSSViewModel.RSSContent
                    {
                        Title = item.Title.Text,
                        Summary = item.Summary.Text,
                        PubDate = item.PublishDate.DateTime,
                        Link = item.Id
                    });
                }
            }
            return Task.FromResult(result);
        }
    }
}
