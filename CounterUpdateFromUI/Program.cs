=====================================
UI
=====================================
<Window  x:Name="MyWindow"
        x:Class="WpfApp1.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfApp1"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    <Grid>
        <Viewbox>
            <TextBlock Text="{Binding ElementName=MyWindow, Path=MyProperty}"></TextBlock>
        </Viewbox>
    </Grid>
</Window>

=====================================
Source file
=====================================

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


        public int MyProperty
        {
            get { return (int)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(int), typeof(MainWindow), new PropertyMetadata(0));


        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer(TimeSpan.FromSeconds(0.2), DispatcherPriority.Normal,
                delegate
                {
                    int newvalue = 0;
                    if (MyProperty == int.MaxValue)
                    {
                        newvalue = 0;
                    }
                    else
                    {
                        newvalue = MyProperty + 1;
                    }
                    SetValue(MyPropertyProperty, newvalue);
                }, Dispatcher);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
        }

    }
}
