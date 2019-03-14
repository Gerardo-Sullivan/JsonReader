using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonReader
{
    public enum EntityType
    {
        None = 0,
        BuildingCode
    }

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
        private static readonly string _buildingCode = "Building.Code";

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("intentName")]
        public string Intent { get; set; }

        [JsonProperty("entityLabels")]
        public List<EntityLabel> Entities { get; set; }

        public Utterance(string text, string intent)
        {
            Text = text;
            Intent = intent;
            Entities = new List<EntityLabel>();
        }

        public Utterance(string prefix, string entity, string intent, string suffix = "", EntityType entityType = EntityType.None)
        {
            Text = prefix + " " + entity + suffix;
            Intent = intent;
            Entities = new List<EntityLabel>();

            if (entityType == EntityType.BuildingCode)
            {
                Entities.Add(new EntityLabel(_buildingCode, prefix.Length + 1, Text.Length - 1));
            }
        }
    }

    public class EntityLabel
    {
        [JsonProperty("entityName")]
        public string EntityName { get; set; }

        [JsonProperty("startCharIndex")]
        public int StartCharIndex { get; set; }

        [JsonProperty("endCharIndex")]
        public int EndCharIndex { get; set; }

        public EntityLabel(string entityName, int startCharIndex, int endCharIndex)
        {
            EntityName = entityName;
            StartCharIndex = startCharIndex;
            EndCharIndex = endCharIndex;
        }
    }
}