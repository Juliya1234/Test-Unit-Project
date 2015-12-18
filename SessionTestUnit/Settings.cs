using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionTestUnit
{
    public class Settings
    {
        public bool questions_limit { get;set; }
        public bool show_rigth { get; set; }
        public Settings()
        {
            questions_limit = false;
            show_rigth = true;
        }
    }
}
