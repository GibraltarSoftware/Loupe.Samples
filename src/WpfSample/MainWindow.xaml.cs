using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gibraltar.Agent;

namespace AgentTest.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int BulkLogMessageLoops = 3000;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowViewerButton_Click(object sender, RoutedEventArgs e)
        {
            Log.ShowLiveViewer();
        }

        private void ThrowExceptionButton_Click(object sender, RoutedEventArgs e)
        {
            throw new InvalidOperationException("You can't do that!");
        }

        private void ShowWpfViewerButton_Click(object sender, RoutedEventArgs e)
        {
            AlternateLiveViewer newViewer = new AlternateLiveViewer();
            newViewer.Show();
        }

        private void LogManyMessagesButton_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            for (int curLogMessage = 0; curLogMessage < BulkLogMessageLoops; curLogMessage++)
            {
                Log.Information("WPF Sample.Bulk", "This is a bulk informational message", "It's the {0} message out of {1} informational messages we will write out.", curLogMessage, BulkLogMessageLoops);
                Log.Warning("WPF Sample.Bulk", "This is a bulk warning message", "It's the {0} message out of {1} warning messages we will write out.", curLogMessage, BulkLogMessageLoops);
                Log.Error("WPF Sample.Bulk", "This is a bulk error message", "It's the {0} message out of {1} error messages we will write out.", curLogMessage, BulkLogMessageLoops);
            }
            Cursor = Cursors.Arrow;
        }

        private void PackageAndSendButton_Click(object sender, RoutedEventArgs e)
        {
            using(Packager onePackager = new Packager())
            {
                string desktopSamplePackage = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Sample Package");
                onePackager.SendToFile(SessionCriteria.AllSessions, true, desktopSamplePackage);
            }
        }

        private void ShowPackagerDialogButton_Click(object sender, RoutedEventArgs e)
        {
            PackagerDialog ourDialog = new PackagerDialog();
            ourDialog.Send(); //use all defaults.
        }
    }
}
