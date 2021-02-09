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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp9
{
    public class UserEvent : RoutedEventArgs {
        public string Msg { get; set; }
        public UserEvent(RoutedEvent routedEvent) : base(routedEvent)
        {
            
        }
    }

    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public static readonly RoutedEvent MyEvent = EventManager.RegisterRoutedEvent(
            "MyEventName",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(UserControl1)
        );

        public event RoutedEventHandler LoginEventHandler
        {
            add { AddHandler(MyEvent, value); }
            remove { RemoveHandler(MyEvent, value); }

        }

        public UserControl1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var eventArgs = new UserEvent(MyEvent) { Msg = "Hello Ajit" };
            RaiseEvent(eventArgs);
        }
    }
}
