using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SessionTestUnit.QuestionClasses
{
    public class QuestionManager
    {
        private List<Question> _list;
        private int _firstListCount;
        private readonly QuestionProcessor _questionProcessor;

        public QuestionManager()
        {
            _list = new List<Question>(0);
            _questionProcessor = new QuestionProcessor();
        }

        public void SetSourceList(string text)  
        {
            _list = new List<Question>(0);
            text = text.Replace("\t", "");

            _list = _questionProcessor.GetQuestionList(text);
            _firstListCount = _list.Count;
                      
        }

        public Question get_next()
        {
            if (_list.Count <= 0) return null;
            var random = new Random();
            var question = _list[random.Next(_list.Count)];
            _list.Remove(question);
            return question;
        }
        
        public int GetCount() => _list.Count;

        public int get_first_list_count() => _firstListCount;

        public void set_question_limit(int limit)
        {
            if (limit > _list.Count) return;
            var temp = new List<Question>(0);
            for (var i = 0; i < limit; i++)
            {
                temp.Add(_list[i]);
            }
            _list = temp;
            _firstListCount = _list.Count;
        }
    }
}
