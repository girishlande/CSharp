using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int counter = 0;

        public MainWindow()
        {
            InitializeComponent();
            myslider.Minimum = 10;
            myslider.Maximum = 200;
            myslider.Value = 50;
            //new Thread(Work).Start();

            var t1 = new System.Timers.Timer();
            t1.Interval = 1000;
            t1.Elapsed += onTimerEvent;
            t1.Start();
        }

        private void onTimerEvent(object sender, ElapsedEventArgs e)
        {
            UpdateMessage("Girish"+counter++);
        }

        void Work()
        {
            Thread.Sleep(3000);           // Simulate time-consuming task
            UpdateMessage("The answer");
        }

        void UpdateMessage(string message)
        {
            Action action = () => txtMessage.Text = message;
            Dispatcher.Invoke(action);
			
			
			Dispatcher.Invoke(() => txtMessage.Text = message);
        }

    }
}
