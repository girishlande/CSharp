
// ------------------------------------------------------------------------
//  Testing multiple calls to WCF service using multithreading
//  Check how many calls it can handle. 
//  Result: It works pretty fine. It will give response slowly but it can handle calls 
// ------------------------------------------------------------------------

In this example we are creating WCF service and calling one of the api many times on thread execution. 

// ------------------------
// Service Interface
// ------------------------
namespace GirishServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IGirishService
    {
        [OperationContract]
        string GetData(int value);

    }
}

// ------------------------
// Service Class
// ------------------------
namespace GirishServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class GirishService : IGirishService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

    }
}

// -----------------
// WPF Client 
// -----------------

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private readonly Random _random = new Random();

        private void b1_Click(object sender, RoutedEventArgs e)
        {

            Thread loadInitialValueThread = new Thread(new ThreadStart(ThreadFunction));
            loadInitialValueThread.IsBackground = true;
            loadInitialValueThread.Start();
        }

        private void ThreadFunction()
        {
            GirishServiceClient client = new GirishServiceClient();

            for (int i = 0; i < 10000; i++)
            {
                var output = client.GetData(i);
                UpdateMessage(output);
                Thread.Sleep(1);
            }

            client.Close();
        }

        void UpdateMessage(string message)
        {
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                            new Action<object>(stringupdatedelegate),
                            message);
        }

        public void stringupdatedelegate(Object obj)
        {
            string name = (string)obj;
            text1.Text = name;
        }
    }
}



