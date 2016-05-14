using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SessionTestUnit.QuestionClasses
{
    class QuestionProcessor
    {

        public List<Question> GetQuestionList(string text)
        {
            var result = new List<Question>(0);
            text = ProcessText(text);

            var questionCount = GetWordCount("<question>", text);
            for (var i = 0; i < questionCount; i++)
            {
                text = text.Substring(text.IndexOf("<question>", StringComparison.Ordinal) + 10);
                var endPos = text.IndexOf("<question>", StringComparison.Ordinal);
                var questionText = text.Substring(0, endPos > -1 ? text.IndexOf("<question>", StringComparison.Ordinal) : text.Length);

                if (endPos > -1)
                    text = text.Substring(endPos);

                var question = GetQuestion(questionText);
                if (question != null)
                {
                    result.Add(question);
                }
                else
                {
                    MessageBox.Show("Возникла ошибка на этапе обработки вопроса " + i);
                }

            }

            return result;
        }

        private static string ProcessText(string text)
        {
            /*
             * TODO:
             * question > [q]3:1
             * variant > [a] ( variant > [a]+)
             * 
             */

            var result = text
                .Replace("\t", "");

            return result;
        }

        private Question GetQuestion(string text)
        {
            try
            {

                var varCount = GetWordCount("<variant>", text);

                var quest = text.Substring(0, text.IndexOf("\r\n<variant>", StringComparison.Ordinal));

                text = text.Substring(text.IndexOf("<variant>", StringComparison.Ordinal));

                var hash = new List<string>(0);

                for (var j = 0; j < varCount; j++)
                {
                    try
                    {
                        text = text.Substring(text.IndexOf("<variant>", StringComparison.Ordinal) + "<variant>".Length);
                        var pos = text.IndexOf("\r\n", StringComparison.Ordinal);
                        var var = text.Substring(0, pos);
                        hash.Add(var);
                        text = text.Substring(pos + 2);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Проблема в обработке вопроса: \r\n" + quest + "\r\n" + ex.Message);
                    }
                }
                switch (hash.Count)
                {
                    case 5:
                    {
                        return new Question
                        {
                            question = quest,
                            variant_1 = hash[0],
                            variant_2 = hash[1],
                            variant_3 = hash[2],
                            variant_4 = hash[3],
                            variant_5 = hash[4]
                        };
                    }
                    case 4:
                    {
                        return new Question
                        {
                            question = quest,
                            variant_1 = hash[0],
                            variant_2 = hash[1],
                            variant_3 = hash[2],
                            variant_4 = hash[3],
                            variant_5 = "Ошибка: отсутствует 5 вариант ответа"
                        };
                   
                    }
                    case 3:
                        {
                            return new Question
                            {
                                question = quest,
                                variant_1 = hash[0],
                                variant_2 = hash[1],
                                variant_3 = hash[2],
                                variant_4 = "Ошибка: отсутствует 4 вариант ответа",
                                variant_5 = "Ошибка: отсутствует 5 вариант ответа"
                            };

                        }
                    case 2:
                        {
                            return new Question
                            {
                                question = quest,
                                variant_1 = hash[0],
                                variant_2 = hash[1],
                                variant_3 = "Ошибка: отсутствует 3 вариант ответа",
                                variant_4 = "Ошибка: отсутствует 4 вариант ответа",
                                variant_5 = "Ошибка: отсутствует 5 вариант ответа"
                            };

                        }
                    case 1:
                        {
                            return new Question
                            {
                                question = quest,
                                variant_1 = hash[0],
                                variant_2 = "Ошибка: отсутствует 2 вариант ответа",
                                variant_3 = "Ошибка: отсутствует 3 вариант ответа",
                                variant_4 = "Ошибка: отсутствует 4 вариант ответа",
                                variant_5 = "Ошибка: отсутствует 5 вариант ответа"
                            };

                        }
                    default:
                        return null;
                }

                
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }

        public int GetWordCount(string word, string source)
        {
            return (source.Length - source.Replace(word, "").Length) / word.Length;
        }

        
    }
}
