using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonReader
{
    [Serializable]
    public class LuisApplication
    {
        [JsonProperty("luis_schema_version")]
        public string LuisSchemaVersion { get; set; }
        [JsonProperty("versonId")]
        public string VersionId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("desc")]
        public string Description { get; set; }
        [JsonProperty("culture")]
        public string Culture { get; set; }
        [JsonProperty("tokenizerVersion")]
        public string TokenizerVersion { get; set; }
        [JsonProperty("intents")]
        public List<Intent> Intents { get; set; }
        [JsonProperty("utterances")]
        public List<Utterance> Utterances { get; set; }

        public LuisApplication()
        {
            Intents = new List<Intent>();
            Utterances = new List<Utterance>();
        }
    }

    public class Intent
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public Intent(string name)
        {
            Name = name;
        }
    }

    public class Utterance
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("intent")]
        public string Intent { get; set; }
        [JsonProperty("entities")]
        public List<string> Entities { get; set; }

        public Utterance(string text, string intent)
        {
            Text = text;
            Intent = intent;
            Entities = new List<string>();
        }
    }

}
