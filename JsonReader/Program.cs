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


        public static void Main(string[] args)
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
