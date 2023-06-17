using Newtonsoft.Json.Linq;
using Noise.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Логика взаимодействия для SongUpload.xaml
    /// </summary>
    public partial class SongUpload : Page
    {
        public string thumbnailUri = "";
        public string melodyUri = "";

        public Song songData;

        public SongUpload(Song song)
        {
            InitializeComponent();

            uploadErrorText.Content = "";

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

            songUploadPanel.BeginAnimation(UIElement.OpacityProperty, opacity);
            songUploadPanel.BeginAnimation(StackPanel.MarginProperty, posY);

            fetchGenreList();

            if (song != null) 
            {
                songData = song;
                uploadButton.Content = (string)Application.Current.Resources["editSongButton"];
            }
        }

        private async void fetchGenreList()
        {
            ServerResponse serverResponse = await ServerAPI.fetchGenreList();

            List<string> genreList = new List<string>();

            var genreJSONList = JArray.Parse(serverResponse.response).OfType<JObject>();

            foreach (var genre in genreJSONList)
            {
                genreList.Add((string)Application.Current.Resources["genre_" + genre["name"].ToString()]);
            }

            songGenreList.ItemsSource = genreList;
        }

        private void Upload_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image thumbnailPreview = new Image();
            thumbnailPreview.Width = 128;
            thumbnailPreview.Height = 128;

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            if (dlg.ShowDialog() == true)
            {
                thumbnailPreview.Source = new BitmapImage(new Uri(dlg.FileName));
                thumbDropArea.Child = thumbnailPreview;
            }
        }

        private void UploadMelody_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".mp3";
            dlg.Filter = "MP3 Files (*.mp3)|*.mp3|Wave Files (*.wav)|*.wav";

            if (dlg.ShowDialog() == true)
            {
                melodyIcon.Source = new BitmapImage(new Uri("/Assets/icon-music-green.png", UriKind.RelativeOrAbsolute));
                melodyTooltip.Text = dlg.FileName;
            }
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            if (songData is null)
            {
                if (melodyUri.Length != 0
                && thumbnailUri.Length != 0
                && songTitleBox.Text.Length != 0)
                {
                    UploadData songUploadData = new UploadData()
                    {
                        songName = songTitleBox.Text,
                        songGenreId = songGenreList.SelectedIndex,
                    };


                }
            }
        }
    }
}
