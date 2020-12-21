
// This is demo of how to use async operation in WCF service.

// 1. Interface

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GirishLibrary
{
    [ServiceContract(CallbackContract = typeof(IProgressCallback))]
    public interface IGirish
    {
        [OperationContract]
        Task<string> GetData(int value);

        [OperationContract(IsOneWay = true)]
        Task StartProgress(int target);
    }

    [ServiceContract]
    public interface IProgressCallback
    {
        [OperationContract(IsOneWay = true)]
        Task UpdateProgress(int progress);
    }

}

// 2. Class 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GirishLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Girish : IGirish
    {
        async Task<string> IGirish.GetData(int value)
        {
            return await Task.FromResult(string.Format("You entered: {0}", value));
        }

        async Task IGirish.StartProgress(int target)
        {
            Action action1 = () =>
            {
                int progress = 0;
                double step = (double)target / 40.0;
                while (progress < target)
                {
                    Thread.Sleep(100);
                    progress += (int)step;
                    Console.WriteLine("Progress updated:" + progress);
                    IProgressCallback cb = OperationContext.Current.GetCallbackChannel<IProgressCallback>();
                    cb.UpdateProgress(progress);
                }
            };
            await Task.Run(action1);
        }
    }
}


// 3. Host 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GirishLibrary;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace GirishHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost Sh = new ServiceHost(typeof(Girish));
            Sh.Open();

            Console.WriteLine("Started service:");
            Console.ReadLine();
        }
    }
}


// 4. App.config

<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
    </startup>

    <system.serviceModel>
        <behaviors>
            <serviceBehaviors>
                <behavior name="MyBeh">
                    <serviceMetadata/>
                </behavior>
            </serviceBehaviors>
        </behaviors>
        <services>
            <service name="GirishLibrary.Girish" behaviorConfiguration="MyBeh">
                <endpoint address="net.tcp://localhost:9090/MyTcpEndPoint" binding="netTcpBinding"
                    bindingConfiguration="" contract="GirishLibrary.IGirish" />
                <endpoint address="net.tcp://localhost:9090/MyTcpEndPoint/mex" binding="mexTcpBinding"
                        bindingConfiguration="" contract="IMetadataExchange" />
            </service>
        </services>
    </system.serviceModel>

      // THIS IS VERY VERY VERY IMPORTANT FOR ASYNC OPERATION 
	<appSettings>
		<add key="wcf:disableOperationContextAsyncFlow" value="false" />
	</appSettings>
    
</configuration>


// 5. Client

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

        public GirishCallback()
        {
        }

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


// 6. UI 

 <TextBox x:Name="text1" HorizontalAlignment="Left" Height="23" Margin="186,31,0,0" TextWrapping="Wrap" Text="TextBox" VerticalAlignment="Top" Width="120"/>
        <TextBlock x:Name="text2" HorizontalAlignment="Left" Margin="186,114,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top"/>
        <ProgressBar x:Name="progress1" HorizontalAlignment="Left" Height="46" Margin="169,160,0,0" VerticalAlignment="Top" Width="531" Grid.ColumnSpan="2"/>
        <TextBlock x:Name="text3" HorizontalAlignment="Left" Margin="371,31,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Height="23" Width="155"/>
        <TextBlock x:Name="text4" HorizontalAlignment="Left" Margin="71,253,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Height="59" Width="594"/>
