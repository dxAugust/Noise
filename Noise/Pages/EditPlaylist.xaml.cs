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
    /// Логика взаимодействия для EditPlaylist.xaml
    /// </summary>
    public partial class EditPlaylist : Page
    {
        public string thumbnailURI = "";
        public Playlist editingPlaylist;
        public EditPlaylist(Playlist playlist)
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

            errorText.Content = "";

            if (!(playlist is null))
            {
                editingPlaylist = playlist;

                namePlaylist.Text = playlist.name;
                descriptionBox.Text = playlist.description;

                playlistCreateButton.Content = (string)Application.Current.Resources["editPlaylistButton"];

                if (playlist.playlistThumb.Length != 0)
                {
                    Image thumbnailPreview = new Image();
                    thumbnailPreview.Width = 128;
                    thumbnailPreview.Height = 128;

                    thumbnailPreview.Source = new BitmapImage(new Uri(Config.serverURL + playlist.playlistThumb));
                    thumbDropArea.Child = thumbnailPreview;
                }
            }
        }

        private void playlistCreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (editingPlaylist is null)
            {
                if (namePlaylist.Text == String.Empty)
                {
                    errorText.Content = (string)Application.Current.Resources["errorName"];
                    return;
                }

                Playlist playlist = new Playlist()
                {
                    id = 0,
                    name = namePlaylist.Text,
                    description = descriptionBox.Text,
                    songs_id = new int[0],
                    playlistThumb = thumbnailURI,
                };

                editPlaylist(playlist);
            } else
            {
                if (namePlaylist.Text == String.Empty)
                {
                    errorText.Content = (string)Application.Current.Resources["errorName"];
                    return;
                }

                Playlist playlist = new Playlist()
                {
                    id = editingPlaylist.id,
                    name = namePlaylist.Text,
                    description = descriptionBox.Text,
                    songs_id = editingPlaylist.songs_id,
                    playlistThumb = thumbnailURI,
                };

                if (thumbnailURI.Length != 0)
                {
                    playlist.playlistThumb = thumbnailURI;
                }

                editPlaylist(playlist);
            }
            
        }

        private async void editPlaylist(Playlist playlist)
        {
            ServerResponse serverResponse = await ServerAPI.editPlaylist(playlist);

            if (serverResponse.statusCode == 200)
            {
                NavigationService.Navigate(new Playlists());
            }
        }

        private void UploadThumb(object sender, MouseButtonEventArgs e)
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
                thumbnailURI = dlg.FileName;
                thumbDropArea.Child = thumbnailPreview;
            }
        }
    }
}
