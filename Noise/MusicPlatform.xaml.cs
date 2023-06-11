using NAudio.Gui;
using NAudio.Utils;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Noise.Client;
using Noise.MainPages;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading;
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

        public DiscoverSongs discoverSongsPage = new DiscoverSongs();
        public Studio studioPage = new Studio();

        public static WasapiOut musicPlayer;
        public static MediaFoundationReader mf;

        public bool changingPosition = false;

        public static double time = 2;
        DoubleAnimation opacity = new DoubleAnimation
        {
            From = 0.0,
            To = 1.0,
            Duration = new Duration(TimeSpan.FromSeconds(time)),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
        };

        ThicknessAnimation posY = new ThicknessAnimation
        {
            From = new Thickness(40, 0, 0, 0),
            To = new Thickness(0, 0, 0, 0),
            Duration = new Duration(TimeSpan.FromSeconds(time)),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
        };

        ThicknessAnimation posX = new ThicknessAnimation
        {
            From = new Thickness(-20, 0, 0, 0),
            To = new Thickness(0, 0, 0, 0),
            Duration = new Duration(TimeSpan.FromSeconds(time)),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
        };

        public enum Pages : int
        {
            home = 0,
            studio = 1,
        }
        public static Pages currentPage = Pages.home;

        Thread thread;

        public MusicPlatform()
        {
            InitializeComponent();
            mainScreen.NavigationUIVisibility = NavigationUIVisibility.Hidden;

            discoverSongsPage = new DiscoverSongs();
            mainScreen.Navigate(discoverSongsPage);
            discoverSongsPage.playingNewSong += new EventHandler(playSongByIdAsync);

            profileName.Content = Config.userInfo.login;

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

            using (mf = new MediaFoundationReader(Config.serverURL + "/" + currentPlayingSong.path))
            {
                if (!(musicPlayer is null))
                {
                    musicPlayer.Dispose();
                }
                musicPlayer = new WasapiOut();

                musicPlayer.Volume = (float)((volumeSlider.Value / 10) / 10);

                this.playerSlider.Maximum = mf.TotalTime.TotalSeconds;

                thread = new Thread(OnSongPlaying);
                thread.Start();

                Console.WriteLine(mf.TotalTime.TotalSeconds);

                string maxLength = string.Format("{0:D2}:{1:D2}",
                mf.TotalTime.Minutes,
                mf.TotalTime.Seconds);

                this.playerMax.Text = maxLength;

                
                musicPlayer.Init(mf);
                musicPlayer.Play();
            }
        }

        private void OnSongPlaying()
        {
            while (true)
            {
                if (musicPlayer.PlaybackState == PlaybackState.Playing)
                {
                    try
                    {
                        Dispatcher.Invoke((Action)(() =>
                        {
                            if (!changingPosition)
                            {
                                TimeSpan posTimeSpan = mf.CurrentTime;

                                string currentPositon = string.Format("{0:D2}:{1:D2}",
                                posTimeSpan.Minutes,
                                posTimeSpan.Seconds);

                                this.playerPosition.Text = currentPositon;
                            
                                this.playerSlider.Value = posTimeSpan.TotalSeconds;
                            }
                        }));
                    } catch
                    {
                        
                    }
                    
                }
                Thread.Sleep(1000);
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

        private void playerSlider_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            changingPosition = true;

            musicPlayer.Pause();
            Console.WriteLine("Dragging the slider");
        }

        private void playerSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            changingPosition = false;
            if (!(mf is null))
            {
                mf.CurrentTime = TimeSpan.FromSeconds(playerSlider.Value);
                mf.Position = (long)(playerSlider.Value * mf.WaveFormat.AverageBytesPerSecond);

                musicPlayer.Dispose();

                musicPlayer = new WasapiOut();
                musicPlayer.Init(mf);
                musicPlayer.Play();
                Console.WriteLine("Got a new position: " + mf.CurrentTime);
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage != Pages.home)
            {
                currentPage = Pages.home;
                mainScreen.Navigate(discoverSongsPage);

                categoryTitle.BeginAnimation(StackPanel.MarginProperty, posX);
                categoryTitle.BeginAnimation(StackPanel.OpacityProperty, opacity);
                categoryTitleText.Content = (string)Application.Current.Resources["splashMessage"];
            }
        }
        private void Studio_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage != Pages.studio)
            {
                currentPage = Pages.studio;
                mainScreen.Navigate(studioPage);

                categoryTitle.BeginAnimation(StackPanel.MarginProperty, posX);
                categoryTitle.BeginAnimation(StackPanel.OpacityProperty, opacity);
                categoryTitleText.Content = (string)Application.Current.Resources["studioCategory"];
            }
        }
    }
}
