using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Threepio
{
    public class Starship : Item
    {
        public string Name { get; set; }
        public string Model { get; set; }
        [JsonProperty("starship_class")]
        public string Class { get; set; }
        [JsonProperty("cost_in_credits")]
        public string Cost { get; set; }
        public float Length { get; set; }
        public string Crew { get; set; }
        [JsonProperty("passengers")]
        public string PassengerCapacity { get; set; }
        [JsonProperty("hyperdrive_rating")]
        public string HyperdriveRating { get; set; }
        [JsonProperty("cargo_capacity")]
        public string CargoCapacity { get; set; }
        public List<int> Films { get; set; }
        public List<int> Pilots { get; set; }
        public string Consumables { get; set; }
        public string Manufacturer { get; set; }

        public Starship()
        {
            Films = new List<int>();
            Pilots = new List<int>();
        }
        public static async Task<Starship> Get(int id)
        {
            return await GetInternal<Starship>(id, "starships");
        }

        public override void ExtractIds()
        {
            foreach (Uri filmUri in FilmUris)
            {
                Films.Add(extractId(filmUri));
            }
            foreach (Uri pilotUri in PilotUris)
            {
                Pilots.Add(extractId(pilotUri));
            }
        }

        public static async Task<List<Starship>> GetPage(int pageNumber = 1)
        {
            return await GetPageInternal<Starship>("starships", pageNumber);
        }
    }
}
