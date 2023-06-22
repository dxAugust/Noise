using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Noise.Client;
using Noise.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            fetchSongsByArtist(Config.userInfo.id);
        }

        public class songItem
        {
            public int song_id { get; set; }
            public ImageSource image { get; set; }
            public string name { get; set; }
            public int plays { get; set; }
            public string publicationDate { get; set; }
        }

        private async void fetchSongsByArtist(int userId)
        {
            ServerResponse serverResponse = await ServerAPI.fetchSongByArtistUID(Config.userInfo.id);

            Console.WriteLine(serverResponse.statusCode);

            try
            {
                if (serverResponse.statusCode == 200)
                {
                    List<songItem> songList = new List<songItem>();
                    var userSongList = JArray.Parse(serverResponse.response).OfType<JObject>();
                    foreach (var song in userSongList)
                    {
                        Uri thumbURI = new Uri("./Assets/music_no_thumbnail.png", UriKind.Relative);

                        if (song["thumbnail_path"].ToString().Length != 0)
                        {
                            thumbURI = new Uri(Config.serverURL + "/" + song["thumbnail_path"].ToString(), UriKind.RelativeOrAbsolute);
                        }
                        BitmapImage thumbnailImage = new BitmapImage(thumbURI);

                        TimeSpan songTimestamp = TimeSpan.FromMilliseconds((double)song["publication_date"]);
                        DateTime songPublicDate = new DateTime(1970, 1, 1) + songTimestamp;

                        string songDate = songPublicDate.Day + " " + (string)Application.Current.Resources["month_" + songPublicDate.Month] + " " + songPublicDate.Year;

                        songList.Add(new songItem()
                        {
                            song_id = (int)song["id"],
                            image = thumbnailImage,
                            name = song["name"].ToString(),
                            plays = (int)song["plays"],
                            publicationDate = songDate,
                        });
                    }

                    SongsList.ItemsSource = songList.OrderBy(song => song.publicationDate).ToList();
                } 
                
                if (serverResponse.statusCode == 506) { 
                    NavigationService.Navigate(new CreateArtist());
                }
            } catch (Exception e) {

            }
                
        }

        public void refreshSongsList()
        {

        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SongUpload(null));
        }

        private async void openEditTab(songItem songItem)
        {
            ServerResponse serverResponse = await ServerAPI.fetchSongById(songItem.song_id);
            
            if (serverResponse.statusCode == 200)
            {
                NavigationService.Navigate(new SongUpload(JsonConvert.DeserializeObject<Song>(serverResponse.response)));
            }
        }

        private void SongsList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (SongsList.SelectedItems.Count > 0)
            {
                foreach (var song in SongsList.SelectedItems)
                {
                    songItem songItem = song as songItem;
                    openEditTab(songItem);
                }
            }
        }
    }
}
