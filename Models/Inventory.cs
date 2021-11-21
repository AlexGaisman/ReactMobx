using Newtonsoft.Json;

namespace Tracker.Models
{
    public class Inventory
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("itemNumber")]
        public int ItemNumber { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("unitPrice")]
        public string UnitPrice { get; set; }

    }
}
