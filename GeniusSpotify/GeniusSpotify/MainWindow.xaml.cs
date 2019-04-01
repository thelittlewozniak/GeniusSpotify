using Genius;
using GeniusSpotify.HistoryWork;
using Spotify;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace GeniusSpotify
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool Checking { get; set; } = true;
        private ISpotify spotifyClient;
        private IGenius geniusClient;
        private Thread thread;
        public Histories histories;
        public MainWindow()
        {
            InitializeComponent();
            spotifyClient = new SpotifyClient();
            histories = Histories.Instance;
            geniusClient = new GeniusClient(Properties.Resources.ACCESS_TOKEN_GENIUS);
            thread = new Thread(CheckButton);
            thread.Start();
        }
        public async void CheckButton()
        {
            while (Checking)
            {
                await Application.Current.Dispatcher.Invoke(async delegate
                 {
                     if (Keyboard.IsKeyDown(Key.F2))
                     {
                         var name = spotifyClient.GetSongTitle();
                         if (name != null)
                         {
                             var result = await geniusClient.SearchSong(name);
                             if(result.Response.Hits.Count > 0)
                             {
                                 Process.Start(result.Response.Hits[0].Result.url);
                                 histories.AddHistory(result.Response.Hits[0].Result.Full_title, "", result.Response.Hits[0].Result.url);
                                 histories.Save();
                             }
                         }
                     }
                 });
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Checking = false;
            thread.Abort();
            Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
