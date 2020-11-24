// Demo of callback mechanism in WCF . 

// 1. Interface 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GirishLibrary
{
    [ServiceContract(CallbackContract =typeof(IProgressCallback))]
    public interface IGirish
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract(IsOneWay = true)]
        void StartProgress(int target);
    }

    [ServiceContract]
    public interface IProgressCallback
    {
        [OperationContract(IsOneWay = true)]
        void UpdateProgress(int progress);
    }
}


// 2. Service class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace GirishLibrary
{
    public class Girish : IGirish
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public void StartProgress(int target)
        {
            int progress = 0;
            double step = (double)target / 20;
            while(progress<target)
            {
                Thread.Sleep(100);
                progress += (int)step;
                Console.WriteLine("Progress updated:"+progress);
                IProgressCallback cb = OperationContext.Current.GetCallbackChannel<IProgressCallback>();
                cb.UpdateProgress(progress);
            }
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
            var Sh = new ServiceHost(typeof(Girish));
            Sh.Open();
            Console.WriteLine("Service started...");

            Console.ReadLine();

        }
    }
}


4. App.config of host

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
	
</configuration>


5. Cilent WPF 

using GirishClientWPF.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GirishClientWPF
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

            context = new InstanceContext(new GirishCallback(this));
            client = new GirishClient(context);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var target = text1.Text;
            client.StartProgress(int.Parse(target));
        }
    }
}

