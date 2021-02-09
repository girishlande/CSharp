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

namespace WpfApp14
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public event EventHandler CutEventOccured;
        public event EventHandler CopyEventOccured;
        public event EventHandler PasteEventOccured;

        public MainWindow()
        {
            InitializeComponent();
            CutEventOccured += OnCutEventOccured;
        }

        private void OnCutEventOccured(object sender, EventArgs e)
        {
            Console.WriteLine("Cut occured:");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserAction a1 = new UserAction(this,UserActionEnum.USER_CUT);
            a1.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserAction a1 = new UserAction(this,UserActionEnum.USER_COPY);
            a1.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UserAction a1 = new UserAction(this,UserActionEnum.USER_PASTE);
            a1.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Cut event occured:");
            CutEventOccured?.Invoke(this, new EventArgs());
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            CopyEventOccured?.Invoke(this, new EventArgs());
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            PasteEventOccured?.Invoke(this, new EventArgs());
        }
    }
}
