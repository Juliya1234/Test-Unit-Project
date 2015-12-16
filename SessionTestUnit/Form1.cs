using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
        private string version = "0.1";
        public Form1()
        {
            InitializeComponent();
            this.Text = "Turan test unit version " + version;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            manager = new TestLoadManager();
            load_settings();

        }
        private void load_source_file(string text)
        {
            manager.set_source_list(text);
            current_question = manager.get_next();
            count += 1;
            load_to_labels(current_question);
            label1.Text = "Осталось: " + manager.get_count() +
                ". Правильно отмечено: " + rigth_checked;


        }
        private void load_to_labels(TestQuestion test)
        {
            if (test != null)
            {
                question_label.Text = count + ") " +  test.question;
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
                //-----------------
                string text = "";
                //--------------
                text = hash[random.Next(hash.Count)];
                variant_1.Text = text;
                hash.Remove(text);
                //------------------
                text = hash[random.Next(hash.Count)];
                variant_2.Text = text;
                hash.Remove(text);
                //---------------------------
                text = hash[random.Next(hash.Count)];
                variant_3.Text = text;
                hash.Remove(text);
                //---------------------------
                text = hash[random.Next(hash.Count)];
                variant_4.Text = text;
                hash.Remove(text);
                //---------------------------
                text = hash[random.Next(hash.Count)];
                variant_5.Text = text;
                hash.Remove(text);
                //---------------------------
            }
        }

        private void load_settings()
        {
            //var reader = new StreamReader("questions.txt", Encoding.Default);
            //string text = reader.ReadToEnd();
            //reader.Close();
            count = 0;
            rigth_checked = 0;
            answered = new List<AnsweredQuestion>(0);
            //load_source_file(text);

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
                try
                {
                    var reader = new StreamReader(openFileDialog1.FileName, Encoding.Default);
                    string text = reader.ReadToEnd();
                    reader.Close();
                    load_source_file(text);
                    label2.Text = "Файл загружен. Нажмите \"Начать\"";
                    loaded_file = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            load_settings();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (manager.get_count() == 0)
                new resultForm(count, rigth_checked, answered).Show();
            count += 1;
            current_question = manager.get_next();
            load_to_labels(current_question);
            label1.Text = "Осталось: " + manager.get_count() + 
                ". Правильно отмечено: " + rigth_checked;
            button1.Enabled = true;
            
        }
        private void check_question()
        {
            var answer = new AnsweredQuestion();
            answer.question = current_question.question;
            answer.rigth = current_question.variant_1;
            answer.pos = count;
            //------------------------
            if (variant_1.Checked)
            {
                if (variant_1.Text == current_question.variant_1)
                {
                    variant_1.BackColor = Color.FromArgb(153, 230, 153);
                    rigth_checked += 1;
                 }
                 else
                    variant_1.BackColor = Color.FromArgb(255, 128, 128);
                 answer.was_choose = variant_1.Text;
            }
            //-----------------------------
            if (variant_2.Checked)
            { 
                if (variant_2.Text == current_question.variant_1)
                {
                    variant_2.BackColor = Color.FromArgb(153, 230, 153);
                    rigth_checked += 1;
                }
                else
                    variant_2.BackColor = Color.FromArgb(255, 128, 128);
                answer.was_choose = variant_2.Text;
            }
            //----------------------------
            if (variant_3.Checked)
            {
                if (variant_3.Text == current_question.variant_1)
                {
                    variant_3.BackColor = Color.FromArgb(153, 230, 153);
                    rigth_checked += 1;
                }
                else
                    variant_3.BackColor = Color.FromArgb(255, 128, 128);
                answer.was_choose = variant_3.Text;
            }
            //---------------------
            if (variant_4.Checked)
            {
                if (variant_4.Text == current_question.variant_1)
                {
                    variant_4.BackColor = Color.FromArgb(153, 230, 153);
                    rigth_checked += 1;
                }
                else
                    variant_4.BackColor = Color.FromArgb(255, 128, 128);
                answer.was_choose = variant_4.Text;
            }
            //----------------------
            if (variant_5.Checked)
            {
                if (variant_5.Text == current_question.variant_1)
                {
                    variant_5.BackColor = Color.FromArgb(153, 230, 153);
                    rigth_checked += 1;
                }
                else
                    variant_5.BackColor = Color.FromArgb(255, 128, 128);
                answer.was_choose = variant_5.Text;
            }
            //----------------------------------
            show_rigth();
            answered.Add(answer);

        }
        private void show_rigth()
        {
            if (variant_1.Text == current_question.variant_1)
                variant_1.BackColor = Color.FromArgb(153, 230, 153);

            if (variant_2.Text == current_question.variant_1)
                variant_2.BackColor = Color.FromArgb(153, 230, 153);

            if (variant_3.Text == current_question.variant_1)
                variant_3.BackColor = Color.FromArgb(153, 230, 153);

            if (variant_4.Text == current_question.variant_1)
                variant_4.BackColor = Color.FromArgb(153, 230, 153);

            if (variant_5.Text == current_question.variant_1)
                variant_5.BackColor = Color.FromArgb(153, 230, 153);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (current_question != null)
                check_question();
            button1.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
    }
}
