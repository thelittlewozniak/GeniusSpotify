using GeniusSpotify.model;
using Newtonsoft.Json;
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
        private HttpClient httpClient;
        public MainWindow()
        {
            InitializeComponent();
            httpClient = new HttpClient();
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
                         var name = GetSongTitle();
                         if (name != null)
                         {
                             name = name.Trim();
                             // Replace All space (unicode is \\s) to %20 
                             name = name.Replace(" ", "%20");
                             httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Properties.Resources.ACCESS_TOKEN_GENIUS);
                             var result = await httpClient.GetAsync("https://api.genius.com/search?q="+ name);
                             result.EnsureSuccessStatusCode();
                             string jsonBody = await result.Content.ReadAsStringAsync();
                             var resultBody = JsonConvert.DeserializeObject<Search>(jsonBody);
                         }
                     }

                 });
            }
        }
        public string GetSongTitle()
        {
            var proc = Process.GetProcessesByName("Spotify").Where(x => x.MainWindowTitle != "").FirstOrDefault();
            try
            {
                if (proc != null)
                    return proc.MainWindowTitle;
                else
                    throw new SpotifyNotFoundException("Spotify not found");
            }
            catch (SpotifyNotFoundException)
            {
                return null;
            }
        }
    }
}
