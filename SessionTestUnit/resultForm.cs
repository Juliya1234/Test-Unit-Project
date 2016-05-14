using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SessionTestUnit.Helpers;
using SessionTestUnit.QuestionClasses;
using SessionTestUnit.SettingsHelpers;

namespace SessionTestUnit
{
    public partial class resultForm : Form
    {
        private readonly int                    _rigth;
        private readonly List<AnsweredQuestion> _hash;
        private readonly SwearHelper            _swearHelper;
        private readonly CommentHelper          _goodHelper;
        private readonly Settings               _settings;

        public resultForm(int rigth, List<AnsweredQuestion> hash)
        {
            InitializeComponent();
            this._rigth     = rigth;
            this._hash      = hash;
            _goodHelper     = new CommentHelper();
            _swearHelper    = new SwearHelper();
            _settings       = new SettingsManager().Load();
        }

        private void resultForm_Load(object sender, EventArgs e)
        {
            if (_rigth > 0)
            {
                var result = (double)_rigth / _hash.Count;
                result = result * 100;
                label1.Text = "Ваш результат: " + result.ToString("#.##") + "%";
                label2.Text = get_words(result);
            }
            else
            {
                label1.Text = "Ваш результат: 0%";
                label2.Text = get_words(0);
            }
            create_labels(_hash);
            //----------------------------------------
    }
        private string get_words(double res)
        {
            return _settings.show_swearing ? _swearHelper.Get(res) : _goodHelper.Get(res);
        }

        
        
        private void create_labels(IEnumerable<AnsweredQuestion> hash)
        {
            
            var start_location_y = label2.Location.Y + label2.Height + 10;
            var count = 1;
            var font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);

            var uiHelper = new UiHelper(font, label2.Width);

            foreach (var q in hash)
            {
                uiHelper.setPoint(new Point(label2.Location.X, start_location_y));
                var label = uiHelper.GetLabel(q, count);
                Controls.Add(label);
                start_location_y += label.Height + 10;
                count += 1;
            }
        }
    }
}
