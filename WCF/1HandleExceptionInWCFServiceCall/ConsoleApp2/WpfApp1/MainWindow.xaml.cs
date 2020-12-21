using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using WpfApp1.ServiceReference1;

namespace WpfApp1
{

    public class GirishCallback : IGirishCallback
    {
        MainWindow m_window;
        public GirishCallback(MainWindow window)
        {
            m_window = window;
        }

        public void UpdateProgress(int progress)
        {
            Console.WriteLine("Girish Callback Progress updated:" + progress);
            m_window.progress1.Value = progress;
            m_window.text2.Text = progress.ToString();
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        InstanceContext context = null;
        GirishClient client = null;

        public MainWindow()
        {
            InitializeComponent();
            InitialiseClient();
        }

        public void InitialiseClient()
        {
            if (client == null)
            {
                context = new InstanceContext(new GirishCallback(this));
                client = new GirishClient(context);
            }
        }
        public void DisposeClient()
        {
            client.Abort();
            client = null;
            context = null;
        }

        void UpdateMessage(bool isAlive)
        {
            Action action1 = () => { if (isAlive) text3.Text = "Status:Running"; else text3.Text = "Status:Stopped"; };
            Dispatcher.Invoke(action1);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InitialiseClient();

            try
            {
                client.StartProgress(100);
                UpdateMessage(true);
            }
            catch (Exception ex)
            {
                UpdateMessage(false);
                DisposeClient();
                Action action1 = () => { text4.Text = ex.Message.ToString(); };
                Dispatcher.Invoke(action1);
            }

        }
    }
}
