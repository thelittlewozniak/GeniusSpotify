using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeniusSpotify.HistoryWork
{
    public class History
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Link { get; set; }
        public DateTime ListenedAt { get; set; } = DateTime.Now;
    }
}
