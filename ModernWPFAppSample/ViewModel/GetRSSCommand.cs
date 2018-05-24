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
    class GetRSSCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            GetRss((RSSViewModel)parameter);
        }

        private void GetRss(RSSViewModel vm)
        {
            using (var reader = XmlReader.Create("http://chronoir.net/feed/"))
            {
                var feed = SyndicationFeed.Load(reader);
                vm.Title = feed.Title.Text;
                vm.Description = feed.Description.Text;
                vm.LastUpdatedTime = feed.LastUpdatedTime.DateTime;
                foreach (var item in feed.Items)
                {
                    vm.Items.Add(new RSSViewModel.RSSContent
                    {
                        Title = item.Title.Text,
                        Summary = item.Summary.Text,
                        PubDate = item.PublishDate.DateTime,
                        link = item.Id
                    });
                }
            }
        }
    }
}
