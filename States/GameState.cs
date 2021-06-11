using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseDefenderGame.Classes;
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

        public static Player player;
        public static Shop houseShop;
        //Health bar
        Texture2D healthTexture;
        Rectangle healthRectangle;
        MouseState pastMouse;
        public static List<SoundEffect> soundEffects = new List<SoundEffect>();

        // Error loading log
        public static string errorLog;
        public TextureLoader Loader;
        private SpriteFont arialFont;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            random = new Random();
        }

        public override void LoadContent()
        {
            errorLog = "";
            Loader = new TextureLoader(_content);
            arialFont = _content.Load<SpriteFont>("font");

            Texture2D wallTexture, windowTexture, windowTextureVertical, doorTexture;
            wallTexture = Loader.loadTexture("tempWallRepeating");
            windowTexture = Loader.loadTexture("tempWindow");
            windowTextureVertical = Loader.loadTexture("tempWindowVertica");
            doorTexture = Loader.loadTexture("tempDoor");

            Texture2D playerTexture = _content.Load<Texture2D>("Player");
            Texture2D zombieTexture = _content.Load<Texture2D>("Zombie1");
            Texture2D hitmarkTexture = _content.Load<Texture2D>("Hitmark");
            healthTexture = _content.Load<Texture2D>("Health");
            player = new Player(playerTexture, new Vector2(100, 100), 1, 3, hitmarkTexture, 100);

            var rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                zombies.Add(new Zombie(zombieTexture, new Vector2(rnd.Next(10, 400), rnd.Next(10, 400)), 1, 3,(float)rnd.Next(1,10),rnd.Next(1,7)));
            }
            
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
            entities.AddRange(houseWindows);
            entities.AddRange(zombies);

            //Dzwiek Strzalu
            SoundEffect.MasterVolume = 0.1f;
            soundEffects.Add(_content.Load<SoundEffect>("GunShot1"));

            // Sklep
            Texture2D shopTexture = _content.Load<Texture2D>("Shop");
            houseShop = new Shop(new Vector2(1100, 180), 0f, shopTexture);

        }


        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
            KeyboardState ks = Keyboard.GetState();


            MouseState mouse = Mouse.GetState();
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
            healthRectangle = new Rectangle(50, 20,player.Health, 20);

            //Tutaj logika otrzymywania obrażeń od zombie trzeba dodać
            
                


            // TODO: Add your update logic here
            player.Update(ks, gameTime, mapObjects);
            pastMouse = mouse;
            foreach (var zombie in zombies)
            {
                zombie.Update(entities);
            }
            
            // TODO: Add your update logic here
            player.Update(ks, gameTime, mapObjects);

            houseShop.Update(ks);

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
                if (zombie.IsSolid)
                {
                    zombie.Draw(spriteBatch,player.Position);
                }
	           
            }
            if (player.Health > 0)
            {
                player.Draw(spriteBatch, ms);
            }
            spriteBatch.Draw(healthTexture, healthRectangle, Color.White);

            spriteBatch.DrawString(arialFont, errorLog, new Vector2(10, 10), Color.Black);

            spriteBatch.End();

            houseShop.Draw(spriteBatch);
        }

    }
}
