using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SessionTestUnit.QuestionClasses;
using Label = System.Reflection.Emit.Label;

namespace SessionTestUnit.Helpers
{
    public class UiHelper
    {

        private readonly Font   _font;
        private Point           _point;
        private readonly int    _labelWidth;

        public UiHelper(Font font, int width)
        {
            _labelWidth = width;
            _font       = font;
        }

        public void setPoint(Point point)
        {
            _point = point;
        }

        public System.Windows.Forms.Label GetLabel(AnsweredQuestion q, int count)
        {
            var label = new System.Windows.Forms.Label();
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
            label.Location = _point;
            label.Font = _font;
            label.AutoSize = false;
            label.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            label.Height = 115;
            label.Width = _labelWidth /* label2.Width */ ;

            return label;
        }
    }
}
