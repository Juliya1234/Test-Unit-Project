using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SessionTestUnit
{
    public partial class resultForm : Form
    {
        private int count;
        private int rigth;
        public resultForm(int count, int rigth)
        {
            InitializeComponent();
            this.count = count;
            this.rigth = rigth;
        }

        private void resultForm_Load(object sender, EventArgs e)
        {
            double result = (double)rigth / count;
            result = result * 100;
            label2.Text = result.ToString("#.##") + "%\r\n" + get_swearing(result);
        }
        private string get_swearing(double res)
        {
            if (res >= 90)
                return "Круто, долбанный ботаник";
            if ((res >= 75) && (res < 90))
                return "Слабовато.\r\nДаже твоя жирная мамаша может лучше";
            if ((res >= 60) && (res < 75))
                return "Да ты шутишь?\r\nАутист с 4 классами образования смог бы лучше";
            if ((res >= 50) && (res < 60))
                return "Хуже уже быть не может, дубина";
            return "Просто нет слов";
        }
    }
}
