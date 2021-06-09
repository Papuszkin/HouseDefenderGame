using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseDefenderGame.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HouseDefenderGame.States
{
    public class MenuState : State
    {
        private List<Component> _components;
        private Texture2D menuBackGroundTexture;
        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Button");
            var buttonFont = _content.Load<SpriteFont>("font");


            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(900, 200),
                Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(900, 250),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
      {
        newGameButton,
        
        quitGameButton,
      };
        }
        public override void LoadContent()
        {
            menuBackGroundTexture = _content.Load<Texture2D>("MainMenu");
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(menuBackGroundTexture, new Vector2(0, 0), Color.White);
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

    }
}
