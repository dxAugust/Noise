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

namespace Noise
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            errorMessage.Text = "";

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
                From = new Thickness(0, 100, 0, 0),
                To = new Thickness(0, 0, 0, 0),
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            MainContent.BeginAnimation(UIElement.OpacityProperty, opacity);
            Timeline.SetDesiredFrameRate(posY, 60);
            MainContent.BeginAnimation(Grid.MarginProperty, posY);
            
        }

        private void dragAWindow(object sender, MouseButtonEventArgs e)
        {
            try {
                DragMove();
            } catch (Exception)
            {

            }
        }

        private void usernameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                passwordBox.Focus();
            }
        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                authorizeUser(usernameBox.Text, passwordBox.Password);
            }
        }

        public void authorizeUser(string login, string password)
        {
            if (usernameBox.Text.Length > 3 || passwordBox.Password.Length > 3)
            {

            } else {
                errorMessage.Text = (string)Application.Current.Resources["errorEmptyValues"];
            }
        }

        private void firstTime_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var registerForm = new RegisterWindow();

            var location = this.PointToScreen(new Point(0, 0));
            this.Left = location.X;
            this.Top = location.Y - this.Height;

            registerForm.Show();
            this.Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            authorizeUser(usernameBox.Text, passwordBox.Password);
        }
    }
}
