using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WpfApp9
{
    public class LogData {
        public string LogString  { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LogData mLogData;
        public MainWindow()
        {
            InitializeComponent();
            mLogData = new LogData() { LogString = "Hello Girish How are you" };
            this.DataContext = mLogData;

            AddHandler(UserControl1.MyEvent, new RoutedEventHandler(OnMyEvent));
        }

        private void OnMyEvent(object sender, RoutedEventArgs e)
        {
            UserEvent u = e as UserEvent;
            Console.WriteLine("Got some message:" + u.Msg);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t1 = sender as TextBox;
            Console.WriteLine("text changed:"+t1.Text.ToString());

            string s1 = t1.Text.ToString();
            int n;
            bool isParsable = int.TryParse(s1, out n);
            if (isParsable)
            {
                Console.WriteLine("String is number :"+n);
                if (n>50 && n<100)
                {
                    t1.Background = new SolidColorBrush(Colors.Green);
                }else
                {
                    t1.Background = new SolidColorBrush(Colors.Red);
                }
            } else
            {
                Console.WriteLine("String is not number:");
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            text1.Dispatcher.Invoke(() => text1.Text += "hello");
        }
    }
}
