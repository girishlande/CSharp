using System;
using System.IO;
using System.Windows;

namespace AssignmentWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Private members
        private string logFile = Path.Combine(Environment.CurrentDirectory, "Logs.txt");
        #endregion

        #region Private methods
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            File.AppendAllText(logFile, string.Format("{0} : Error! {1}.", DateTime.Now.ToString(), e.Exception.Message));
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {

        } 
        #endregion
    }
}
