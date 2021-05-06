using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseDefenderGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HouseDefenderGame.Classes.Gameplay
{
    public class Zombie
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Vector2 Position;
        public float Rotation { get; set; }
        public ITargetable Target { get; set; }


        private int currentFrame;
        private int totalFrames;
        public bool isMoving;

        private float damage = 5;
        private float zombieSpeed = 20f;
        private int health = 100;


        public Zombie(Texture2D texture, Vector2 position, int rows, int columns, Texture2D hitmarkTexture)
        {
            Texture = texture;
            Position = position;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            isMoving = false;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 zombiePosition = new Vector2(Position.X, Position.Y);

            
            Vector2 dPos = Position - zombiePosition;
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

        public void Update(KeyboardState keyboardState, GameTime gameTime, IEnumerable<ICollidable> collidables)
        {
            var rnd = new Random();
            var delta = new Vector2(Position.X, Position.Y);
            
            
            Position.X = zombieSpeed + rnd.Next(1,12);
            Position.Y = zombieSpeed + rnd.Next(1,12);
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;

            
        }
    }
}
