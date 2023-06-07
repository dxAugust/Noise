using Newtonsoft.Json;
using Noise.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using static Noise.Client.ServerAPI;

namespace Noise.MainPages
{
    /// <summary>
    /// Логика взаимодействия для DiscoverSongs.xaml
    /// </summary>
    public partial class DiscoverSongs : Page
    {
        public DiscoverSongs()
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

            songsList.BeginAnimation(UIElement.OpacityProperty, opacity);
            songsList.BeginAnimation(StackPanel.MarginProperty, posY);

            fetchAllSongs();
        }

        public async void fetchAllSongs()
        {
            ServerResponse serverResponse = await ServerAPI.fetchAllSongs();
            Console.WriteLine(serverResponse.response);

            try
            {
                var discoverSongs = JsonConvert.DeserializeObject<Song[]>(serverResponse.response);

                if (discoverSongs.Length > 0)
                {
                    foreach (Song song in discoverSongs)
                    {
                        Uri thumbURI = new Uri("Assets/music_no_thumbnail.png", UriKind.Relative);

                        if (song.thumbnail_path.Length != 0)
                        {
                            thumbURI = new Uri(song.thumbnail_path);
                        }
                        BitmapImage thumbnailImage = new BitmapImage(thumbURI);
                        
                        var thumbnailPic = new ImageBrush {
                            ImageSource = thumbnailImage,   
                            Stretch = Stretch.Fill,
                        };
                        RenderOptions.SetBitmapScalingMode(thumbnailPic, BitmapScalingMode.HighQuality);

                        var thumbnailBorder = new Border
                        {
                            Width = 150,
                            Height = 150,
                            CornerRadius = new CornerRadius(10),
                        };
                        thumbnailBorder.Background = thumbnailPic;

                        Label songName = new Label
                        {
                            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF")),
                            FontWeight = FontWeights.Bold,
                            FontSize = 20.0,
                            Content = song.name,
                        };

                        Label artistTitle = new Label
                        {
                            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9F9F9F")),
                            FontWeight = FontWeights.Bold,
                            FontSize = 14.0,
                            Content = song.artist_name,
                        };

                        WrapPanel songPanel = new WrapPanel
                        {
                            Width = 150,
                            Height = 250,
                            Orientation = Orientation.Vertical,
                            HorizontalAlignment = HorizontalAlignment.Center,
                        };

                        songPanel.Children.Add(thumbnailBorder);
                        songPanel.Children.Add(songName);
                        songPanel.Children.Add(artistTitle);

                        songsList.Children.Add(songPanel);
                    }
                }
            } catch (Exception ex)
            {

            }
        }
    }
}
