using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseDefenderGame.Classes
{
    class AnimatedSprite : StaticSprite
    {
        public int SpriteHeight { get; set; }
        public int SpriteWidth { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        private int currentFrame;

        public AnimatedSprite(int rows, int columns, Vector2 position, float rotation, Texture2D texture, Color setColor, bool isSolid) : base(position, rotation, texture, setColor, isSolid)
        {
            SpriteHeight = texture.Height / rows;
            SpriteWidth = texture.Width / columns;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(SpriteWidth * column, SpriteHeight * row, SpriteWidth, SpriteHeight);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, SpriteWidth, SpriteHeight);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
