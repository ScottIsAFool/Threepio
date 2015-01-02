using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Threepio
{
    public class Species : Item
    {
        public string Name { get; set; }
        public string Classification { get; set; }
        public string Designation { get; set; }
        public string Language { get; set; }
        [JsonIgnore]
        public int Homeworld { get; set; }
        [JsonProperty("Homeworld")]
        public Uri HomeworldUri { get; set; }
        public List<int> Films { get; set; }
        public List<int> Members { get; set; }
        [JsonProperty("average_height")]
        public string AverageHeight { get; set; }
        [JsonProperty("average_lifespan")]
        public string AverageLifespan { get; set; }
        [JsonProperty("eye_colors")]
        public string EyeColours { get; set; }
        [JsonProperty("skin_colors")]
        public string SkinColours { get; set; }

        public Species()
        {
            Films = new List<int>();
            Members = new List<int>();
        }

        public static async Task<Species> Get(int id)
        {
            var species = await GetInternal<Species>(id, "species");

            species.Homeworld = extractId(species.HomeworldUri);

            return species;
        }

        public override void ExtractIds()
        {
            foreach (Uri filmUri in FilmUris)
            {
                Films.Add(extractId(filmUri));
            }
            foreach (Uri memberUri in MemberUris)
            {
                Members.Add(extractId(memberUri));
            }
        }

        public static async Task<List<Species>> GetPage(int pageNumber = 1)
        {
            return await GetPageInternal<Species>("species", pageNumber);
        }
    }
}
