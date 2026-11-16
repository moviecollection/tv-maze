using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieCollection.TVMaze.Models
{
    public class Embedded
    {
        [JsonProperty("episodes")]
        public IEnumerable<Episode> Episodes { get; set; }

        [JsonProperty("show")]
        public Show Show { get; set; }

        [JsonProperty("character")]
        public Character Character { get; set; }
    }
}
