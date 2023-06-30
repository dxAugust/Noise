using Noise.Client;
using Noise.MainPages;
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
    /// Логика взаимодействия для EditArtist.xaml
    /// </summary>
    public partial class EditArtist : Page
    {
        Artist editInfo = new Artist();
        public string bannerUri;
        public EditArtist(Artist artistInfo)
        {
            InitializeComponent();

            errorText.Content = "";

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

            artistPanel.BeginAnimation(UIElement.OpacityProperty, opacity);
            artistPanel.BeginAnimation(StackPanel.MarginProperty, posY);

            if (artistInfo != null)
            {
                editInfo = artistInfo;
                usernameBox.Text = artistInfo.username;
                descriptionBox.Text = artistInfo.description;

                Image bannerPreview = new Image();
                bannerPreview.Width = 300;
                bannerPreview.Height = 128;

                if (artistInfo.banner.Length != 0)
                {
                    Uri bannerSrc = new Uri(Config.serverURL + "/banner/" + artistInfo.id + ".png");
                    bannerPreview.Source = new BitmapImage(bannerSrc);
                    thumbDropArea.Child = bannerPreview;
                }
            }
        }
        private async void editArtist(Artist artistInfo)
        {
            ServerResponse serverResponse = await ServerAPI.setArtistInfo(artistInfo, bannerUri);
            if (serverResponse.statusCode == 200)
            {
                NavigationService.Navigate(new Studio());
            } else if (serverResponse.statusCode == 504)
            {
                errorText.Content = (string)Application.Current.Resources["errorOccupiedNick"];
            }
        }

        private void UploadBanner(object sender, MouseButtonEventArgs e)
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
                bannerUri = dlg.FileName;
                thumbDropArea.Child = thumbnailPreview;
            }
        }

        private void artistButton_Click(object sender, RoutedEventArgs e)
        {
            editInfo.username = usernameBox.Text;
            editInfo.description = descriptionBox.Text;
            editArtist(editInfo);
        }
    }
}
