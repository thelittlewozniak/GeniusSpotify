using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace GeniusSpotify.HistoryWork
{
    public class Histories
    {
        public List<History> HistoriesList { get; set; }
        private static Histories _instance;

        public static Histories Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Histories();
                return _instance;
            }
        }
        public Histories()
        {
            HistoriesList = JsonConvert.DeserializeObject<List<History>>(Properties.Settings.Default.Histories);
            if (HistoriesList == null)
                HistoriesList = new List<History>();
        }
        public void AddHistory(string title, string artist, string url) => HistoriesList.Add(new History { Title = title, Artist = artist, Link = url, ListenedAt = DateTime.Now });
        public void Save() => Properties.Settings.Default.Histories = JsonConvert.SerializeObject(HistoriesList);
    }
}
