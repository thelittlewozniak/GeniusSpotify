using Genius.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Genius
{
    public class GeniusClient:IGenius
    {
        private HttpClient httpClient;
        private readonly string token;
        public GeniusClient(string token)
        {
            httpClient = new HttpClient();
            this.token = token;
        }
        public async Task<Search> SearchSong(string name)
        {
            name = name.Trim();
            name = name.Replace(" ", "%20");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var result = await httpClient.GetAsync("https://api.genius.com/search?q=" + name);
            result.EnsureSuccessStatusCode();
            string jsonBody = await result.Content.ReadAsStringAsync();
            var resultBody = JsonConvert.DeserializeObject<Search>(jsonBody);
            return resultBody;
        }
    }
}
