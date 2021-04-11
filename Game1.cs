using HouseDefenderGame.Classes.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace HouseDefenderGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<Wall> houseWalls = new List<Wall>();
        private List<Window> houseWindows = new List<Window>();
        private List<Door> houseDoors = new List<Door>();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D wallTexture = Content.Load<Texture2D>("tempWallRepeating");
            Texture2D windowTexture = Content.Load<Texture2D>("tempWindow");
            Texture2D doorTexture = Content.Load<Texture2D>("tempDoor");
            
            houseWalls.Add(new Wall(300, 350, 100, true, wallTexture));
            houseDoors.Add(new Door(400, 350, true, doorTexture));
            houseWalls.Add(new Wall(528, 350, 172, true, wallTexture));
            houseWindows.Add(new Window(700, 350, true, windowTexture));
            houseWalls.Add(new Wall(828, 350, 100, true, wallTexture));
            houseWalls.Add(new Wall(900, 328, 54, false, wallTexture));
            houseWindows.Add(new Window(900, 200, false, windowTexture));
            houseWalls.Add(new Wall(900, 150, 50, false, wallTexture));
            houseWalls.Add(new Wall(900, 150, 300, true, wallTexture));
            houseWalls.Add(new Wall(1168, 150, 450, false, wallTexture));
            houseWalls.Add(new Wall(300, 600, 900, true, wallTexture));
            houseWalls.Add(new Wall(300, 350, 250, false, wallTexture));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);

            foreach (var wall in houseWalls)
            {
                wall.Draw(_spriteBatch);
            }

            foreach (var window in houseWindows)
            {
                window.Draw(_spriteBatch);
            }

            foreach (var door in houseDoors)
            {
                door.Draw(_spriteBatch);
            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
