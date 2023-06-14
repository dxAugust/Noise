using Newtonsoft.Json.Linq;
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

namespace Noise.MainPages
{
    /// <summary>
    /// Логика взаимодействия для Studio.xaml
    /// </summary>
    public partial class Studio : Page
    {
        public Studio()
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
                From = new Thickness(0, 40, 0, 0),
                To = new Thickness(0, 0, 0, 0),
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            StudioPage.BeginAnimation(UIElement.OpacityProperty, opacity);
            StudioPage.BeginAnimation(StackPanel.MarginProperty, posY);

            fetchSongsByArtist(Config.userInfo.session_token);
        }

        private void fetchSongsByArtist(string session_token)
        {
            ServerResponse serverResponse = await ServerAPI.fetchAllSongs();

            try
            {
                var discoverSongs = JArray.Parse(serverResponse.response).OfType<JObject>();
            } catch (Exception e) {

            }
                
        }

        public void refreshSongsList()
        {

        }
    }
}
