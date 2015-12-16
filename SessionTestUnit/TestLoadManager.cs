using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionTestUnit
{
    public class TestLoadManager
    {
        private List<TestQuestion> list;
        public TestLoadManager()
        {
            list = new List<TestQuestion>(0);
        }

        public void set_source_list(string text)  
        {
            list = new List<TestQuestion>(0);
            int count = (text.Length - text.Replace("<question>", "").Length) / "<question>".Length;
            for (int i = 0; i < count; i++)
            {
                //------------------------------------------------------
                text = text.Substring(10);
                string sub_text;
                int end_pos = text.IndexOf("<question>");
                if (end_pos > -1)
                    sub_text = text.Substring(0, text.IndexOf("<question>"));
                else 
                    sub_text = text.Substring(0, text.Length);

                string quest = sub_text.Substring(0, text.IndexOf("\r\n"));
                sub_text = sub_text.Substring(text.IndexOf("<variant>"));
                var hash = new List<string>(0);
                for (int j = 0; j < 5; j++)
                {
                    sub_text = sub_text.Substring(9);
                    int pos = sub_text.IndexOf("\r\n");
                    string var = sub_text.Substring(0, pos);
                    hash.Add( var );
                    sub_text = sub_text.Substring(pos+2);
                }
                var question = new TestQuestion();
                question.question = quest;
                question.variant_1 = hash[0];
                question.variant_2 = hash[1];
                question.variant_3 = hash[2];
                question.variant_4 = hash[3];
                question.variant_5 = hash[4];
                list.Add(question);
                if (end_pos > -1)
                    text = text.Substring(end_pos);
            }           
        }

        public TestQuestion get_next()
        {
            if (list.Count > 0)
            {
                var random = new Random();
                var question = list[random.Next(list.Count)];
                list.Remove(question);
                return question;
            }
            return null;
        }
        
        public int get_count()
        {
            
            return list.Count;
        }
    }
}
