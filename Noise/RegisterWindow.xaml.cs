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

namespace Noise
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
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
            try
            {
                DragMove();
            }
            catch (Exception)
            {

            }
        }

        private void usernameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                emailBox.Focus();
            }
        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                registerUser(usernameBox.Text, passwordBox.Password);
            }
        }

        private void emailBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                passwordBox.Focus();
            }
        }

        private void secondTimeText_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var loginForm = new MainWindow();

            var location = this.PointToScreen(new Point(0, 0));
            loginForm.Left = location.X;
            loginForm.Top = location.Y;

            loginForm.Show();
            this.Close();
        }

        public void registerUser(string login, string password)
        {
            if (usernameBox.Text.Length > 3 || passwordBox.Password.Length > 3)
            {
                
            }
            else
            {
                errorMessage.Text = (string)Application.Current.Resources["errorEmptyValues"];
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            registerUser(usernameBox.Text, passwordBox.Password);
        }
    }
}
