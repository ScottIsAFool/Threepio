using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Threepio
{
    public class Vehicle : Item
    {
        public string Name { get; set; }
        public string Model { get; set; }
        [JsonProperty("vehicle_class")]
        public string Class { get; set; }
        [JsonProperty("cost_in_credits")]
        public string Cost { get; set; }
        public string Length { get; set; }
        public int Crew { get; set; }
        [JsonProperty("passengers")]
        public string PassengerCapacity { get; set; }
        [JsonProperty("cargo_capacity")]
        public string CargoCapacity { get; set; }
        public List<int> Films { get; set; }
        public List<int> Pilots { get; set; }
        public string Manufacturer { get; set; }

        public Vehicle()
        {
            Films = new List<int>();
            Pilots = new List<int>();
        }

        public static async Task<Vehicle> Get(int id)
        {
            return await GetInternal<Vehicle>(id, "vehicles");
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

        public static async Task<List<Vehicle>> GetPage(int pageNumber = 1)
        {
            return await GetPageInternal<Vehicle>("vehicles", pageNumber);
        }
    }
}
