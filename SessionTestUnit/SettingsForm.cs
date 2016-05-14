using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SessionTestUnit.SettingsHelpers;

namespace SessionTestUnit
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            var settings = new SettingsManager().Load();
            checkBox2.Checked = settings.questions_limit;
            checkBox1.Checked = settings.show_swearing;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var settings = new Settings();
            settings.questions_limit    = checkBox2.Checked;
            settings.show_swearing         = checkBox1.Checked;
            new SettingsManager().SaveSettings(settings);
            this.Close();
        }
    }
}
