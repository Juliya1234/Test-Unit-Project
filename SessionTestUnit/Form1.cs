using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SessionTestUnit.QuestionClasses;
using SessionTestUnit.SettingsHelpers;

namespace SessionTestUnit
{
    public partial class Form1 : Form
    {
        private QuestionManager _manager;
        private Question _currentQuestion;
        private List<AnsweredQuestion> _answered;
        private int count = 0;
        private int rigth_checked = 0;
        private bool loaded_file = false;
        private string file_name = "";
        //-------------------------
        private Settings settings;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Platonus Тестер";
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            _manager = new QuestionManager();

        }
        private void load_source_file(string filename)
        {
            var reader = new StreamReader(filename, Encoding.Default);
            string text = reader.ReadToEnd();
            reader.Close();
            //-----------------------------
            _manager.SetSourceList(text);
            if (settings != null)
                if (settings.questions_limit)
                    _manager.set_question_limit(25);
            //----------------------------
            _currentQuestion = _manager.get_next();
            count += 1;
            load_to_labels(_currentQuestion);
            label1.Text = "Осталось вопросов: " + _manager.GetCount();
            if (settings.show_swearing)
                label6.Text = "Включено отображение ругательств. Вы сами этого хотите";
            else
                label6.Text = "Ругательства выключены";


        }
        private void load_to_labels(Question test)
        {
            if (test == null) return;
            question_label.Text = test.question;
            var random = new Random();
            var hash = new List<string>(0)
            {
                test.variant_1,
                test.variant_2,
                test.variant_3,
                test.variant_4,
                test.variant_5
            };
            //-----------------
            variant_1.BackColor = Color.FromArgb(248, 248, 248);
            variant_2.BackColor = Color.FromArgb(248, 248, 248);
            variant_3.BackColor = Color.FromArgb(248, 248, 248);
            variant_4.BackColor = Color.FromArgb(248, 248, 248);
            variant_5.BackColor = Color.FromArgb(248, 248, 248);
            //--------------
            variant_1.Text = hash[random.Next(hash.Count)];
            hash.Remove(variant_1.Text);
            //------------------
            variant_2.Text = hash[random.Next(hash.Count)];
            hash.Remove(variant_2.Text);
            //---------------------------
            variant_3.Text = hash[random.Next(hash.Count)];
            hash.Remove(variant_3.Text);
            //---------------------------
            variant_4.Text = hash[random.Next(hash.Count)];
            hash.Remove(variant_4.Text);
            //---------------------------
            variant_5.Text = hash[random.Next(hash.Count)];
            hash.Remove(variant_5.Text);
            //---------------------------
        }

        private void load_settings()
        {
            settings = new SettingsManager().Load();
            count = 0;
            rigth_checked = 0;
            _answered = new List<AnsweredQuestion>(0);
            if (file_name != "")
                load_source_file(file_name);
            //----------------------------
            if ((_manager != null) && (_manager.GetCount() > 0))
            {
                label2.Text = "Файл загружен. Нажмите \"Начать\". Вопросов " 
                                + _manager.GetCount();
                loaded_file = true;
            }
            else
                label2.Text = @"Возникли проблемы с обработкой вопросов. Количество вопросов: " 
                                + _manager.GetCount();
            //--------------------------
            button3.Text = "Следующий вопрос";

        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenFile()
        {
            var openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            //--------------------------

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            //load_source_file(openFileDialog1.FileName);
            file_name = openFileDialog1.FileName;
            load_settings();
            button2.Text = "Начать тест";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            settings = new SettingsManager().Load();
            if (_answered.Count == _manager.get_first_list_count())
            {
                new resultForm(rigth_checked, _answered).Show();
            }
            else
            {
                check_question();
                _currentQuestion = _manager.get_next();
                load_to_labels(_currentQuestion);
                label1.Text = "Осталось вопросов: " + _manager.GetCount();
                if (settings.show_swearing)
                    label6.Text = "Включено отображение ругательств. Вы сами этого хотите";
                else
                    label6.Text = "Ругательства выключены";
                if (_answered.Count == _manager.get_first_list_count())
                    button3.Text = "Показать результат";
            }
        }
        private void paint_radiobutton(RadioButton sender, bool rigth)
        {
            if (rigth)
                sender.BackColor = Color.FromArgb(153, 230, 153);
            else 
                sender.BackColor = Color.FromArgb(255, 128, 128);
        }
        private void check_question()
        {
            var answer = new AnsweredQuestion();
            answer.question = _currentQuestion.question;
            answer.rigth = _currentQuestion.variant_1;
            //------------------------
            if (variant_1.Checked)
            {
                if (variant_1.Text == _currentQuestion.variant_1)
                    rigth_checked += 1;
                answer.was_choose = variant_1.Text;
            }
            //-----------------------------
            if (variant_2.Checked)
            {
                if (variant_2.Text == _currentQuestion.variant_1)
                    rigth_checked += 1;
                answer.was_choose = variant_2.Text;
            }
            //----------------------------
            if (variant_3.Checked)
            {
                if (variant_3.Text == _currentQuestion.variant_1)
                    rigth_checked += 1;
                answer.was_choose = variant_3.Text;
            }
            //---------------------
            if (variant_4.Checked)
            {
                if (variant_4.Text == _currentQuestion.variant_1)
                    rigth_checked += 1;
                answer.was_choose = variant_4.Text;
            }
            //----------------------
            if (variant_5.Checked)
            {
                if (variant_5.Text == _currentQuestion.variant_1)
                    rigth_checked += 1;
                answer.was_choose = variant_5.Text;
            }
            //----------------------------------
            
            _answered.Add(answer);

        }
        private void show_rigth()
        {
            if (variant_1.Text == _currentQuestion.variant_1)
                paint_radiobutton(variant_1, true);
            else
               if (variant_1.Checked)
                paint_radiobutton(variant_1, false);

            if (variant_2.Text == _currentQuestion.variant_1)
                paint_radiobutton(variant_2, true);
            else
               if (variant_2.Checked)
                paint_radiobutton(variant_2, false);

            if (variant_3.Text == _currentQuestion.variant_1)
                paint_radiobutton(variant_3, true);
            else
               if (variant_3.Checked)
                paint_radiobutton(variant_3, false);

            if (variant_4.Text == _currentQuestion.variant_1)
                paint_radiobutton(variant_4, true);
            else
               if (variant_4.Checked)
                paint_radiobutton(variant_4, false);

            if (variant_5.Text == _currentQuestion.variant_1)
                paint_radiobutton(variant_5, true);
            else
               if (variant_5.Checked)
                paint_radiobutton(variant_5, false);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_currentQuestion != null)
                show_rigth();
            //button1.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            start_panel.Dock = DockStyle.Fill;
            label2.Text = "";
            label3.Text = "Версия: " + Assembly.GetExecutingAssembly().GetName().Version.ToString(); ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (loaded_file)
            {
                start_panel.Visible = false;
            }
            else
            {
                OpenFile();
            }
            // label2.Text = "Загрузите файл";
            //question_panel.Visible = true;
        }

        private void start_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void начатьЗановоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (file_name != "")
            {
                label1.Text = "";
                load_settings();
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new SettingsForm().Show();
        }

        private void пулВопросовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsForm().Show();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().Show();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            question_label.Height = this.Height - 480;
        }

        private void закончитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((_answered != null) && (_answered.Count > 0))
                new resultForm(rigth_checked, _answered).Show();
        }
    }
}
