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
        public HistoryDb histories;
        public MainWindow()
        {
            InitializeComponent();
            spotifyClient = new SpotifyClient();
            histories = new HistoryDb();
            geniusClient = new GeniusClient(Properties.Resources.ACCESS_TOKEN_GENIUS);
            thread = new Thread(CheckButton);
            thread.Start();
            viewHistory.ItemsSource = histories.Histories.ToList();
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
                                 histories.Histories.Add(new History { Title = result.Response.Hits[0].Result.Full_title, Artist = "", Link = result.Response.Hits[0].Result.url });
                                 await histories.SaveChangesAsync();
                                 viewHistory.ItemsSource = histories.Histories.ToList();
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

        private void Title_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                History selected = (History) viewHistory.SelectedItem;
                Process.Start(selected.Link);
            }
        }
    }
}
