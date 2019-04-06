using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryDatabase.Database
{
    public class History:EntityBase
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Link { get; set; }
        public DateTime ListenedAt { get; set; } = DateTime.Now;
    }
}
