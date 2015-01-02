using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Threepio
{
    public abstract class Item 
    {
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }

        [JsonProperty("films")]
        internal List<Uri> FilmUris { get; set; }
        [JsonProperty("species")]
        internal List<Uri> SpeciesUris { get; set; }
        [JsonProperty("starships")]
        internal List<Uri> StarshipUris { get; set; }
        [JsonProperty("vehicles")]
        internal List<Uri> VehicleUris { get; set; }
        [JsonProperty("planets")]
        internal List<Uri> PlanetUris { get; private set; }
        [JsonProperty("characters")]
        internal List<Uri> CharacterUris { get; private set; }
        [JsonProperty("residents")]
        internal List<Uri> ResidentUris { get; set; }
        [JsonProperty("people")]
        internal List<Uri> MemberUris { get; set; }
        [JsonProperty("pilots")]
        internal List<Uri> PilotUris { get; set; }


        internal static int extractId(Uri uri)
        {
            string link = uri.AbsoluteUri;

            link = link.Replace(Settings.RootUrl, string.Empty);
            link = link.Replace("/", string.Empty);
            link = link.Replace("films", string.Empty);
            link = link.Replace("people", string.Empty);
            link = link.Replace("species", string.Empty);
            link = link.Replace("planets", string.Empty);
            link = link.Replace("starships", string.Empty);
            link = link.Replace("vehicles", string.Empty);

            int result;
            int.TryParse(link, out result);

            return result;
        }

        public virtual void ExtractIds() { }

        internal static async Task<List<T>> GetPageInternal<T>(string endpoint, int pageNumber = 1) where T : Item
        {
            string data;
            using (HttpClient client = WebClientFactory.GetClient())
            {
                var response = await client.GetAsync(string.Format("{0}/{1}/?page={2}", Settings.RootUrl, endpoint, pageNumber));
                response.EnsureSuccessStatusCode();
                data = await response.Content.ReadAsStringAsync();
            }
            List<T> items = JsonConvert.DeserializeObject<BulkGet<T>>(data).items;

            foreach (T item in items)
            {
                item.ExtractIds();
            }
            return items;
        }

        internal static async Task<T> GetInternal<T>(int id, string endpoint) where T : Item
        {
            string data;
            using (HttpClient client = WebClientFactory.GetClient())
            {
                data = await client.GetStringAsync(string.Format("{0}/{1}/{2}/", Settings.RootUrl, endpoint, id));
            }
            
            T item = JsonConvert.DeserializeObject<T>(data);
            
            item.ExtractIds();

            return item;
        }
    }
}
