﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseDefenderGame.Interfaces;
using HouseDefenderGame.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace HouseDefenderGame.Classes.Gameplay
{
    public class Zombie : IEntity
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Vector2 Position;
        public float Rotation { get; set; }

        public Rectangle Hitbox { get ; set; }
        public bool IsSolid { get; set; }
        public int Health { get ; set; }

        private int currentFrame;
        private int totalFrames;
        private float zombieSpeed = 200f;
        public bool isMoving;

        public int X { get; set; }
        public int Y { get; set; }

        public Zombie(Texture2D texture, Vector2 position, int rows, int columns)
        {
            X = (int)position.X;
            Y = (int)position.Y;
            Texture = texture;
            Position = position;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            isMoving = false;
            IsSolid = true;
            Health = 10;
            Hitbox = new Rectangle(X,Y,texture.Width,texture.Height);
        }

        public void Update(IEnumerable<ICollidable> collidables)
        {
            var rnd = new Random();
            var delta = new Vector2(Position.X, Position.Y);

            if (rnd.Next(1,2)==10)
            {
                Position.X = zombieSpeed + rnd.Next(1, 20);
                Position.Y = zombieSpeed + rnd.Next(1, 10);
            }
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, MouseState mouseState)
        {
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            Vector2 dPos = Position - mousePosition;
            Rotation = (float)Math.Atan2(dPos.Y, dPos.X);

            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);

            Vector2 origin = new Vector2(width / 2, height / 2);


            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, Rotation + (float)(3 * Math.PI / 2), origin, SpriteEffects.None, 1);

            
        }

        public void Hurt(int damage)
        {
            Health -= damage;
            if (Health==0)
            {
                IsSolid = false;
            }
        }
    }
}
