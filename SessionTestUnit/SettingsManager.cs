using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SessionTestUnit
{
    public class SettingsManager
    {
        private string file_name = "";
        public SettingsManager()
        {
            file_name = "settings.xml";
        }
        public void SaveSettings(Settings settings)
        {
            try
            {
                var stream = new StreamWriter(File.Open(file_name, FileMode.Create));
                var serializer = new XmlSerializer(typeof(Settings));
                serializer.Serialize(stream, settings);
                stream.Close();
            }
            catch (Exception ex)
            {
                define_error("Ошибка 4.1: " + ex.Message);
            }
        }

        public Settings Load()
        {
            try
            {
                if (File.Exists(file_name))
                {
                    var stream = new StreamReader(File.Open(file_name, FileMode.Open));
                    var serializer = new XmlSerializer(typeof(Settings));
                    var return_settings = serializer.Deserialize(stream) as Settings;
                    stream.Close();
                    return return_settings;
                }
                else
                {
                    var settings = new Settings();
                    SaveSettings(settings);
                    return settings;
                }
            }
            catch (Exception ex)
            {

                define_error("Ошибка 4.2\r\nЗагружены настройки по умолчанию " + ex.Message);
                return new Settings();
            }
        }
        private void define_error(string text)
        {
            MessageBox.Show(text);
        }
    }
}

