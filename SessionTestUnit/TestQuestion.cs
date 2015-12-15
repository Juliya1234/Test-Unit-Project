using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionTestUnit
{
    public class TestQuestion
    {
        public string question { get; set; }
        public string variant_1 { get; set; }
        public string variant_2 { get; set; }
        public string variant_3 { get; set; }
        public string variant_4 { get; set; }
        public string variant_5 { get; set; }
        public TestQuestion()
        {

        }
        public TestQuestion(string question, 
                    string var1, 
                    string var2, 
                    string var3, 
                    string var4, 
                    string var5)
        {
            this.question = question;
            variant_1 = var1;
            variant_2 = var2;
            variant_3 = var3;
            variant_4 = var4;
            variant_5 = var5;
        }
    }
}
