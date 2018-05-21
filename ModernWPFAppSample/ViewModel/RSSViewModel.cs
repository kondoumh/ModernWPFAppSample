using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernWPFAppSample.ViewModel
{
    class RSSViewModel
    {
        public RSSViewModel()
        {
            Items = new ObservableCollection<RSSContent>();
        }
        public String Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public class RSSContent
        {
            public String Title { get; set; }
            public String Summary { get; set; }
            public DateTime PubDate { get; set; }
            public String link { get; set; }
        }

        public ObservableCollection<RSSContent> Items { get; set; }
    }
}
