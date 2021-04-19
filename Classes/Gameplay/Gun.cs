using HouseDefenderGame.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseDefenderGame.Classes.Gameplay
{
    public class Gun
    {
        public int Damage { get; set; }
        public int PelletCount { get; set; }
        public int Spread { get; set; }
        public int RateOfFire { get; set; }

        public Gun(int damage, int spread, int rateOfFire)
        {
            Damage = damage;
            Spread = spread;
            RateOfFire = rateOfFire;
        }

        // Change rectangle to MapObjects containing all walls, windows and doors
        // Change rectangle to Entites containing all enemies and shop
        public void Shoot(Vector2 sourcePosition, Vector2 destinationPosition, List<ICollidable> mapObjects, List<Rectangle> entities)
        {
            var rnd = new Random();                     // generator liczb losowych

            for (int i = 0; i < PelletCount; i++)       // dla każdego pocisku
            {
                float rndOffset = rnd.Next(-Spread, Spread);     // Oblicz losowe odchylenie zgodne z Spread
                float baseAngle = (float)Math.Atan2(destinationPosition.Y - sourcePosition.Y, destinationPosition.X - sourcePosition.X);        // Oblicz startowy kąt między pozycją gracza i myszki

                double shootAngle = baseAngle + rndOffset;       // Oblicz kąt strzału z odchyleniem

                // Sprawdzanie kolizji
                bool colided = false;
                int distanceTravelled = 0;
                double xToCheck = sourcePosition.X;
                double yToCheck = sourcePosition.Y;
                while (!colided)
                {
                    foreach (var mapObj in mapObjects)
                    {
                        // Kolizja ze scianą, oknem, drzwiami
                        if(mapObj.Hitbox.Contains((int)xToCheck, (int)yToCheck))
                        {
                            colided = true;
                            return;
                        }
                    }

                    if (!colided)
                    {
                        foreach (var entity in entities)
                        {
                            if (entity.Contains((int)xToCheck, (int)yToCheck))
                            {
                                //entity.Hurt(Damage);        // Dodać metode do przeciwników która odejmuje zdrowie
                                colided = true;
                                return;
                            }
                        }
                    }

                    

                    // Przesuń sprawdzany punkt
                    xToCheck = xToCheck + Math.Cos(shootAngle) * 10;
                    yToCheck = yToCheck + Math.Sin(shootAngle) * 10;
                }
            }
        }
    }
}
