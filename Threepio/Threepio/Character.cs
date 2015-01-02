using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Threepio
{
    public class Character : Item
    {
        public string Name { get; set; }
        [JsonIgnore]
        public int Homeworld { get; set; }
        [JsonProperty("Homeworld")]
        public Uri HomeworldUri { get; set; }
        public List<int> Films { get; set; }
        public List<int> Species { get; set; }
        public List<int> Ships { get; set; }
        public List<int> Vehicles { get; set; }
        [JsonProperty("birth_year")]
        public string BirthYear { get; set; }

        public string Height { get; set; }
        public string Mass { get; set; }
        [JsonProperty("skin_color")]
        public string SkinColour { get; set; }
        [JsonProperty("hair_color")]
        public string HairColour { get; set; }
        [JsonProperty("eye_color")]
        public string EyeColour { get; set; }

        public string Gender { get; set; }

        public Character()
        {
            Films = new List<int>();
            Species = new List<int>();
            Ships = new List<int>();
            Vehicles = new List<int>();
        }

        public static async Task<Character> Get(int id)
        {
            return await GetInternal<Character>(id, "people");
        }

        public override void ExtractIds()
        {
            foreach (Uri filmUri in FilmUris)
            {
                Films.Add(extractId(filmUri));
            }
            foreach (Uri speciesUri in SpeciesUris)
            {
                Species.Add(extractId(speciesUri));
            }
            foreach (Uri shipUri in StarshipUris)
            {
                Ships.Add(extractId(shipUri));
            }
            foreach (Uri vehicleUri in VehicleUris)
            {
                Vehicles.Add(extractId(vehicleUri));
            }

            Homeworld = extractId(HomeworldUri);
        }

        public static async Task<List<Character>> GetPage(int pageNumber = 1)
        {
            return await GetPageInternal<Character>("people", pageNumber);
        }
    }
}
