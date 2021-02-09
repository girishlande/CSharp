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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for TempPopup.xaml
    /// </summary>
    public partial class TempPopup : Window
    {
        MainWindow _parent;
        public TempPopup()
        {
            InitializeComponent();
        }
        public TempPopup(MainWindow parent,string message)
        {
            InitializeComponent();
            _parent = parent;
            _parent.Closed += OnWindowClosed;
            text1.Text = message;
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await AnimateIn();
        }

        public async Task AnimateIn()
        {
            var sb = new Storyboard();
            var slideAnimation = new ThicknessAnimation()
            {
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                From = new Thickness(this.Width, 0, -this.Width, 0),
                To = new Thickness(0),
                DecelerationRatio = 0.0f
            };
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
            sb.Children.Add(slideAnimation);
            sb.Begin(this);
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() => Close());
        }
    }
}
