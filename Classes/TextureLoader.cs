using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseDefenderGame.Classes
{
    public class TextureLoader
    {
        public ContentManager contentManager { get; set; }

        public TextureLoader(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        public Texture2D loadTexture(string fileName)
        {
            Texture2D newTexture;
            try
            {
                newTexture = contentManager.Load<Texture2D>(fileName);
            }
            catch (Exception)
            {
                HouseDefenderGame.States.GameState.errorLog += $"{fileName} failed to load";
                newTexture = contentManager.Load<Texture2D>("default");
            }

            return newTexture;
        }
    }
}
