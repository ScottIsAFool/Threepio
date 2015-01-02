using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Threepio
{
    public class Planet : Item
    {
        public string Name { get; set; }
        public int Diameter { get; set; }
        public string Climate { get; set; }
        [JsonProperty("orbital_period")]
        public string OrbitalPeriod { get; set; }
        public string Population { get; set; }
        [JsonProperty("rotation_period")]
        public string RotationPeriod { get; set; }
        [JsonProperty("surface_water")]
        public string SurfaceWaterPercentage { get; set; }
        public string Terrain { get; set; }
        public List<int> Films { get; set; }
        public List<int> Residents { get; set; }

        public Planet()
        {
            Films = new List<int>();
            Residents = new List<int>();
        }

        public static async Task<Planet> Get(int id)
        {
            return await GetInternal<Planet>(id, "planets");
        }

        public override void ExtractIds()
        {
            foreach (Uri filmUri in FilmUris)
            {
                Films.Add(extractId(filmUri));
            }
            foreach (Uri residentUri in ResidentUris)
            {
                Residents.Add(extractId(residentUri));
            }
        }

        public static async Task<List<Planet>> GetPage(int pageNumber = 1)
        {
            return await GetPageInternal<Planet>("planets", pageNumber);
        }
    }
}
