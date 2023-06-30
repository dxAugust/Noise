using Newtonsoft.Json;
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

namespace Noise.Pages
{
    /// <summary>
    /// Логика взаимодействия для Playlists.xaml
    /// </summary>
    public partial class Playlists : Page
    {
        public Playlists()
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
                From = new Thickness(40, 0, 0, 0),
                To = new Thickness(0, 0, 0, 0),
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            mainContent.BeginAnimation(UIElement.OpacityProperty, opacity);
            mainContent.BeginAnimation(StackPanel.MarginProperty, posY);

            getPlaylists();
        }

        public class playlistItem
        {
            public ImageSource image { get; set; }

            public int id { get; set; }
            public string name { get; set; }
            public int songsAmount { get; set; }
        }

        public event EventHandler pageChange;

        public async void getPlaylists()
        {
            ServerResponse serverResponse = await ServerAPI.getPlaylists(Config.userInfo.id);
            try
            {
                if (serverResponse.statusCode == 200)
                {
                    List<playlistItem> playLists = new List<playlistItem>();
                    var userPlayList = JArray.Parse(serverResponse.response).OfType<JObject>();

                    foreach (var playlist in userPlayList)
                    {
                        Uri thumbURI = new Uri("./Assets/music_no_thumbnail.png", UriKind.Relative);

                        if (playlist["playlistThumb"].ToString().Length != 0)
                        {
                            thumbURI = new Uri(Config.serverURL + "/" + playlist["playlistThumb"].ToString(), UriKind.RelativeOrAbsolute);
                        }
                        BitmapImage thumbnailImage = new BitmapImage(thumbURI);

                        playLists.Add(new playlistItem()
                        {
                            image = thumbnailImage,
                            id = (int)playlist["id"],
                            name = playlist["name"].ToString(),
                            songsAmount = ((JArray)playlist["songs_id"]).Count,
                        });
                    }

                    playlistList.ItemsSource = playLists;
                }
            }
            catch
            {

            }
        }

        private void AddPlaylist_Click(object sender, RoutedEventArgs e)
        {
            EditPlaylist editPlaylistPage = new EditPlaylist(null);
            NavigationService.Navigate(editPlaylistPage);
        }

        private void playlistList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            playlistItem playlistSelected = playlistList.SelectedItem as playlistItem;

            if (!(playlistSelected is null))
            {
                infoPlaylist(playlistSelected.id);
            }
        }

        private async void infoPlaylist(int playlistId)
        {
            ServerResponse serverResponse = await ServerAPI.getPlaylistById(playlistId);
            Playlist playlistObject = JsonConvert.DeserializeObject<Playlist>(serverResponse.response);

            PlaylistSongs editPlaylistPage = new PlaylistSongs(playlistObject);
            NavigationService.Navigate(editPlaylistPage);
        }

        private async void editPlaylist(int playlistId)
        {
            ServerResponse serverResponse = await ServerAPI.getPlaylistById(playlistId);
            Playlist playlistObject = JsonConvert.DeserializeObject<Playlist>(serverResponse.response);

            EditPlaylist editPlaylistPage = new EditPlaylist(playlistObject);
            NavigationService.Navigate(editPlaylistPage);
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            playlistItem playlistSelected = playlistList.SelectedItem as playlistItem;

            if (!(playlistSelected is null))
            {
                editPlaylist(playlistSelected.id);
            }
        }
    }
}
