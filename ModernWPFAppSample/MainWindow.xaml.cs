using ModernWPFAppSample.ViewModel;
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
            RSSViewModel vm = new RSSViewModel();
            vm.Title = "hoge";
            vm.Description = "HogeHoge";
            vm.PubDate = System.DateTime.Now;
        }
    }
}
