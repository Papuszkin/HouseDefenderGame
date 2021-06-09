using HouseDefenderGame.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseDefenderGame.Classes.Map
{
    public class Shop
    {
        int HEIGHT = 64;
        int WIDTH = 64;

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Texture2D ShopTexture { get; set; }

        public bool IsHidden { get; set; }
        public int[] Prices { get; set; }

        public Shop(Vector2 position, float rotation, Texture2D shopTexture)
        {
            Position = position;
            Rotation = rotation;
            ShopTexture = shopTexture;

            IsHidden = true;
            Prices = new int[] { 2, 7, 5, 10 };
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(
                ShopTexture,
                new Rectangle((int)Position.X, (int)Position.Y, WIDTH, HEIGHT),
                new Rectangle(Convert.ToInt32(IsHidden) * WIDTH, 0, WIDTH, HEIGHT),
                Color.White
                );
            sb.End();
        }

        public bool CheckDistance()
        {
            Vector2 playerPos = GameState.player.Position;
            float dist =  Vector2.Distance(playerPos, Position);

            if (dist < 200)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Update(KeyboardState kb)
        {

            // In distance
            if (CheckDistance())
            {
                // Change texture
                IsHidden = false;

                // Shop button held down
                if(kb.IsKeyDown(Keys.LeftControl))
                {
                    // Buying pistol ammo
                    if (kb.IsKeyDown(Keys.D1))
                    {
                        GameState.player.GunInventory[0].AmmoCount += 10;
                        GameState.player.Money -= 2;
                    }
                    // Buying shotgun ammo
                    if (kb.IsKeyDown(Keys.D2))
                    {
                        GameState.player.GunInventory[1].AmmoCount += 5;
                        GameState.player.Money -= 7;
                    }
                    // Buying machinegun ammo
                    if (kb.IsKeyDown(Keys.D3))
                    {
                        GameState.player.GunInventory[2].AmmoCount += 10;
                        GameState.player.Money -= 5;
                    }
                    // Buying rifle ammo
                    if (kb.IsKeyDown(Keys.D4))
                    {
                        GameState.player.GunInventory[3].AmmoCount += 2;
                        GameState.player.Money -= 10;
                    }
                }
            }

            if (!CheckDistance())
            {
                IsHidden = true;
            }
        }
    }
}
