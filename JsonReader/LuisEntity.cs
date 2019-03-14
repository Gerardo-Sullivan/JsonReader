using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace JsonReader
{
    [Serializable]
    public class EntityValues
    {
        [JsonProperty("canonicalForm")]
        public string CanonicalForm { get; set; }

        [JsonProperty("list")]
        public List<string> List { get; set; }

        public EntityValues(string canonicalForm, params string[] list)
        {
            CanonicalForm = canonicalForm;
            List = new List<string>();
            foreach (string item in list)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    List.Add(item);
                }
            }
        }
    }
}