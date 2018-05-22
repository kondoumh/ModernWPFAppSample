using ModernWPFAppSample.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;


namespace ModernWPFAppSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RSSListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var content = new RSSViewModel.RSSContent();
            rssViewModel.Title = "Dummy Title";
            rssViewModel.Description = "Dummy Description....";
            rssViewModel.LastUpdatedTime = DateTime.Now;
            content.Title = "hoge";
            content.PubDate = DateTime.Now;
            content.Summary = "hogehoge";
            rssViewModel.Items.Add(content);
        }
    }
}
