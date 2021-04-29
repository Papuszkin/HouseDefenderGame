using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseDefenderGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HouseDefenderGame.Classes.Gameplay
{
    class ZombieSpawner
    {
        public Vector2 Position { get; set; }
        public int cooldown { get; set; }

        public ZombieSpawner(Vector2 position, int cooldown)
        {
            Position = position;
            this.cooldown = cooldown;
        }

        public void SpawnZombie()
        {
            //spawnowanie ranodomych wartosci u zombie (sila, zdrowie szybkos), sprawdzenie cooldownu

        }
    }
}
