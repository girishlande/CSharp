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
        public event EventHandler<WaitEventArgs> WaitEventOccured;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CutClick(object sender, RoutedEventArgs e)
        {
            UserAction a1 = new UserAction(this,UserActionEnum.USER_CUT);
            a1.Show();
        }

        private void CopyClick(object sender, RoutedEventArgs e)
        {
            UserAction a1 = new UserAction(this,UserActionEnum.USER_COPY);
            a1.Show();
        }

        private void PasteClick(object sender, RoutedEventArgs e)
        {
            UserAction a1 = new UserAction(this,UserActionEnum.USER_PASTE);
            a1.Show();
        }

        private void CutDoneClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Cut event occured:");
            WaitEventOccured?.Invoke(this, new WaitEventArgs() { UserAction = UserActionEnum.USER_CUT });
        }

        private void CopyDoneClick(object sender, RoutedEventArgs e)
        {
            WaitEventOccured?.Invoke(this, new WaitEventArgs() { UserAction = UserActionEnum.USER_COPY });
        }

        private void PasteDoneClick(object sender, RoutedEventArgs e)
        {
            WaitEventOccured?.Invoke(this, new WaitEventArgs() { UserAction = UserActionEnum.USER_PASTE });
        }
    }

    public class WaitEventArgs : EventArgs
    {
        private UserActionEnum _userAction;

        public UserActionEnum UserAction
        {
            get { return _userAction; }
            set { _userAction = value; }
        }

    }
}
