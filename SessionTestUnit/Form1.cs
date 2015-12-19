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

namespace SessionTestUnit
{
    public partial class Form1 : Form
    {
        private TestLoadManager manager;
        private TestQuestion current_question;
        private List<AnsweredQuestion> answered;
        private int count = 0;
        private int rigth_checked = 0;
        private bool loaded_file = false;
        private string file_name = "";
        //-------------------------
        private Settings settings;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Тестировщик Platonus";
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            manager = new TestLoadManager();

        }
        private void load_source_file(string filename)
        {
            var reader = new StreamReader(filename, Encoding.Default);
            string text = reader.ReadToEnd();
            reader.Close();
            //-----------------------------
            manager.set_source_list(text);
            if (settings != null)
                if (settings.questions_limit)
                    manager.set_question_limit(25);
            //----------------------------
            current_question = manager.get_next();
            count += 1;
            load_to_labels(current_question);
            label1.Text = "Осталось: " + manager.get_count();


        }
        private void load_to_labels(TestQuestion test)
        {
            if (test != null)
            {
                question_label.Text = test.question;
                var random = new Random();
                var hash = new List<string>(0);
                hash.Add(test.variant_1);
                hash.Add(test.variant_2);
                hash.Add(test.variant_3);
                hash.Add(test.variant_4);
                hash.Add(test.variant_5);
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
        }

        private void load_settings()
        {
            settings = new SettingsManager().Load();
            count = 0;
            rigth_checked = 0;
            answered = new List<AnsweredQuestion>(0);
            if (file_name != "")
                load_source_file(file_name);
            button3.Text = "Следующий вопрос";

        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //--------------------------
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                 //load_source_file(openFileDialog1.FileName);
                 file_name = openFileDialog1.FileName;
                 load_settings();
                 label2.Text = "Файл загружен. Нажмите \"Начать\"";
                 loaded_file = true;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (answered.Count == manager.get_first_list_count())
            {
                new resultForm(rigth_checked, answered).Show();
            }
            else
            {
                check_question();
                current_question = manager.get_next();
                load_to_labels(current_question);
                //label1.Text = "Вопрос " + count + " из " + manager.get_count();
                label1.Text = "Осталось: " + manager.get_count();
                button1.Enabled = true;
                if (answered.Count == manager.get_first_list_count())
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
            answer.question = current_question.question;
            answer.rigth = current_question.variant_1;
            //------------------------
            if (variant_1.Checked)
            {
                if (variant_1.Text == current_question.variant_1)
                    rigth_checked += 1;
                answer.was_choose = variant_1.Text;
            }
            //-----------------------------
            if (variant_2.Checked)
            {
                if (variant_2.Text == current_question.variant_1)
                    rigth_checked += 1;
                answer.was_choose = variant_2.Text;
            }
            //----------------------------
            if (variant_3.Checked)
            {
                if (variant_3.Text == current_question.variant_1)
                    rigth_checked += 1;
                answer.was_choose = variant_3.Text;
            }
            //---------------------
            if (variant_4.Checked)
            {
                if (variant_4.Text == current_question.variant_1)
                    rigth_checked += 1;
                answer.was_choose = variant_4.Text;
            }
            //----------------------
            if (variant_5.Checked)
            {
                if (variant_5.Text == current_question.variant_1)
                    rigth_checked += 1;
                answer.was_choose = variant_5.Text;
            }
            //----------------------------------
            
            answered.Add(answer);

        }
        private void show_rigth()
        {
            if (variant_1.Text == current_question.variant_1)
                paint_radiobutton(variant_1, true);
            else
               if (variant_1.Checked)
                paint_radiobutton(variant_1, false);

            if (variant_2.Text == current_question.variant_1)
                paint_radiobutton(variant_2, true);
            else
               if (variant_2.Checked)
                paint_radiobutton(variant_2, false);

            if (variant_3.Text == current_question.variant_1)
                paint_radiobutton(variant_3, true);
            else
               if (variant_3.Checked)
                paint_radiobutton(variant_3, false);

            if (variant_4.Text == current_question.variant_1)
                paint_radiobutton(variant_4, true);
            else
               if (variant_4.Checked)
                paint_radiobutton(variant_4, false);

            if (variant_5.Text == current_question.variant_1)
                paint_radiobutton(variant_5, true);
            else
               if (variant_5.Checked)
                paint_radiobutton(variant_5, false);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (current_question != null)
                show_rigth();
            button1.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            start_panel.Dock = DockStyle.Fill;
            label3.Text = "Версия: " + Assembly.GetExecutingAssembly().GetName().Version.ToString(); ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (loaded_file)
                start_panel.Visible = false;
            else
                label2.Text = "Загрузите файл";
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
    }
}
