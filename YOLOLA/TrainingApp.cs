using Alturos.Yolo;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FutureNNAimbot
{
   
    public class TrainingApp
    {
        private GameProcess gp;
        private gController gc;
        private NeuralNet nn;
        private DrawHelper dh;
        Settings settings;

        static Random random = new Random();

        public string[] objectNames;

        public TrainingApp(GameProcess gp, gController gc, NeuralNet nn, DrawHelper dh)
        {
            this.gp = gp;
            this.nn = nn;
            this.gc = gc;
            this.settings = gp.s;
            this.dh = dh;

            objectNames = nn.TrainingNames;
        }

      

        public void Run()
        {
            if (!gp.IsRunning())
            {
                dh.DrawDisabled();
                return;
            }

            Point coordinates = Cursor.Position;
            var bitmap = gc.ScreenCapture(true, coordinates);
            var items = nn.GetItems(bitmap);
            dh.Draw(coordinates, objectNames?[settings.selectedObject], settings, items);

            if (Util.IsKeyToggled(settings.ScreenshotKey))
            {
                string text = "";
                if (settings.ScreenshotMode != ScreenshotModes.NoObjects)
                {
                    foreach (var item in items)
                    {
                        int iType = Array.IndexOf<string>(objectNames, item.Type);

                        if (settings.ScreenshotMode == ScreenshotModes.AllSelectedType)
                            iType = settings.selectedObject;

                        float sx = settings.SizeX, sy = settings.SizeY;
                        float x = item.X / sx, y = item.Y / sy;
                        float w = item.Width / sx, h = item.Height / sy;

                        x += w / 2;
                        y += h / 2;

                        text += string.Format("{0} {1} {2} {3} {4}", iType, x, y, w, h);
                        text = text.Replace(",", ".");
                        text += "\n";
                    }
                }
                int rand = random.Next(5000, 999999);
                gc.saveCapture(true, $"img/{settings.Game}{rand}.png");
                File.WriteAllText($"img/{settings.Game}{rand}.txt", text);
                Console.WriteLine($"saved {rand}");
                Console.Beep(370,100);
            }

        }

        public void ReadInput()
        {
            if (Util.IsKeyToggled(Keys.PageUp))
            {
                settings.selectedObject = (settings.selectedObject + 1) % nn.TrainingNames.Length;
            }

            if (Util.IsKeyToggled(Keys.PageDown))
            {
                settings.selectedObject = (settings.selectedObject - 1 + nn.TrainingNames.Length) % nn.TrainingNames.Length;
            }

            if (Util.IsKeyToggled(settings.ScreenshotModeKey))
            {
                settings.ScreenshotMode = (ScreenshotModes) ((((int)settings.ScreenshotMode) + 1) % (int)ScreenshotModes.Length);
            }
           
        }
    }
}
