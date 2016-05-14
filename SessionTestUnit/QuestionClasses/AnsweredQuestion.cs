namespace SessionTestUnit.QuestionClasses
{
    public class AnsweredQuestion
    {
        public string question { get; set; }
        public string rigth { get; set; }
        public string was_choose { get; set; }
        public AnsweredQuestion(string question, string rigth, string was_choose)
        {
            this.question = question;
            this.rigth = rigth;
            this.was_choose = was_choose;
        }
        public AnsweredQuestion()
        {
            this.question = "";
            this.rigth = "";
            this.was_choose = "";
        }
    }
}
