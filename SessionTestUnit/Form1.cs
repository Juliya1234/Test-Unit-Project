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
        private string file_name = "";
        private TestLoadManager manager;
        private TestQuestion current_question;
        private int count = 0;
        private int rigth_checked = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            manager = new TestLoadManager();
            load_settings();

        }
        private void load_source_file(string text)
        {
            manager.set_source_list(text);
            //var questions = manager.get_questions();
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
                variant_1.BackColor = DefaultBackColor;
                variant_2.BackColor = DefaultBackColor;
                variant_3.BackColor = DefaultBackColor;
                variant_4.BackColor = DefaultBackColor;
                variant_5.BackColor = DefaultBackColor;
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

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
                string fileName = openFileDialog1.FileName;
                var reader = new StreamReader(file_name, Encoding.Default);
                string text = reader.ReadToEnd();
                reader.Close();
                load_source_file(text);
        }
        private void load_settings()
        {
            var reader = new StreamReader("questions.txt", Encoding.Default);
            string text = reader.ReadToEnd();
            reader.Close();
            count = 0;
            rigth_checked = 0;
            load_source_file(text);

        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            load_settings();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            count += 1;
            current_question = manager.get_next();
            load_to_labels(current_question);
            label1.Text = "Осталось: " + manager.get_count() + 
                ". Правильно отмечено: " + rigth_checked;
            button1.Enabled = true;
            if (manager.get_count() == 0)
                new resultForm(count, rigth_checked).Show();
        }
        private void check_question()
        {
            if (current_question != null)
            {
                if (variant_1.Text == current_question.variant_1)
                {
                    variant_1.BackColor = Color.Green;
                    if (variant_1.Checked)
                        rigth_checked += 1;
                }
                else
                    if (variant_1.Checked)
                        variant_1.BackColor = Color.Red;

                if (variant_2.Text == current_question.variant_1)
                {
                    variant_2.BackColor = Color.Green;
                    if (variant_2.Checked)
                        rigth_checked += 1;
                }
                else
                    if (variant_2.Checked)
                        variant_2.BackColor = Color.Red;

                if (variant_3.Text == current_question.variant_1)
                {
                    variant_3.BackColor = Color.Green;
                    if (variant_3.Checked)
                        rigth_checked += 1;
                }
                else
                    if (variant_3.Checked)
                        variant_3.BackColor = Color.Red;

                if (variant_4.Text == current_question.variant_1)
                {
                    variant_4.BackColor = Color.Green;
                    if (variant_4.Checked)
                        rigth_checked += 1;
                }
                else
                    if (variant_4.Checked)
                        variant_4.BackColor = Color.Red;

                if (variant_5.Text == current_question.variant_1)
                {
                    variant_5.BackColor = Color.Green;
                    if (variant_5.Checked)
                        rigth_checked += 1;
                }
                else
                    if (variant_5.Checked)
                        variant_5.BackColor = Color.Red;
                //----------------------------------
                show_rigth();
            }

        }
        private void show_rigth()
        {
            if (variant_1.Text == current_question.variant_1)
                variant_1.BackColor = Color.Green;

            if (variant_2.Text == current_question.variant_1)
                variant_2.BackColor = Color.Green;

            if (variant_3.Text == current_question.variant_1)
                variant_3.BackColor = Color.Green;

            if (variant_4.Text == current_question.variant_1)
                variant_4.BackColor = Color.Green;

            if (variant_5.Text == current_question.variant_1)
                variant_5.BackColor = Color.Green;


        }
        private void variant_1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            check_question();
            button1.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
