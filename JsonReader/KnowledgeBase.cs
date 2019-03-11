using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonReader
{
    [Serializable]
    public class KnowledgeBase
    {

        public string Name { get; set; }

        public List<QnA> Questions { get; set; }

        public KnowledgeBase()
        {
            Questions = new List<QnA>();
        }
    }

    public class QnA
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Source { get; set; }
        public string Metadata { get; set; }
        public string SuggestedQuestions { get; set; }
    }
}
