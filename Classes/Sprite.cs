using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HouseDefenderGame.Classes
{
    class Sprite
    {
        // Positioning
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Center { get; set; }

        // Texture
        public Texture2D Texture { get; set; }
        public Color SetColor { get; set; }

        // Size
        public int Width { get; set; }
        public int Height { get; set; }

        // State
        public bool IsSolid { get; set; }

        public Sprite(Vector2 position, float rotation, Texture2D texture, Color setColor, bool isSolid)
        {
            Position = position;
            Rotation = rotation;
            Texture = texture;
            SetColor = setColor;
            IsSolid = isSolid;

            Width = texture.Width;
            Height = texture.Height;

            Center = new Vector2(Width / 2, Height / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,
                new Rectangle((int)Position.X, (int)Position.Y, Width, Height),
                null,
                SetColor,
                Rotation,
                Center,
                SpriteEffects.None,
                1f
            );
        }
    }
}
