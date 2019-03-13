using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;

namespace FutureNNAimbot
{
    public enum ScreenshotModes { Prediction = 0, AllSelectedType = 1, NoObjects = 2, Length = 3 };

    [DataContract]
    public class Settings
    {
        [DataMember]
        public int SizeX { get; set; }
        [DataMember]
        public int SizeY { get; set; }
        [DataMember]
        public string Game { get; set; }
        [DataMember]
        public Keys ScreenshotKey { get; set; }
        [DataMember]
        public Keys ScreenshotModeKey { get; set; }
        [DataMember]
        public bool Information { get; set; }
        [DataMember]
        public bool DrawAreaRectangle { get; set; }
        [DataMember]
        public bool DrawText { get; set; }
        [DataMember]
        public ScreenshotModes ScreenshotMode { get; set; }

        //Temporary not saved Settings
        public int selectedObject = 0;
        public bool trainingMode = false;
        
        [DataMember]
        public int Transparency { get; set; }
        
        static public Settings ReadSettings()
        {
            // Read settings
            DataContractJsonSerializer Settings = new DataContractJsonSerializer(typeof(Settings[]));
            Settings[] settings = null;
            Settings auto_config = new Settings()
            {
                SizeX = 320,
                SizeY = 320,
                Game = "processname",
                ScreenshotKey = Keys.Home,
                ScreenshotModeKey = Keys.NumPad9,
                Information = true,
                DrawAreaRectangle = true,
                DrawText = true,
                Transparency = 255

            };
            using (var fs = new FileStream("config.json", FileMode.OpenOrCreate))
            {
                if (fs.Length == 0)
                {
                    Settings.WriteObject(fs, new Settings[1] { auto_config });
                    fs.Close();
                    File.WriteAllText("config.json", File.ReadAllText("config.json").Replace(",", ",\n"));
                    MessageBox.Show($"Created auto-config, change whatever settings you want and restart.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                    return null;
                }
                else settings = (Settings[])Settings.ReadObject(fs);
                return settings?[0];
            }
        }
    }
}
