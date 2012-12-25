using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Flakcore.Utils
{
    class Debug
    {
        private static int timer;
        private static string timerSubject;
        private static Dictionary<string, string> infoLines;

        public static void writeLine(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public static void DrawLine(SpriteBatch batch, Texture2D blank, float width, Color color, Vector2 point1, Vector2 point2)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);

            batch.Draw(blank, point1, null, color,angle, Vector2.Zero, new Vector2(length, width),SpriteEffects.None, 0);
        }

        public static void timerStart(string subject)
        {
            timerSubject = subject;
            timer = System.Environment.TickCount;
        }

        public static int timerStop()
        {
            //System.Diagnostics.Debug.WriteLine("Flakcore - Timer stopped: " + timerSubject + " took " + (System.Environment.TickCount - timer));

            return System.Environment.TickCount - timer;
        }

        public static void addDebugItem(string name, string value)
        {
            if (infoLines == null)
                infoLines = new Dictionary<string, string>();

            if(!infoLines.ContainsKey(name))
                infoLines.Add(name, value);
        }

        public static void draw(SpriteBatch spriteBatch)
        {
            if (infoLines == null)
                return;

            int i = 0;
            foreach (KeyValuePair<string, string> line in infoLines)
            {
                spriteBatch.DrawString(GameManager.fontDefault, line.Key + ":", new Vector2(3, 20 * i), Color.Black);
                spriteBatch.DrawString(GameManager.fontDefault, line.Value, new Vector2(180, 20 * i), Color.Black);
                i++;
            }

            infoLines.Clear();
        }
    }
}
