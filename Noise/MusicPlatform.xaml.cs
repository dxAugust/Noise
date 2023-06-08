using NAudio.Utils;
using NAudio.Wave;
using Newtonsoft.Json;
using Noise.Client;
using Noise.MainPages;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Noise.Client.ServerAPI;

namespace Noise
{
    /// <summary>
    /// Логика взаимодействия для MusicPlatform.xaml
    /// </summary>
    public partial class MusicPlatform : Window
    {
        public bool isMaximasized = false;
        public CornerRadius closeButtonRadius;

        Song currentPlayingSong;

        public DiscoverSongs discoverSongsPage;
        WasapiOut musicPlayer = new WasapiOut();

        //System.Threading.Thread Thread = new System.Threading.Thread(new System.Threading.ThreadStart(OnSongPlaying)); 

        public MusicPlatform()
        {
            InitializeComponent();

            discoverSongsPage = new DiscoverSongs();
            mainScreen.Navigate(discoverSongsPage);
            discoverSongsPage.playingNewSong += new EventHandler(playSongByIdAsync);

            profileName.Content = Config.userInfo.login;

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

            var posX = new ThicknessAnimation
            {
                From = new Thickness(-20, 0, 0, 0),
                To = new Thickness(0, 0, 0, 0),
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            profilePanel.BeginAnimation(UIElement.OpacityProperty, opacity);
            Timeline.SetDesiredFrameRate(posY, 140);
            Timeline.SetDesiredFrameRate(opacity, 140);
            Timeline.SetDesiredFrameRate(posX, 140);
            profilePanel.BeginAnimation(Grid.MarginProperty, posY);
            categoryTitle.BeginAnimation(StackPanel.MarginProperty, posX);
            categoryTitle.BeginAnimation(StackPanel.OpacityProperty, opacity);
        }



        private async void playSongByIdAsync(object sender, EventArgs e)
        {
            WrapPanel songPanel = (WrapPanel)sender;
            ServerResponse serverResponse = await ServerAPI.playSongById(Convert.ToInt32(songPanel.Name.Substring(5)));
            currentPlayingSong = JsonConvert.DeserializeObject<Song>(serverResponse.response);

            currentPlayingSongName.Content = currentPlayingSong.name;
            currentPlayingArtistName.Content = currentPlayingSong.artist_name;

            Uri thumbURI = new Uri("./Assets/music_no_thumbnail.png", UriKind.Relative);
            if (currentPlayingSong.thumbnail_path.Length != 0)
            {
                thumbURI = new Uri(Config.serverURL + "/" + currentPlayingSong.thumbnail_path, UriKind.RelativeOrAbsolute);
            }
            BitmapImage thumbnailImage = new BitmapImage(thumbURI);
            currentPlayingThumb.ImageSource = thumbnailImage;

            using (var mf = new MediaFoundationReader(Config.serverURL + "/" + currentPlayingSong.path))
            {
                musicPlayer.Volume = (float)((volumeSlider.Value / 10) / 10);
                //Thread.Start();

                Console.WriteLine(mf.TotalTime.TotalSeconds);

                musicPlayer.Init(mf);
                musicPlayer.Play();
            }
        }

        private void OnSongPlaying()
        {
            while (true)
            {
                if (this.musicPlayer.PlaybackState == PlaybackState.Playing)
                { 
                    Console.WriteLine(musicPlayer.GetPositionTimeSpan().TotalSeconds);
                    
                }
                System.Threading.Thread.Sleep(100);
            }
        }

        private void dragAWindow(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!isMaximasized)
                    DragMove();
            }
            catch (Exception)
            {

            }

            if (e.ClickCount == 2)
            {
                if (!isMaximasized)
                {
                    var time = 2;
                    var sizeAnimationWidth = new DoubleAnimation
                    {
                        From = this.Width,
                        To = SystemParameters.WorkArea.Width,
                        Duration = new Duration(TimeSpan.FromSeconds(time)),
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
                    };

                    var sizeAnimationHeight = new DoubleAnimation
                    {
                        From = this.Height,
                        To = SystemParameters.WorkArea.Height,
                        Duration = new Duration(TimeSpan.FromSeconds(time)),
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
                    };

                    this.BeginAnimation(Window.WidthProperty, sizeAnimationWidth);
                    Timeline.SetDesiredFrameRate(sizeAnimationHeight, 60);
                    this.BeginAnimation(Window.HeightProperty, sizeAnimationHeight);

                    windowBorder.CornerRadius = new CornerRadius(0);
                    songPlayer.CornerRadius = new CornerRadius(0);
                    programMenu.CornerRadius = new CornerRadius(0, 0, 0, 0);

                    this.Left = 0;
                    this.Top = 0;

                    isMaximasized = true;
                } else {
                    this.Width = 800;
                    this.Height = 450;

                    this.Left = (SystemParameters.WorkArea.Width / 2) - (this.Width / 2);
                    this.Top = (SystemParameters.WorkArea.Height / 2) - (this.Height / 2);

                    windowBorder.CornerRadius = new CornerRadius(25);
                    songPlayer.CornerRadius = new CornerRadius(0, 0, 25, 25);
                    programMenu.CornerRadius = new CornerRadius(25, 0, 0, 0);

                    isMaximasized = false;
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            currentVolume.Content = (int)volumeSlider.Value + "%";
            musicPlayer.Volume = (float)((volumeSlider.Value / 10) / 10);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (musicPlayer.PlaybackState == PlaybackState.Playing)
            {
                musicPlayer.Pause();
            } else
            {
                musicPlayer.Play();
            }
        }
    }
}
