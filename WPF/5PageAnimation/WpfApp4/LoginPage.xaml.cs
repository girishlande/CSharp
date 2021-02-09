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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await FadeIn();
        }

        public async Task AnimateIn()
        {
            var sb = new Storyboard();
            var slideAnimation = new ThicknessAnimation()
            {
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                From = new Thickness(this.WindowWidth, 0, -this.WindowWidth, 0),
                To = new Thickness(0),
                DecelerationRatio = 0.0f
            };
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
            sb.Children.Add(slideAnimation);
            sb.Begin(this);
        }

        public async Task FadeIn()
        {
            var storyboard = new Storyboard();
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(2)),
                From = 0,
                To = 1,
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
            storyboard.Children.Add(animation);

            storyboard.Begin(this);
        }

        public async Task FadeOut()
        {
            var storyboard = new Storyboard();
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(2)),
                From = 1,
                To = 0,
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
            storyboard.Children.Add(animation);

            storyboard.Begin(this);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await AnimateIn();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await FadeIn();
        }
    }
}
