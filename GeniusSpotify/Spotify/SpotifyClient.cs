using Spotify.Exception;
using System.Diagnostics;
using System.Linq;

namespace Spotify
{
    public class SpotifyClient:ISpotify
    {
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
