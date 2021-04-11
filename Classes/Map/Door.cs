using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseDefenderGame.Classes.Map
{
    public class Door
    {
        static int DOOR_WIDTH = 128;
        static int DOOR_HEIGHT = 32;
        static int STARTING_STATE = 0;
        static bool STARTING_COLLISION = true;


        public int X { get; set; }
        public int Y { get; set; }
        public bool IsHorizontal { get; set; }

        public Texture2D DoorTexture { get; set; }
        public Color TransparencyColor { get; set; }

        public int CurrentState { get; set; }
        public bool IsCollidable { get; set; }


        public Door(int x, int y, bool isHorizontal, Texture2D doorTexture)
        {
            X = x;
            Y = y;
            IsHorizontal = isHorizontal;
            DoorTexture = doorTexture;
            TransparencyColor = Color.White;
            CurrentState = STARTING_STATE;
            IsCollidable = STARTING_COLLISION;
        }

        public void Draw(SpriteBatch sb)
        {
            
            sb.Begin();
            if (IsHorizontal)
            {
                sb.Draw(
                DoorTexture,
                new Rectangle(X, Y, DOOR_WIDTH, DOOR_HEIGHT),
                new Rectangle(CurrentState * DOOR_HEIGHT, CurrentState * DOOR_WIDTH, DOOR_WIDTH, DOOR_HEIGHT),
                TransparencyColor
                );
            }
            else
            {
                sb.Draw(
                DoorTexture,
                new Rectangle(X, Y, DOOR_HEIGHT, DOOR_WIDTH),
                new Rectangle(CurrentState * DOOR_HEIGHT, CurrentState * DOOR_WIDTH, DOOR_WIDTH, DOOR_HEIGHT),
                TransparencyColor
                );
            }
            sb.End();
        }
    }
}
