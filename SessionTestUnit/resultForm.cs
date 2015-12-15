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
            label2.Text = result.ToString("#.##") + "%";
        }
    }
}
