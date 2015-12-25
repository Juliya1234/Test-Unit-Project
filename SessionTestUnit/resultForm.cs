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
        private int rigth;
        private List<AnsweredQuestion> hash;
        private List<string> swear_100;
        private List<string> swear_99_90;
        private List<string> swear_89_75;
        private List<string> swear_74_50;
        private List<string> swear_49;
        public resultForm(int rigth, List<AnsweredQuestion> hash)
        {
            InitializeComponent();
            this.rigth = rigth;
            this.hash = hash;
        }

        private void resultForm_Load(object sender, EventArgs e)
        {
            initiate_swears();
            if (rigth > 0)
            {
                double result = (double)rigth / hash.Count;
                result = result * 100;
                label1.Text = "Ваш результат: " + result.ToString("#.##") + "%";
                label2.Text = get_words(result);
            }
            else
            {
                label1.Text = "Ваш результат: 0%";
                label2.Text = get_words(0);
            }
            create_labels(hash);
            //----------------------------------------
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
            if (res == 100)
                return "Молодец! Так держать! Эта сессия - ничто для тебя!";
            if ((res >= 90) && (res < 100))
                return "Круто! Еще немного подготовки - и стольник тебе обеспечен!";
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
            if (res == 100)
                return swear_100[new Random().Next(swear_100.Count)];
            if ((res >= 90) && (res < 100))
                return swear_99_90[new Random().Next(swear_99_90.Count)];
            if ((res >= 75) && (res < 90))
                return swear_89_75[new Random().Next(swear_89_75.Count)];
            if ((res >= 50) && (res < 75))
                return swear_74_50[new Random().Next(swear_74_50.Count)];
            return swear_49[new Random().Next(swear_49.Count)];
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
                label.Font = new Font("Segoe UI", 10.2F, 
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
        private void initiate_swears()
        {
            swear_100 = new List<string>(0);
            swear_100.Add("Возьми с полки пирожок, долбанный очкарик");
            swear_100.Add("Тебе мозги не жмут черепушку?");
            swear_100.Add("Батя - повелитель белого трона, маманя смотрит Рен-ТВ. И в кого ты такой умный...");
            swear_100.Add("В церковь сходи, бес в тебе сидит, от лукавого это все");
            swear_100.Add("Ублюдок, мать твою! А ну иди сюда, говно собачье! А? Сдуру решил на стольник написать?");
            //---------------------------------
            swear_99_90 = new List<string>(0);
            swear_99_90.Add("Если такой головастый, то что на стольник не пишешь?");
            swear_99_90.Add("У тебя личная жизнь то есть, законченный ботаник?");
            swear_99_90.Add("Тут должна была быть запись о том, что ты молодец, но ты будешь послан этой программой!");
            swear_99_90.Add("Ну ничего страшного! Ты ж не даун на стольники писать.");
            swear_99_90.Add("Опять не 100? Ты в который раз уже пишешь тест, а все так и не научился ничему");
            //---------------------------------
            swear_89_75 = new List<string>(0);
            swear_89_75.Add("Даже твоя толстая мамаша может лучше");
            swear_89_75.Add("Серьезно? Почему ты не делаешь на 5? Система тестирования легкая ведь!");
            swear_89_75.Add("Ты разве не в курсе, что этот тест расчитан на второй класс?");
            swear_89_75.Add("Ты пишешь тесты, как маленькая девочка");
            swear_89_75.Add("Люк - сын Вейдара, а ты - соседа по лестничной площадке");
            //---------------------------------
            swear_74_50 = new List<string>(0);
            swear_74_50.Add("Ну и что тут такого? Да любой бомжара может написать тест лучше тебя");
            swear_89_75.Add("Твои родители умные люди, раз заставляют тебя учиться в универе. А ты... Ты, наверное, приемный.");
            swear_74_50.Add("Утешай себя, что и Эйнштейн учился на двойки");
            swear_74_50.Add("Помни, что это - последняя грань. Дальше только аутизм");
            swear_74_50.Add("Ты понимаешь, что это начало конца?");
            //--------------------------------
            swear_49 = new List<string>(0);
            swear_49.Add("У меня нет слов.. Это просто невозможно");
            swear_49.Add("Знаешь, я как раз искал недоразвитых для социальной рекламы...");
            swear_49.Add("Эту надпись видят только аутисты, которые не могут даже в проходной");
            swear_49.Add("Если бы твой мозг стал планетой, то ее бы не было");
            swear_49.Add("Зачем тебе учеба? Лучше мемасики в интернетике погляди да картиночки полайкай");
        }
    }
}
