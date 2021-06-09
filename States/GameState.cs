using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseDefenderGame.Classes.Gameplay;
using HouseDefenderGame.Classes.Map;
using HouseDefenderGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HouseDefenderGame.States
{
    public class GameState : State
    {


        public static Random random;
        private List<Wall> houseWalls = new List<Wall>();
        private List<Window> houseWindows = new List<Window>();
        private List<Door> houseDoors = new List<Door>();
        private List<Zombie> zombies = new List<Zombie>();

        public static List<ICollidable> mapObjects = new List<ICollidable>();
        public static List<IEntity> entities = new List<IEntity>();

        private Player player;
        private Zombie zombie;

        public static List<SoundEffect> soundEffects = new List<SoundEffect>();

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            random = new Random();
        }

        public override void LoadContent()
        {


            Texture2D wallTexture = _content.Load<Texture2D>("tempWallRepeating");
            Texture2D windowTexture = _content.Load<Texture2D>("tempWindow");
            Texture2D windowTextureVertical = _content.Load<Texture2D>("tempWindowVertical");
            Texture2D doorTexture = _content.Load<Texture2D>("tempDoor");

            Texture2D playerTexture = _content.Load<Texture2D>("Player");
            Texture2D zombieTexture = _content.Load<Texture2D>("Zombie1");
            Texture2D hitmarkTexture = _content.Load<Texture2D>("Hitmark");
            player = new Player(playerTexture, new Vector2(100, 100), 1, 3, hitmarkTexture);

            zombies.Add(new Zombie(zombieTexture, new Vector2(120, 120), 1, 3));
            //zombies.Add(new Zombie(zombieTexture, new Vector2(140, 120), 1, 3));

            houseWalls.Add(new Wall(300, 350, 100, true, wallTexture));
            houseDoors.Add(new Door(400, 350, true, doorTexture));
            houseWalls.Add(new Wall(528, 350, 172, true, wallTexture));
            houseWindows.Add(new Window(700, 350, true, windowTexture));
            houseWalls.Add(new Wall(828, 350, 100, true, wallTexture));
            houseWalls.Add(new Wall(900, 328, 54, false, wallTexture));
            houseWindows.Add(new Window(900, 200, false, windowTextureVertical));
            houseWalls.Add(new Wall(900, 150, 50, false, wallTexture));
            houseWalls.Add(new Wall(900, 150, 300, true, wallTexture));
            houseWalls.Add(new Wall(1168, 150, 450, false, wallTexture));
            houseWalls.Add(new Wall(300, 600, 900, true, wallTexture));
            houseWalls.Add(new Wall(300, 350, 250, false, wallTexture));

            mapObjects.AddRange(houseWalls);
            mapObjects.AddRange(houseWindows);
            entities.AddRange(zombies);

            //Dzwiek Strzalu
            SoundEffect.MasterVolume = 0.1f;
            soundEffects.Add(_content.Load<SoundEffect>("GunShot1"));

        }


        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
            KeyboardState ks = Keyboard.GetState();



            // TODO: Add your update logic here
            player.Update(ks, gameTime, mapObjects);
            foreach (var zombie in zombies)
            {
                zombie.Update(mapObjects);
            }

            PostUpdate(gameTime);


        }

        public override void PostUpdate(GameTime gameTime)
        {


        }



        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            MouseState ms = Mouse.GetState();
            spriteBatch.Begin();


            foreach (var wall in houseWalls)
            {
                wall.Draw(spriteBatch);
            }

            foreach (var window in houseWindows)
            {
                window.Draw(spriteBatch);
            }

            foreach (var door in houseDoors)
            {
                door.Draw(spriteBatch);
            }


            foreach (var zombie in zombies)
            {
                zombie.Draw(spriteBatch, ms);
            }

            player.Draw(spriteBatch, ms);


            spriteBatch.End();
        }

    }
}
