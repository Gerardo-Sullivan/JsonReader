using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace JsonReader
{
    [Serializable]
    public class AUTBuilding
    {
        [JsonProperty("Building_Id")]
        public int BuildingId { get; set; }

        [JsonProperty("Campus_Id")]
        public int CampusId { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Branding_Name")]
        public string BrandingName { get; set; }

        [JsonProperty("Other_Details")]
        public string OtherDetails { get; set; }
    }
}