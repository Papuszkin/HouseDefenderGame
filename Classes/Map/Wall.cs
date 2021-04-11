using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseDefenderGame.Classes.Map
{
    public class Wall
    {
        static int WALL_THICKNESS = 32;

        // Position
        public int X { get; set; }
        public int Y { get; set; }
        public int Lenght { get; set; }
        public bool IsHorizontal { get; set; }

        // Looks
        public Texture2D WallTexture { get; set; }
        public Color TransparencyColor { get; set; }


        public Wall(int x, int y, int lenght, bool isHorizontal, Texture2D wallTexture)
        {
            X = x;
            Y = y;
            Lenght = lenght;
            IsHorizontal = isHorizontal;
            WallTexture = wallTexture;
            TransparencyColor = Color.White;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();

            if (IsHorizontal)
            {
                sb.Draw(WallTexture, new Rectangle(X, Y, Lenght, WALL_THICKNESS), new Rectangle(0, 0, Lenght, WALL_THICKNESS), TransparencyColor);
            }
            else
            {
                sb.Draw(WallTexture, new Rectangle(X, Y, WALL_THICKNESS, Lenght), new Rectangle(0, 0, WALL_THICKNESS, Lenght), TransparencyColor);
            }
            sb.End();
        }
    }
}
