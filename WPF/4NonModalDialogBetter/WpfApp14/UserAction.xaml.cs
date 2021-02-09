using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp14
{
    public enum UserActionEnum
    {
        USER_CUT,
        USER_COPY,
        USER_PASTE
    };

    /// <summary>
    /// Interaction logic for UserAction.xaml
    /// </summary>
    public partial class UserAction : Window
    {
        private UserActionEnum _action;
        MainWindow _parent;

        public UserActionEnum UAction
        {
            get { return _action; }
            set { _action = value; }
        }

        public UserAction()
        {
            InitializeComponent();
        }

        public UserAction(MainWindow parent, UserActionEnum action)
        {
            InitializeComponent();
            _action = action;
            _parent = parent;
            _parent.WaitEventOccured += OnWaitEventOccured;
            text1.Text = "USER ACTION:" + GetActionStr();
        }

        private void OnWaitEventOccured(object sender, WaitEventArgs e)
        {
            if (e.UserAction==_action)
            {
                this.Dispatcher.Invoke(() => Close());
            }
        }

        public string GetActionStr()
        {
            switch (_action)
            {
                case UserActionEnum.USER_CUT: return "CUT";
                case UserActionEnum.USER_COPY: return "COPY";
                case UserActionEnum.USER_PASTE: return "PASTE";
            }
            return "INVALID";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(() => Close());
        }
    }
}
