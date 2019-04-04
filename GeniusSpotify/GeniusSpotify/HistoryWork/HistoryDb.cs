namespace GeniusSpotify.HistoryWork
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class HistoryDb : DbContext
    {
        public DbSet<History> Histories { get; set; }
        public HistoryDb()
            : base("name=HistoryDb")
        {
        }
    }
}

