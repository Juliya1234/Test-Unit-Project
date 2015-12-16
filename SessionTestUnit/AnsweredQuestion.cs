using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionTestUnit
{
    public class AnsweredQuestion
    {
        public int pos; 
        public string question { get; set; }
        public string rigth { get; set; }
        public string was_choose { get; set; }
        public AnsweredQuestion(int pos, string question, string rigth, string was_choose)
        {
            this.pos = pos;
            this.question = question;
            this.rigth = rigth;
            this.was_choose = was_choose;
        }
        public AnsweredQuestion()
        {
            this.pos = -1;
            this.question = "";
            this.rigth = "";
            this.was_choose = "";
        }
    }
}
