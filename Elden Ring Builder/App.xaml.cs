using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Threading;

namespace Elden_Ring_Builder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += (s, e) =>
            {
                MessageBox.Show(e.Exception.ToString(), "Unhandled Exception");
                e.Handled = true;
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                MessageBox.Show(e.ExceptionObject.ToString(), "AppDomain Exception");
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                MessageBox.Show(e.Exception.ToString(), "Task Exception");
                e.SetObserved();
            };
        }
    }
}
