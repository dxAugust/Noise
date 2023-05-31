using Newtonsoft.Json;
using Noise.Client;
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
using static Noise.Client.ServerAPI;

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

        public async void authorizeUser(string login, string password)
        {
            if (usernameBox.Text.Length > 3 || passwordBox.Password.Length > 3)
            {
                var serverResponse = await ServerAPI.authUser(login, password);

                if (serverResponse.statusCode == 200)
                {
                    Config.userInfo = JsonConvert.DeserializeObject<User>(serverResponse.response);

                    var musicForm = new MusicPlatform();

                    var location = this.PointToScreen(new Point(0, 0));
                    musicForm.Left = location.X;
                    musicForm.Top = location.Y;

                    musicForm.Show();
                    this.Close();
                } else if (serverResponse.statusCode == 401)
                {
                    errorMessage.Text = (string)Application.Current.Resources["errorWrongPassword"];
                } else if (serverResponse.statusCode == 404) {
                    errorMessage.Text = (string)Application.Current.Resources["errorExistAccount"];
                }
            } else {
                errorMessage.Text = (string)Application.Current.Resources["errorEmptyValues"];
            }
        }

        private void firstTime_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var registerForm = new RegisterWindow();

            var location = this.PointToScreen(new Point(0, 0));
            registerForm.Left = location.X;
            registerForm.Top = location.Y;

            registerForm.Show();
            this.Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            authorizeUser(usernameBox.Text, passwordBox.Password);
        }
    }
}
