using Genius;
using RepositoryDatabase;
using RepositoryDatabase.Database;
using Spotify;
using System.Diagnostics;
using System.Linq;
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
        private bool checking = true;
        private readonly ISpotify spotifyClient;
        private readonly IGenius geniusClient;
        private readonly Thread thread;
        private readonly HistoryDb histories = new HistoryDb();
        private readonly IRepository<History> repository;
        private History latest;
        public MainWindow()
        {
            InitializeComponent();
            spotifyClient = new SpotifyClient();
            geniusClient = new GeniusClient(Properties.Resources.ACCESS_TOKEN_GENIUS);
            thread = new Thread(CheckButton);
            thread.Start();
            repository = new RepositoryDB<History>(histories);
            viewHistory.ItemsSource = repository.List().ToList();
        }
        public async void CheckButton()
        {
            while (checking)
            {
                await Application.Current.Dispatcher.Invoke(async delegate
                 {
                     if (Keyboard.IsKeyDown(Key.F2))
                     {
                         var name = spotifyClient.GetSongTitle();
                         if (name != null && name !="Spotify")
                         {
                             var result = await geniusClient.SearchSong(name);
                             if(result.Response.Hits.Count > 0)
                             {
                                 Process.Start(result.Response.Hits[0].Result.url);
                                 var history = new History
                                 {
                                     Title = result.Response.Hits[0].Result.Full_title,
                                     Artist = "",
                                     Link = result.Response.Hits[0].Result.url
                                 };
                                 AddToHistory(history);
                             }
                         }
                         else if(latest != null)
                         {
                             Process.Start(latest.Link);
                             AddToHistory(latest);
                         }
                         else
                         {
                             latest = repository.List().OrderByDescending(x => x.ListenedAt).FirstOrDefault();
                             Process.Start(latest.Link);
                             AddToHistory(latest);
                         }
                     }
                 });
            }
        }
        private void AddToHistory(History history)
        {
            repository.Add(history);
            latest = history;
            viewHistory.ItemsSource = repository.List().ToList();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            checking = false;
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
