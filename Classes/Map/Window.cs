﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseDefenderGame.Classes.Map
{
    public class Window
    {
        static int WINDOW_WIDTH = 128;
        static int WINDOW_HEIGHT = 32;
        static int STARTING_HEALTH = 50;
        static int STARTING_STATE = 0;
        static bool STARTING_COLLISION = true;


        public int X { get; set; }
        public int Y { get; set; }
        public bool IsHorizontal { get; set; }

        public Texture2D WindowTexture { get; set; }
        public Color TransparencyColor { get; set; }

        public int CurrentHealth { get; set; }
        public bool IsCollidable { get; set; }
        public int CurrentState { get; set; }

        public Window(int x, int y, bool isHorizontal, Texture2D windowTexture)
        {
            X = x;
            Y = y;
            IsHorizontal = isHorizontal;
            WindowTexture = windowTexture;
            TransparencyColor = Color.White;
            CurrentHealth = STARTING_HEALTH;
            IsCollidable = STARTING_COLLISION;
            CurrentState = STARTING_STATE;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();

            if (IsHorizontal)
            {
                sb.Draw(
                WindowTexture,
                new Rectangle(X, Y, WINDOW_WIDTH, WINDOW_HEIGHT),
                new Rectangle(CurrentState * WINDOW_HEIGHT, CurrentState * WINDOW_WIDTH, WINDOW_WIDTH, WINDOW_HEIGHT),
                TransparencyColor
                );
            }
            else
            {
                sb.Draw(
                WindowTexture,
                new Rectangle(X, Y, WINDOW_HEIGHT, WINDOW_WIDTH),
                new Rectangle(CurrentState * WINDOW_HEIGHT, CurrentState * WINDOW_WIDTH, WINDOW_WIDTH, WINDOW_HEIGHT),
                TransparencyColor
                );
            }
            sb.End();
        }
    }
}