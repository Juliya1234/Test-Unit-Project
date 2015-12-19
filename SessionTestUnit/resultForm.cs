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
        public resultForm(int rigth, List<AnsweredQuestion> hash)
        {
            InitializeComponent();
            this.rigth = rigth;
            this.hash = hash;
        }

        private void resultForm_Load(object sender, EventArgs e)
        {
            if (rigth > 0)
            {
                double result = (double)rigth / hash.Count;
                result = result * 100;
                label2.Text = result.ToString("#.##") + "%\r\n" + get_words(result);
            }
            else
                label2.Text = "0 %\r\n" + get_words(0);
            create_labels(hash);
        }
        private string get_words(double res)
        {
            var settings = new SettingsManager().Load();
            if (settings.show_swearing)
                return get_swearing(res);
            else
                return get_good_words(res);
        }
        private string get_good_words(double res)
        {
            if (res >= 90)
                return "Круто";
            if ((res >= 75) && (res < 90))
                return "Я знаю, ты можешь лучше";
            if ((res >= 60) && (res < 75))
                return "Ну ничего, в другой раз повезет";
            if ((res >= 50) && (res < 60))
                return "Неудачный день?";
            return "Ну как же так? Это ведь легкий тест";
        }
        private string get_swearing(double res)
        {
            if (res >= 90)
                return "Долбанный очкарик";
            if ((res >= 75) && (res < 90))
                return "Даже твоя жирная мамаша может лучше";
            if ((res >= 60) && (res < 75))
                return "Ты что, в доту переиграл? \r\nДаже рак на миде лучше тащит";
            if ((res >= 50) && (res < 60))
                return "Ну просто я не знаю. \r\nПопробуй обследоваться на предмет аутизма";
            return "Да люди с синдромом Дауна лучше пишут тесты";
        }
        private void create_labels(List<AnsweredQuestion> hash)
        {
            int start_location_y = label2.Location.Y + label2.Height + 10;
            int count = 1;
            foreach(AnsweredQuestion q in hash)
            {
                var label = new Label();
                if (q.rigth == q.was_choose)
                {
                    label.Text = "Вопрос №" + count +
                             "\r\nВопрос: " + q.question +
                             "\r\nПравильный ответ: " + q.rigth +
                             "\r\nОтвечен верно";
                    label.BackColor = Color.FromArgb(153, 230, 153);
                }
                else
                {
                    label.Text = "Вопрос №" + count +
                             "\r\nВопрос: " + q.question +
                             "\r\nПравильный ответ: " + q.rigth +
                             "\r\nОтвет пользователя: " + q.was_choose;
                    label.BackColor = Color.FromArgb(255, 128, 128);
                }
                label.Location = new Point(label2.Location.X, start_location_y);
                label.Font = new Font("HelveticaNeueCyr", 10.2F, 
                    FontStyle.Regular, GraphicsUnit.Point, 204);
                label.AutoSize = false;
                label.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                label.Height = 115;
                label.Width = label2.Width;
                this.Controls.Add(label);
                start_location_y += label.Height + 10;
                count += 1;
            }
        }
    }
}
