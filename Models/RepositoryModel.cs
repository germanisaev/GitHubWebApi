using Newtonsoft.Json;

namespace PhoenixAPI.Models;

public class RepositoryModel
{
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("incomplete_results")]
        public bool IncompleteResults { get; set; }

        [JsonProperty("items")]
        public List<ItemsModel> Items { get; set; } = new List<ItemsModel>();
}
