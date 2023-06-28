using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using WpfAnimatedGif;
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
            songsList.Children.Clear();
            ServerResponse serverResponse = await ServerAPI.fetchAllSongs();

            try
            {
                var discoverSongs = JArray.Parse(serverResponse.response).OfType<JObject>();

                foreach (var song in discoverSongs)
                {
                    Uri thumbURI = new Uri("./Assets/music_no_thumbnail.png", UriKind.Relative);

                    if (song["thumbnail_path"].ToString().Length != 0)
                    {
                        thumbURI = new Uri(Config.serverURL + "/" + song["thumbnail_path"].ToString(), UriKind.RelativeOrAbsolute);
                    }
                    BitmapImage thumbnailImage = new BitmapImage(thumbURI);

                    var thumbnailPic = new ImageBrush
                    {
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
                        Content = song["name"].ToString(),
                    };

                    Label artistTitle = new Label
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9F9F9F")),
                        FontWeight = FontWeights.Bold,
                        FontSize = 14.0,
                        Content = song["artist_name"].ToString(),
                    };

                    WrapPanel songPanel = new WrapPanel
                    {
                        Width = 150,
                        Height = 250,
                        Orientation = Orientation.Vertical,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(20),
                        Name = "song_" + song["id"].ToString(),
                    };

                    Uri playURI = new Uri("/Assets/icon-play.png", UriKind.Relative);
                    Uri playsURI = new Uri("/Assets/icon-plays.png", UriKind.Relative);
                    BitmapImage playImg = new BitmapImage(playURI);
                    BitmapImage playsImg = new BitmapImage(playsURI);

                    WrapPanel playPanel = new WrapPanel
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Orientation = Orientation.Vertical,
                    };
                    Image playsImage = new Image
                    {
                        Width = 20,
                        Height = 20,
                        Margin = new Thickness(0, 20, 0, 0),
                        Source = playsImg,
                        Opacity = 0.0,
                    };
                    RenderOptions.SetBitmapScalingMode(playsImage, BitmapScalingMode.HighQuality);

                    Label playsLabel = new Label
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9F9F9F")),
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        FontSize = 12.0,
                        Content = song["plays"].ToString(),
                        Opacity = 0.0,
                    };

                    playPanel.Children.Add(playsImage);
                    playPanel.Children.Add(playsLabel);

                    thumbnailBorder.Child = playPanel;

                    songPanel.AddHandler(WrapPanel.MouseEnterEvent, new RoutedEventHandler(songPanel_Hover));
                    songPanel.AddHandler(WrapPanel.MouseLeaveEvent, new RoutedEventHandler(songPanel_UnHover));
                    songPanel.AddHandler(WrapPanel.MouseDownEvent, new RoutedEventHandler(playSong_Click));

                    songPanel.Children.Add(thumbnailBorder);
                    songPanel.Children.Add(songName);
                    songPanel.Children.Add(artistTitle);

                    songsList.Children.Add(songPanel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async void loadSearchResults(string term)
        {
            ServerResponse serverResponse = await ServerAPI.searchSong(term);

            try
            {
                var discoverSongs = JArray.Parse(serverResponse.response).OfType<JObject>();

                foreach (var song in discoverSongs)
                {
                    Uri thumbURI = new Uri("./Assets/music_no_thumbnail.png", UriKind.Relative);

                    if (song["thumbnail_path"].ToString().Length != 0)
                    {
                        thumbURI = new Uri(Config.serverURL + "/" + song["thumbnail_path"].ToString(), UriKind.RelativeOrAbsolute);
                    }
                    BitmapImage thumbnailImage = new BitmapImage(thumbURI);

                    var thumbnailPic = new ImageBrush
                    {
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
                        Content = song["name"].ToString(),
                    };

                    Label artistTitle = new Label
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9F9F9F")),
                        FontWeight = FontWeights.Bold,
                        FontSize = 14.0,
                        Content = song["artist_name"].ToString(),
                    };

                    WrapPanel songPanel = new WrapPanel
                    {
                        Width = 150,
                        Height = 250,
                        Orientation = Orientation.Vertical,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(20),
                        Name = "song_" + song["id"].ToString(),
                    };

                    Uri playURI = new Uri("/Assets/icon-play.png", UriKind.Relative);
                    Uri playsURI = new Uri("/Assets/icon-plays.png", UriKind.Relative);
                    BitmapImage playImg = new BitmapImage(playURI);
                    BitmapImage playsImg = new BitmapImage(playsURI);

                    WrapPanel playPanel = new WrapPanel
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Orientation = Orientation.Vertical,
                    };
                    Image playsImage = new Image
                    {
                        Width = 20,
                        Height = 20,
                        Margin = new Thickness(0, 20, 0, 0),
                        Source = playsImg,
                        Opacity = 0.0,
                    };
                    RenderOptions.SetBitmapScalingMode(playsImage, BitmapScalingMode.HighQuality);

                    Label playsLabel = new Label
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9F9F9F")),
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        FontSize = 12.0,
                        Content = song["plays"].ToString(),
                        Opacity = 0.0,
                    };

                    playPanel.Children.Add(playsImage);
                    playPanel.Children.Add(playsLabel);

                    thumbnailBorder.Child = playPanel;

                    songPanel.AddHandler(WrapPanel.MouseEnterEvent, new RoutedEventHandler(songPanel_Hover));
                    songPanel.AddHandler(WrapPanel.MouseLeaveEvent, new RoutedEventHandler(songPanel_UnHover));
                    songPanel.AddHandler(WrapPanel.MouseDownEvent, new RoutedEventHandler(playSong_Click));

                    songPanel.Children.Add(thumbnailBorder);
                    songPanel.Children.Add(songName);
                    songPanel.Children.Add(artistTitle);

                    songsList.Children.Add(songPanel);
                }
            } catch
            {

            }
        }

        public event EventHandler playingNewSong;
        void playSong_Click(object sender, RoutedEventArgs e)
        {
            WrapPanel playSong = (WrapPanel)sender;
            playingNewSong(sender, e);
        }

        void playImage_Hover(object sender, RoutedEventArgs e)
        {
            Image playImage = (Image)sender;

            double time = 1;
            var opacity = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            var posY = new ThicknessAnimation
            {
                From = new Thickness(0, 20, 0, 0),
                To = new Thickness(0, 0, 0, 0),
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            playImage.BeginAnimation(UIElement.OpacityProperty, opacity);
            playImage.BeginAnimation(Image.MarginProperty, posY);
        }

        void playImage_UnHover(object sender, RoutedEventArgs e)
        {
            Image playImage = (Image)sender;

            double time = 1;
            var opacity = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            var posY = new ThicknessAnimation
            {
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(0, 20, 0, 0),
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            playImage.BeginAnimation(UIElement.OpacityProperty, opacity);
            playImage.BeginAnimation(Image.MarginProperty, posY);
        }

        void songPanel_Hover(object sender, RoutedEventArgs e)
        {
            WrapPanel songPanel = (WrapPanel)sender;

            songPanel.Children[0].AddHandler(Border.MouseEnterEvent, new RoutedEventHandler(border_Hover));
            songPanel.Children[0].AddHandler(Border.MouseLeaveEvent, new RoutedEventHandler(border_UnHover));

            double time = 1;
            var opacityPanel = new DoubleAnimation
            {
                From = 1.0,
                To = 0.6,
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            songPanel.BeginAnimation(UIElement.OpacityProperty, opacityPanel);
        }

        void songPanel_UnHover(object sender, RoutedEventArgs e)
        {
            WrapPanel songPanel = (WrapPanel)sender;

            double time = 1;
            var opacity = new DoubleAnimation
            {
                From = 0.6,
                To = 1.0,
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            songPanel.BeginAnimation(UIElement.OpacityProperty, opacity);
        }

        void border_Hover(object sender, RoutedEventArgs e)
        {
            Border playPanel = (Border)sender;

            playPanel.Child.AddHandler(WrapPanel.MouseEnterEvent, new RoutedEventHandler(playWrap_Hover));
        }

        void border_UnHover(object sender, RoutedEventArgs e)
        {
            Border playPanel = (Border)sender;
            playPanel.Child.AddHandler(WrapPanel.MouseLeaveEvent, new RoutedEventHandler(playWrap_UnHover));
        }

        void playWrap_Hover(object sender, RoutedEventArgs e)
        {
            WrapPanel playPanel = (WrapPanel)sender;

            double time = 1;
            var opacity = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            var posY = new ThicknessAnimation
            {
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(0, 20, 0, 0),
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            playPanel.Children[1].BeginAnimation(UIElement.OpacityProperty, opacity);
            playPanel.Children[1].BeginAnimation(Image.MarginProperty, posY);
        }

        void playWrap_UnHover(object sender, RoutedEventArgs e)
        {
            WrapPanel playPanel = (WrapPanel)sender;

            double time = 1;
            var opacity = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            var posY = new ThicknessAnimation
            {
                From = new Thickness(0, 20, 0, 0),
                To = new Thickness(0, 0, 0, 0),
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            playPanel.Children[1].BeginAnimation(UIElement.OpacityProperty, opacity);
            playPanel.Children[1].BeginAnimation(Image.MarginProperty, posY);
        }

        private void searchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                if (String.IsNullOrEmpty(searchBox.Text))
                {
                    songsList.Children.Clear();
                    fetchAllSongs();
                }
            }

            if (!String.IsNullOrEmpty(searchBox.Text))
            {
                songsList.Children.Clear();
                loadSearchResults(searchBox.Text);
            }
            
        }
    }
}
