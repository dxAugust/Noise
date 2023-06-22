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

namespace Noise.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreateArtist.xaml
    /// </summary>
    public partial class CreateArtist : Page
    {
        public CreateArtist()
        {
            InitializeComponent();

            double time = 2;
            var opacity = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            var posY = new ThicknessAnimation
            {
                From = new Thickness(0, 0, 40, 0),
                To = new Thickness(0, 0, 0, 0),
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            mainContent.BeginAnimation(UIElement.OpacityProperty, opacity);
            mainContent.BeginAnimation(StackPanel.MarginProperty, posY);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new EditArtist(null));
        }
    }
}
