using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.IO;

namespace JsonReader
{
    public class Program
    {
        private static readonly string _blackBoardFile = @"C:\Users\Gerardo\Downloads\KB\JSON\Blackboard.json";
        private static readonly string _courseInformationFile = @"C:\Users\Gerardo\Downloads\KB\JSON\CourseInformation.json";
        private static readonly string _doc = @"C:\Users\Gerardo\Downloads\KB\JSON\DOC.json";
        private static readonly string _foundation = @"C:\Users\Gerardo\Downloads\KB\JSON\Foundation.json";
        private static readonly string _graduation = @"C:\Users\Gerardo\Downloads\KB\JSON\Graduation.json";
        private static readonly string _ict = @"C:\Users\Gerardo\Downloads\KB\JSON\ICT.json";
        private static readonly string _kiaOra = @"C:\Users\Gerardo\Downloads\KB\JSON\KiaOra.json";
        private static readonly string _library = @"C:\Users\Gerardo\Downloads\KB\JSON\Library.json";
        private static readonly string _orientation = @"C:\Users\Gerardo\Downloads\KB\JSON\Orientation.json";
        private static readonly string _autBuildings = @"C:\Users\em12664\Documents\TUA\Building.Code.json";
        private static readonly string _utterances = "Location";

        public static void Main(string[] args)
        {
            CreateAutBuildingsJson();
        }

        private static void CreateAutBuildingsJson()
        {
            List<EntityValues> entityValues = new List<EntityValues>();
            List<Utterance> utterances = new List<Utterance>();
            var autBuildings = JsonConvert.DeserializeObject<List<AUTBuilding>>(File.ReadAllText(_autBuildings));

            foreach (AUTBuilding building in autBuildings)
            {
                entityValues.Add(new EntityValues(building.Code, building.Code, building.Name, building.BrandingName));
                if (!string.IsNullOrWhiteSpace(building.Code))
                {
                    utterances.Add(new Utterance("Where is the", building.Code, _utterances, " building", EntityType.BuildingCode));
                    utterances.Add(new Utterance("Where is", building.Code, _utterances, entityType: EntityType.BuildingCode));
                }
                if (!string.IsNullOrWhiteSpace(building.Name))
                {
                    utterances.Add(new Utterance("Where is the", building.Name, _utterances, entityType: EntityType.BuildingCode));
                    utterances.Add(new Utterance("Where is", building.Name, _utterances, entityType: EntityType.BuildingCode));
                    if (building.Name.Contains("&"))
                    {
                        string buldingName = building.Name.Replace("&", "and");
                        utterances.Add(new Utterance("Where is the", buldingName, _utterances, entityType: EntityType.BuildingCode));
                        utterances.Add(new Utterance("Where is", buldingName, _utterances, entityType: EntityType.BuildingCode));
                    }
                    if (building.Name.Contains("+"))
                    {
                        string buldingName = building.Name.Replace("+", "and");
                        utterances.Add(new Utterance("Where is the", buldingName, _utterances, entityType: EntityType.BuildingCode));
                        utterances.Add(new Utterance("Where is", buldingName, _utterances, entityType: EntityType.BuildingCode));
                    }
                }
                if (!string.IsNullOrWhiteSpace(building.BrandingName))
                {
                    utterances.Add(new Utterance("Where is the", building.BrandingName, _utterances, " building", EntityType.BuildingCode));
                    utterances.Add(new Utterance("Where is", building.BrandingName, _utterances, entityType: EntityType.BuildingCode));
                }
            }

            var utterancesFirst99 = utterances.Take(99).ToList();
            var utterancesSkip99 = utterances.Skip(99).ToList();
            File.WriteAllText("TuaLuisFirst99Utterances.json", JsonConvert.SerializeObject(utterancesFirst99, Formatting.Indented));
            File.WriteAllText("TuaLuisSkip99Utterances.json", JsonConvert.SerializeObject(utterancesSkip99, Formatting.Indented));
            File.WriteAllText("TUA_LUIS_BuildingCode.json", JsonConvert.SerializeObject(entityValues, Formatting.Indented));
        }

        private static void CreateLuisJson()
        {
            LuisApplication application = new LuisApplication();

            List<KnowledgeBase> knowledgeBases = new List<KnowledgeBase>
            {
                JsonConvert.DeserializeObject<KnowledgeBase>(File.ReadAllText(_blackBoardFile)),
                JsonConvert.DeserializeObject<KnowledgeBase>(File.ReadAllText(_courseInformationFile)),
                JsonConvert.DeserializeObject<KnowledgeBase>(File.ReadAllText(_doc)),
                JsonConvert.DeserializeObject<KnowledgeBase>(File.ReadAllText(_foundation)),
                JsonConvert.DeserializeObject<KnowledgeBase>(File.ReadAllText(_graduation)),
                JsonConvert.DeserializeObject<KnowledgeBase>(File.ReadAllText(_ict)),
                JsonConvert.DeserializeObject<KnowledgeBase>(File.ReadAllText(_kiaOra)),
                JsonConvert.DeserializeObject<KnowledgeBase>(File.ReadAllText(_library)),
                JsonConvert.DeserializeObject<KnowledgeBase>(File.ReadAllText(_orientation))
            };

            foreach (KnowledgeBase kb in knowledgeBases)
            {
                application.Intents.Add(new Intent(kb.Name));
                foreach (QnA q in kb.Questions)
                {
                    application.Utterances.Add(new Utterance(q.Question, kb.Name));
                }
            }

            File.WriteAllText("TUA_LUIS_App.json", JsonConvert.SerializeObject(application, Formatting.Indented));
        }
    }
}