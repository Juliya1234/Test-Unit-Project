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
        private List<AnsweredQuestion> hash;
        public resultForm(int count, int rigth, List<AnsweredQuestion> hash)
        {
            InitializeComponent();
            this.count = count;
            this.rigth = rigth;
            this.hash = hash;
        }

        private void resultForm_Load(object sender, EventArgs e)
        {
            double result = (double)rigth / count;
            result = result * 100;
            label2.Text = result.ToString("#.##") + "%\r\n" + get_swearing(result);
            create_labels(hash);
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
        private void create_labels(List<AnsweredQuestion> hash)
        {
            int start_location_y = label2.Location.Y + label2.Height + 10;
            foreach(AnsweredQuestion q in hash)
            {
                var label = new Label();
                if (q.rigth == q.was_choose)
                {
                    label.Text = "Вопрос №" + q.pos +
                             "\r\nВопрос: " + q.question +
                             "\r\nПравильный ответ: " + q.rigth +
                             "\r\nОтвечен верно";
                    label.BackColor = Color.FromArgb(153, 230, 153);
                }
                else
                {
                    label.Text = "Вопрос №" + q.pos +
                             "\r\nВопрос: " + q.question +
                             "\r\nПравильный ответ: " + q.rigth +
                             "\r\nОтвет пользователя: " + q.was_choose;
                    label.BackColor = Color.FromArgb(255, 128, 128);
                }
                label.Location = new Point(label2.Location.X, start_location_y);
                label.Font = new Font("HelveticaNeueCyr", 10.2F, 
                    FontStyle.Regular, GraphicsUnit.Point, 204);
                label.AutoSize = false;
                label.Height = 95;
                label.Width = this.Width - 40;
                this.Controls.Add(label);
                start_location_y += label.Height + 10;
            }
        }
    }
}
