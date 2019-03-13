using GameOverlay.Drawing;
using System;
using System.Collections.Generic;

namespace FutureNNAimbot
{
    public class DrawHelper
    {
        private Settings s;
        GraphicWindow mainWnd;

        public DrawHelper(Settings settings)
        {
            s = settings;
            mainWnd = new GraphicWindow(settings.SizeX, settings.SizeY, s);
        }

        
        public void Draw(System.Drawing.Point curMousPos, string selectedObject, Settings settings, IEnumerable<Alturos.Yolo.Model.YoloItem> items)
        { 
            mainWnd.window.X = curMousPos.X - s.SizeX / 2;
            mainWnd.window.Y = curMousPos.Y - s.SizeY / 2;
            mainWnd.graphics.BeginScene();
            mainWnd.graphics.ClearScene();

           
          
            if (s.DrawAreaRectangle)
                mainWnd.graphics.DrawRectangle(mainWnd.graphics.csb, 0, 0, s.SizeX, s.SizeY, 2);


            mainWnd.graphics.DrawCrosshair(mainWnd.graphics.csfmb,
                s.SizeX / 2, s.SizeY / 2, 6, 2,CrosshairStyle.Gap);

            //draw main text
            mainWnd.graphics.WriteText($"Object {selectedObject}; Mode: {s.ScreenshotMode.ToString()}" );

            foreach (var item in items)
            {
                DrawItem(item, selectedObject);
            }

            mainWnd.graphics.EndScene();
        }

        private void DrawItem(Alturos.Yolo.Model.YoloItem item, string selcected)
        {
            
            Rectangle body = Rectangle.Create(item.X + Convert.ToInt32(item.Width / 6), item.Y + item.Height / 6, Convert.ToInt32(item.Width / 1.5f), item.Height / 3);

            var clr = item.Type == selcected ? mainWnd.graphics.csfmb : mainWnd.graphics.csb;

            mainWnd.graphics.DrawRectangle(clr, Rectangle.Create(item.X, item.Y, item.Width, item.Height), 2);

            
            mainWnd.graphics.DrawRectangle(mainWnd.graphics.bcb, body, 2);
            mainWnd.graphics.DrawCrosshair(mainWnd.graphics.bcb, body.Left + body.Width / 2, body.Top + body.Height / 2 , 2, 2, CrosshairStyle.Cross);
            mainWnd.graphics.DrawLine(mainWnd.graphics.bcb, s.SizeX / 2, s.SizeY / 2, body.Left + body.Width / 2, body.Top + body.Height / 2, 2);
            
        }

        public void DrawDisabled()
        {
            mainWnd.window.X = 0;
            mainWnd.window.Y = 0;
            mainWnd.graphics.BeginScene();
            mainWnd.graphics.ClearScene();
            mainWnd.graphics.EndScene();
        }

        public float DistanceBetweenCross(float X, float Y)
        {
            float ydist = (Y - s.SizeY / 2);
            float xdist = (X - s.SizeX / 2);
            float Hypotenuse = (float)Math.Sqrt(Math.Pow(ydist, 2) + Math.Pow(xdist, 2));
            return Hypotenuse;
        }
    }
}
