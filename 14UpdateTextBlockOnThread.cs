using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public object LoadInitialDataThreadMethod { get; private set; }

        public Window1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Thread loadInitialValueThread = new Thread(new ThreadStart(ThreadFunction));
            loadInitialValueThread.IsBackground = true;
            loadInitialValueThread.Start();
        }

        private void ThreadFunction()
        {
            for(int i=0;i<10;i++)
            {
                string msg = " [ Thread:" + Thread.CurrentThread.ManagedThreadId + " i:" + i + "] ";
                UpdateMessage(msg);
                UpdatNumber(i);
                Thread.Sleep(1000);
            }
        }

        void UpdatNumber(int num)
        {
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                           new Action<object>(intupdatedelegate),
                           num);
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
            label1.Text += name;
        }

        public void intupdatedelegate(Object obj)
        {
            int name = (int)obj;
            label1.Text += " ->" + name;
        }

    }
}
