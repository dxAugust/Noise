using Newtonsoft.Json.Linq;
using Noise.Client;
using Noise.MainPages;
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
            deleteButton.Visibility = Visibility.Hidden;

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
                songTitleBox.Text = song.name;
                songGenreList.SelectedIndex = song.genre;

                Image thumbnailPreview = new Image();
                thumbnailPreview.Width = 128;
                thumbnailPreview.Height = 128;

                thumbnailPreview.Source = new BitmapImage(new Uri(Config.serverURL + "/thumbnails/" + song.id + ".png"));
                thumbDropArea.Child = thumbnailPreview;

                melodyIcon.Source = new BitmapImage(new Uri("/Assets/icon-music-green.png", UriKind.RelativeOrAbsolute));
                melodyTooltip.Text = (string)Application.Current.Resources["msgSongFileExist"];

                uploadButton.Content = (string)Application.Current.Resources["editSongButton"];

                deleteButton.Visibility = Visibility.Visible;
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
            dlg.Filter = "Image Files|*.png;*.jpeg;*.gif|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            if (dlg.ShowDialog() == true)
            {
                thumbnailPreview.Source = new BitmapImage(new Uri(dlg.FileName));
                thumbnailUri = dlg.FileName;
                thumbDropArea.Child = thumbnailPreview;
            }
        }

        private void UploadMelody_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".mp3";
            dlg.Filter = "Audio Files|*.mp3;*.wav|MP3 Files (*.mp3)|*.mp3|Wave Files (*.wav)|*.wav";

            if (dlg.ShowDialog() == true)
            {
                melodyIcon.Source = new BitmapImage(new Uri("/Assets/icon-music-green.png", UriKind.RelativeOrAbsolute));
                melodyUri = dlg.FileName;
                melodyTooltip.Text = dlg.FileName;
            }
        }

        private async void uploadSong(UploadData songUploadData)
        {
            ServerResponse serverResponse = await ServerAPI.uploadSong(songUploadData);
            if (serverResponse.statusCode == 200)
            {
                NavigationService.Navigate(new Studio());
            } else if (serverResponse.statusCode == 503) {
                uploadErrorText.Content = (string)Application.Current.Resources["errorUpload"];
            }
        }

        private async void editSong(UploadData songUploadData, int songId)
        {
            ServerResponse serverResponse = await ServerAPI.editSong(songUploadData, songId);
            if (serverResponse.statusCode == 200)
            {
                NavigationService.Navigate(new Studio());
            }
            else if (serverResponse.statusCode == 503)
            {
                uploadErrorText.Content = (string)Application.Current.Resources["errorDelete"];
            }
        }

        private async void deleteSong(int songId)
        {
            ServerResponse serverResponse = await ServerAPI.deleteSong(songId);
            if (serverResponse.statusCode == 200)
            {
                NavigationService.Navigate(new Studio());
            }
            else if (serverResponse.statusCode == 503)
            {
                uploadErrorText.Content = (string)Application.Current.Resources["errorDelete"];
            }
        }

        private async void editSong(UploadData songEditData)
        {
            editSong(songEditData, songData.id);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            deleteSong(songData.id);
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
                        thumbnailPath = thumbnailUri,
                        songPath = melodyUri,
                    };

                    uploadSong(songUploadData);
                } else {
                    uploadErrorText.Content = (string)Application.Current.Resources["errorEmptyValuesUpload"];
                }
            } else
            {
                if ((melodyUri.Length != 0
                || thumbnailUri.Length != 0)
                || songTitleBox.Text.Length != 0)
                {
                    UploadData songUploadData = new UploadData()
                    {
                        songName = songTitleBox.Text,
                        songGenreId = songGenreList.SelectedIndex,
                        thumbnailPath = thumbnailUri,
                        songPath = melodyUri,
                    };

                    editSong(songUploadData);
                } else {
                    uploadErrorText.Content = (string)Application.Current.Resources["errorEmptyValuesUpload"];
                }
            }
        }

        private void DockPanel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Image thumbnailPreview = new Image();
                thumbnailPreview.Width = 128;
                thumbnailPreview.Height = 128;

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                thumbnailPreview.Source = new BitmapImage(new Uri(files[0]));
                thumbnailUri = files[0];
                thumbDropArea.Child = thumbnailPreview;
            }
        }

        private void musicDropArea_PreviewDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Image thumbnailPreview = new Image();
                thumbnailPreview.Width = 128;
                thumbnailPreview.Height = 128;

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                melodyIcon.Source = new BitmapImage(new Uri("/Assets/icon-music-green.png", UriKind.RelativeOrAbsolute));
                melodyUri = files[0];
                melodyTooltip.Text = files[0];
            }
        }
    }
}
