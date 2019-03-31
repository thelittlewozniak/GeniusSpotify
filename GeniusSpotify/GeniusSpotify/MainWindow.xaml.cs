using Genius;
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
        public MainWindow()
        {
            InitializeComponent();
            spotifyClient = new SpotifyClient();
            geniusClient = new GeniusClient(Properties.Resources.ACCESS_TOKEN_GENIUS);
            var thread = new Thread(CheckButton);
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
                         }
                     }
                 });
            }
        }
    }
}
