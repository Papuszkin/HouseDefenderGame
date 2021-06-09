using System;
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

        public Rectangle Hitbox { get; set; }
        public bool IsSolid { get; set; }
        public int Health { get; set; }
        public float zombieSpeed { get; set; }
        public int zombieDamage { get; set; }
        private int currentFrame;
        private int totalFrames;
        public bool isMoving;
        public int attackCooldown { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Zombie(Texture2D texture, Vector2 position, int rows, int columns,float speed, int damage)
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
            Health = 20;
            zombieSpeed = speed;
            zombieDamage = damage;
            attackCooldown = 0;
        }

        public void Update(IEnumerable<ICollidable> collidables)
        {
            var rnd = new Random();
            var playerPosition = new Vector2(GameState.player.Position.X, GameState.player.Position.Y);
            var delta = new Vector2(playerPosition.X - Position.X, playerPosition.Y - Position.Y);


            if (Position.X > playerPosition.X)
            {
                Position.X -= zombieSpeed / 12;
            }
            if (Position.Y > playerPosition.Y)
            {
                Position.Y -= zombieSpeed / 12;
            }
            if (Position.X <= playerPosition.X)
            {
                Position.X += zombieSpeed / 12;
            }
            if (Position.Y <= playerPosition.Y)
            {
                Position.Y += zombieSpeed / 12;
            }

            Hitbox = new Rectangle((int)Position.X-16, (int)Position.Y-16,32, 32);

            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;

            if (Hitbox.Intersects(GameState.player.Hitbox) && attackCooldown > 100)
            {
                attackCooldown = 0;
                GameState.player.Health -= zombieDamage;
            }
            attackCooldown++;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 playerPosition)
        {
            Rotation = (float)Math.Atan2(playerPosition.X,playerPosition.Y);

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
            if (Health == 0)
            {
                IsSolid = false;
            }
        }
    }
}
