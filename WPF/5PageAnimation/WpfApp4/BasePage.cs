using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WpfApp4
{
    class BasePage : Page
    {
        public BasePage()
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await AnimateIn();
        }

        public async Task AnimateIn()
        {
            var sb = new Storyboard();
            var slideAnimation = new ThicknessAnimation() { 
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                From = new Thickness(this.WindowWidth,0,-this.WindowWidth,0),
                To = new Thickness(0),
                DecelerationRatio = 0.0f
                };
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
            sb.Children.Add(slideAnimation);
            sb.Begin(this);
        }
    }
}
