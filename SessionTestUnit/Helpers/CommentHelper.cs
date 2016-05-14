using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionTestUnit.Helpers
{
    public class CommentHelper
    {
        /*
         * Этот класс является предком для класса ругательств
         * Здесь определены все методы, а так же хэши комментариев
         * В дочернем классе переопределены именно хэши
         */

        protected List<string> _hash_100;
        protected List<string> _hash_99_90;
        protected List<string> _hash_89_75;
        protected List<string> _hash_74_50;
        protected List<string> _hash_49;

        public CommentHelper()
        {
            InitiateHashes();
        }

        public string Get(double res)
        {

            if (res >= 100) return GetRandomSwear(_hash_100);

            if (res >= 90 && res < 100) return GetRandomSwear(_hash_99_90);

            if (res >= 75 && res < 90) return GetRandomSwear(_hash_89_75);

            if ((res >= 50) && (res < 75)) return GetRandomSwear(_hash_74_50);

            return GetRandomSwear(_hash_49);
        }

        protected virtual string GetRandomSwear(IReadOnlyList<string> hash)
        {
            return hash[GetRandomNumber(hash.Count)];
        }

        protected virtual int GetRandomNumber(int count)
        {
            return new Random().Next(count);
        }

        protected virtual void InitiateHashes()
        {
            _hash_100 = new List<string>(0)
            {
                "Молодец! Так держать! Эта сессия - ничто для тебя!"
            };
            //---------------------------------
            _hash_99_90 = new List<string>(0)
            {
                "Круто! Еще немного подготовки - и стольник тебе обеспечен!"
            };
            //---------------------------------
            _hash_89_75 = new List<string>(0)
            {
                "Я знаю, ты можешь лучше"
            };
            //---------------------------------
            _hash_74_50 = new List<string>(0)
            {
                "Ну ничего, в другой раз повезет"
            };
            //--------------------------------
            _hash_49 = new List<string>(0)
            {
                "Ну как же так? Это ведь легкий тест"
            };
        }
    }
}
