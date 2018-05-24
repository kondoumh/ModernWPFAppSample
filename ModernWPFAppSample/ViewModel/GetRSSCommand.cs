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
        private RSSViewModel _vm;

        public event EventHandler CanExecuteChanged;

        public GetRSSCommand(RSSViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            GetRss();
        }

        private void GetRss()
        {
            using (var reader = XmlReader.Create(_vm.Url))
            {
                var feed = SyndicationFeed.Load(reader);
                _vm.Title = feed.Title.Text;
                _vm.Description = feed.Description.Text;
                _vm.LastUpdatedTime = feed.LastUpdatedTime.DateTime;

                foreach (var item in feed.Items)
                {
                    _vm.Items.Add(new RSSViewModel.RSSContent
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
