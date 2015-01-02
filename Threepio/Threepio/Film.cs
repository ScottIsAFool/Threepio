using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Threepio
{
    public class Film : Item
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }
        [JsonProperty("Opening_Crawl")]
        public string Crawl { get; set; }
        public List<int> Characters { get; private set; }
        public List<int> Planets { get; set; }

        public List<int> Species { get; set; }

        public List<int> Starships { get; set; }

        public List<int> Vehicles { get; set; }
        [JsonProperty("episode_id")]
        public int Episode { get; set; }

        public Film()
        {
            Characters = new List<int>();
            Planets = new List<int>();
            Species = new List<int>();
            Starships = new List<int>();
            Vehicles = new List<int>();
        }

        public static async Task<Film> Get(int id)
        {
            return await GetInternal<Film>(id, "films");
        }

        public override void ExtractIds()
        {
            foreach (Uri characterUri in CharacterUris)
            {
                Characters.Add(extractId(characterUri));
            }
            foreach (Uri planetUri in PlanetUris)
            {
                Planets.Add(extractId(planetUri));
            }
            foreach (Uri speciesUri in SpeciesUris)
            {
                Species.Add(extractId(speciesUri));
            }
            foreach (Uri starshipUri in StarshipUris)
            {
                Starships.Add(extractId(starshipUri));
            }
            foreach (Uri vehicleUri in VehicleUris)
            {
                Vehicles.Add(extractId(vehicleUri));
            }
        }

        public static async Task<List<Film>> GetPage(int pageNumber = 1)
        {
            return await GetPageInternal<Film>("films", pageNumber);
        }

        // Convenience methods to find the individual films
        public static Task<Film> Episode1()
        {
            return Get(4);
        }

        public static Task<Film> Episode2()
        {
            return Get(5);
        }

        public static Task<Film> Episode3()
        {
            return Get(6);
        }

        public static Task<Film> Episode4()
        {
            return Get(1);
        }

        public static Task<Film> Episode5()
        {
            return Get(2);
        }

        public static Task<Film> Episode6()
        {
            return Get(3);
        }
    }
}
