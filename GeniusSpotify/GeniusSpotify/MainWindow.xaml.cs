using Genius;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeniusSpotify
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool Checking { get; set; } = true;
        public MainWindow()
        {
            InitializeComponent();
            CheckButton();
        }
        public async void CheckButton()
        {
            while (Checking)
            {
                if(Keyboard.IsKeyDown(Key.F2))
                {
                    var name = GetSongTitle();
                    if(name!=null)
                    {
                        var geniusClient = new GeniusClient("mDfybGWKRSP2NomhX4a6cAKBsEdI-mFgy_BmmSLyHsIVowWREi_bcEvcf3u8cy59");
                        var result = await geniusClient.SearchClient.Search(Genius.Models.TextFormat.Plain, name);
                        
                    }
                }
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
