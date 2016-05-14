namespace SessionTestUnit.SettingsHelpers
{
    public class Settings
    {
        public bool questions_limit { get;set; }
        public bool show_swearing { get; set; }
        public Settings()
        {
            questions_limit = false;
            show_swearing = false;
        }
    }
}
