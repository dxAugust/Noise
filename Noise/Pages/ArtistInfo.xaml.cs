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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Noise.Client.ServerAPI;

namespace Noise.Pages.StudioPages
{
    /// <summary>
    /// Логика взаимодействия для ArtistInfo.xaml
    /// </summary>
    public partial class ArtistInfo : Page
    {
        Song lastReleaseSong;
        public ArtistInfo(Artist artistInfo)
        {
            InitializeComponent();

            artistName.Content = artistInfo.username;
            artistDescription.Content = artistInfo.description;
            Uri bannerURI = new Uri(Config.serverURL + "/" + artistInfo.banner, UriKind.RelativeOrAbsolute);
            BitmapImage bannerImage = new BitmapImage(bannerURI);
            artistBanner.ImageSource = bannerImage;
            setupLastRelease(artistInfo);
        }

        public async void setupLastRelease(Artist artistInfo)
        {
            ServerResponse serverResponse = await ServerAPI.getLastRelease(artistInfo.id);
            lastReleaseSong = JsonConvert.DeserializeObject<Song>(serverResponse.response);

            Uri thumbURI = new Uri("./Assets/music_no_thumbnail.png", UriKind.Relative);
            if (lastReleaseSong.thumbnail_path.Length != 0)
            {
                thumbURI = new Uri(Config.serverURL + "/" + lastReleaseSong.thumbnail_path, UriKind.RelativeOrAbsolute);
            }
            BitmapImage thumbnailImage = new BitmapImage(thumbURI);
            lastReleaseImage.ImageSource = thumbnailImage;

            TimeSpan songTimestamp = TimeSpan.FromMilliseconds((double)lastReleaseSong.publication_date);
            DateTime songPublicDate = new DateTime(1970, 1, 1) + songTimestamp;

            string songMonth = (string)Application.Current.Resources["month_" + songPublicDate.Month];
            string songDate = songPublicDate.Day + " " + songMonth.Substring(0, 3).ToUpper() + " " + songPublicDate.Year;

            lastReleaseName.Content = lastReleaseSong.name;
            lastReleaseDate.Content = songDate;
        }

        public event EventHandler playingLastRelease;
        private void lastRelease_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WrapPanel songPanel = new WrapPanel();
            songPanel.Name = "song_" + lastReleaseSong.id;
            playingLastRelease(songPanel, e);
        }
    }
}
