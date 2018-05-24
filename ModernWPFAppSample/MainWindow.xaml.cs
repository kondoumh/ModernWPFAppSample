using ModernWPFAppSample.ViewModel;
using System;
using System.Diagnostics;
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
            if (rssViewModel.Items.Count > 0)
            {
                System.Diagnostics.Trace.WriteLine(rssViewModel.Items[RSSListBox.SelectedIndex].Link);
            }
        }
    }
}
