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
    public class Player
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Vector2 Position;
        public float Rotation { get; set; }

        public List<Gun> GunInventory { get; set; }
        public int CurrentGun { get; set; }
        public int GunCooldown { get; set; }

        public Queue<Vector2> HM { get; set; }

        public int Money { get; set; }

     
        public Texture2D HitmarkTexture { get; set; }

        private int currentFrame;
        private int totalFrames;
        private float playerSpeed = 200f;
        public bool isMoving;

        public Player(Texture2D texture, Vector2 position, int rows, int columns, Texture2D hitmarkTexture)
        {
            Texture = texture;
            Position = position;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            isMoving = false;
            HM = new Queue<Vector2>();
            GunInventory = new List<Gun> {
                // Name Damage Pellets Spread RateOfFire
                new Gun("Pistol", 5, 1, 5, 30),
                new Gun("Shotgun", 10, 5, 7, 60),
                new Gun("Chaingun", 5, 1, 4, 10),
                new Gun("Sniper", 20, 1, 1, 90)
            };
            CurrentGun = 0;
            GunCooldown = 0;
            Money = 20;

            HitmarkTexture = hitmarkTexture;

        }

        public void Update(KeyboardState keyboardState, GameTime gameTime, IEnumerable<ICollidable> collidables)
        {

            MouseState mouseState = Mouse.GetState();

            // W
            // Move up
            if (keyboardState.IsKeyDown(Keys.W))
            {
                Vector2 newPosiiton = new Vector2(Position.X, Position.Y - playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                Rectangle newRectangle = new Rectangle((int)newPosiiton.X - 16, (int)newPosiiton.Y - 16, 32, 32);
                bool movementPossible = true;
                foreach (var item in collidables)
                {
                    if (item.Hitbox.Intersects(newRectangle) && item.IsSolid)
                    {
                        movementPossible = false;
                        return;
                    }
                }

                if (movementPossible)
                {
                    Position.Y -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    isMoving = true;
                }
            }

            // S
            // Move down
            if (keyboardState.IsKeyDown(Keys.S))
            {
                Vector2 newPosiiton = new Vector2(Position.X, Position.Y + playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                Rectangle newRectangle = new Rectangle((int)newPosiiton.X - 16, (int)newPosiiton.Y - 16, 32, 32);
                bool movementPossible = true;
                foreach (var item in collidables)
                {
                    if (item.Hitbox.Intersects(newRectangle) && item.IsSolid)
                    {
                        movementPossible = false;
                        return;
                    }
                }

                if (movementPossible)
                {
                    Position.Y += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    isMoving = true;
                }
            }

            // A
            // Move left
            if (keyboardState.IsKeyDown(Keys.A))
            {
                Vector2 newPosiiton = new Vector2(Position.X - playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);
                Rectangle newRectangle = new Rectangle((int)newPosiiton.X - 16, (int)newPosiiton.Y - 16, 32, 32);
                bool movementPossible = true;
                foreach (var item in collidables)
                {
                    if (item.Hitbox.Intersects(newRectangle) && item.IsSolid)
                    {
                        movementPossible = false;
                        return;
                    }
                }

                if (movementPossible)
                {
                    Position.X -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    isMoving = true;
                }

            }

            // D
            // Move right
            if (keyboardState.IsKeyDown(Keys.D))
            {
                Vector2 newPosiiton = new Vector2(Position.X + playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);
                Rectangle newRectangle = new Rectangle((int)newPosiiton.X - 16, (int)newPosiiton.Y - 16, 32, 32);
                bool movementPossible = true;
                foreach (var item in collidables)
                {
                    if (item.Hitbox.Intersects(newRectangle) && item.IsSolid)
                    {
                        movementPossible = false;
                        return;
                    }
                }

                if (movementPossible)
                {
                    Position.X += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    isMoving = true;
                }

            }

            // LMB
            // Shoot
            if (mouseState.LeftButton == ButtonState.Pressed)
            {

                Shoot(Position, mouseState.Position.ToVector2(), GameState.mapObjects, GameState.entities);
            }

            // 1
            // Change gun to 0
            if (keyboardState.IsKeyDown(Keys.D1))
            {
                CurrentGun = 0;
            }

            // 2
            // Change gun to 1
            if (keyboardState.IsKeyDown(Keys.D2))
            {
                CurrentGun = 1;
            }

            // 3
            // Change gun to 2
            if (keyboardState.IsKeyDown(Keys.D3))
            {
                CurrentGun = 2;
            }

            // 4
            // Change gun to 3
            if (keyboardState.IsKeyDown(Keys.D4))
            {
                CurrentGun = 3;
            }

            // No keys
            // Reset movement
            if (keyboardState.GetPressedKeyCount() == 0)
            {
                isMoving = false;

            }

            //Hitmarks remove
            if (HM.Count>15)
            {
                HM.Dequeue();
            }


            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;
            GunCooldown++;
        }

        public void Draw(SpriteBatch spriteBatch, MouseState mouseState)
        {
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            Vector2 dPos = Position - mousePosition;
            Rotation = (float)Math.Atan2(dPos.Y, dPos.X);

            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);

            Vector2 origin = new Vector2(width / 2, height / 2);


            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, Rotation + (float)(3 * Math.PI / 2), origin, SpriteEffects.None, 1);


            foreach (var mark in HM)
            {
                spriteBatch.Draw(HitmarkTexture, new Rectangle((int)mark.X, (int)mark.Y, 32, 32), Color.Blue);
            }

        }


        public void Shoot(Vector2 sourcePosition, Vector2 destinationPosition, List<ICollidable> mapObjects, List<IEntity> entities)
        {
            var rnd = new Random();                     // generator liczb losowych

            // Sprawdz czy jest amunicja i czy można strzelić
            if (!(GunInventory[CurrentGun].AmmoCount > 0) | !(GunCooldown > GunInventory[CurrentGun].RateOfFire))
            {
                return;
            }

            GunInventory[CurrentGun].AmmoCount -= 1;
            GunCooldown = 0;

            for (int i = 0; i < GunInventory[CurrentGun].PelletCount; i++)       // dla każdego pocisku
            {
                //soundeffect.

                GameState.soundEffects[0].Play();

                float rndOffset = (float)rnd.Next(-GunInventory[CurrentGun].Spread, GunInventory[CurrentGun].Spread);     // Oblicz losowe odchylenie zgodne z Spread
                float radRndOffset = (float)(rndOffset * (Math.PI / 180));

                float baseAngle = (float)Math.Atan2(destinationPosition.Y - sourcePosition.Y, destinationPosition.X - sourcePosition.X);        // Oblicz startowy kąt między pozycją gracza i myszki

                double shootAngle = baseAngle + radRndOffset;       // Oblicz kąt strzału z odchyleniem


                // Sprawdzanie kolizji
                bool colided = false;
                bool outOfRange = false;
                int distanceTravelled = 0;
                double xToCheck = sourcePosition.X;
                double yToCheck = sourcePosition.Y;

                while (!colided & !outOfRange)
                {
                    foreach (var entity in entities)
                    {
                        if (entity.Hitbox.Contains((int)xToCheck, (int)yToCheck) && entity.IsSolid)
                        {
                            entity.Hurt(GunInventory[CurrentGun].Damage);        // Dodać metode do przeciwników która odejmuje zdrowie
                            colided = true;
                        }
                    }

                    foreach (var mapObj in mapObjects)
                    {
                        // Kolizja ze scianą, oknem, drzwiami
                        if (mapObj.Hitbox.Contains((int)xToCheck, (int)yToCheck) && mapObj.IsSolid)
                        {

                            colided = true;
                        }
                    }
                    // Przesuń sprawdzany punkt
                    if (!colided)
                    {
                        xToCheck = xToCheck + Math.Cos(shootAngle) * 1.02;
                        yToCheck = yToCheck + Math.Sin(shootAngle) * 1.02;

                    }

                    // Sprawdz czy nie jest za ekranem
                    if (xToCheck > 2000 | yToCheck > 2000 | xToCheck < 0 | yToCheck < 0)
                    {
                        outOfRange = true;
                    }

                    if (colided)
                    {
                        HM.Enqueue(new Vector2((float)xToCheck - 16, (float)yToCheck - 16));
                    }
                }


            }
        }
    }
}
