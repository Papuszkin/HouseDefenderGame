using System;
using System.Collections.Generic;
using System.Text;
using HouseDefenderGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HouseDefenderGame.Classes.Gameplay
{
    public class Player
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Vector2 Position;
        public float Rotation { get; set; }

        private int currentFrame;
        private int totalFrames;
        private float playerSpeed = 200f;
        private bool isMoving;

        public Player(Texture2D texture, Vector2 position, int rows, int columns)
        {
            Texture = texture;
            Position = position;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }

        public void Update(KeyboardState keyboardState, GameTime gameTime, IEnumerable<ICollidable> collidables)
        {
            if (keyboardState.IsKeyDown(Keys.W))
            {
                Vector2 newPosiiton = new Vector2(Position.X, Position.Y - playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                bool movementPossible = true;
                foreach (var item in collidables)
                {
                    if (item.Hitbox.Contains(newPosiiton))
                    {
                        movementPossible = false;
                        return;
                    }
                }

                if (movementPossible)
                {
                    Position.Y -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    isMoving = true;
                }
            }
            
            if (keyboardState.IsKeyDown(Keys.S))
            {
                Vector2 newPosiiton = new Vector2(Position.X, Position.Y + playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                bool movementPossible = true;
                foreach (var item in collidables)
                {
                    if (item.Hitbox.Contains(newPosiiton))
                    {
                        movementPossible = false;
                        return;
                    }
                }

                if (movementPossible)
                {
                    Position.Y += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    isMoving = true;
                }
            }
            
            if (keyboardState.IsKeyDown(Keys.A))
            {
                Vector2 newPosiiton = new Vector2(Position.X - playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);
                bool movementPossible = true;
                foreach (var item in collidables)
                {
                    if (item.Hitbox.Contains(newPosiiton))
                    {
                        movementPossible = false;
                        return;
                    }
                }

                if (movementPossible)
                {
                    Position.X -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    isMoving = true;
                }
                
            }
            
            if (keyboardState.IsKeyDown(Keys.D))
            {
                Vector2 newPosiiton = new Vector2(Position.X + playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);
                bool movementPossible = true;
                foreach (var item in collidables)
                {
                    if (item.Hitbox.Contains(newPosiiton))
                    {
                        movementPossible = false;
                        return;
                    }
                }

                if (movementPossible)
                {
                    Position.X += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    isMoving = true;
                }
                
            }

            if (keyboardState.GetPressedKeyCount() == 0)
            {
                isMoving = false;

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

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, Rotation + (float)(3 * Math.PI / 2), origin, SpriteEffects.None, 1);
            spriteBatch.End();
        }
    }
}
