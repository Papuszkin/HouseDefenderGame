using HouseDefenderGame.Classes.Gameplay;
using HouseDefenderGame.Classes.Map;
using HouseDefenderGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
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

        public static List<ICollidable> mapObjects = new List<ICollidable>();

        private Player player;

        public static List<SoundEffect> soundEffects;

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

            Texture2D playerTexture = Content.Load<Texture2D>("Player");
            Texture2D hitmarkTexture = Content.Load<Texture2D>("Hitmark");
            player = new Player(playerTexture, new Vector2(100, 100), 1, 3, hitmarkTexture);
            
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

            mapObjects.AddRange(houseWalls);

            //Muzyka Strzalu
            soundEffects.Add(Content.Load<SoundEffect>("GunShot1"));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update(ks, gameTime, houseWalls);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();

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

            player.Draw(_spriteBatch, ms);

            base.Draw(gameTime);
        }
    }
}
