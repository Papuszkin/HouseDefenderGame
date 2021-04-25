using HouseDefenderGame.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HouseDefenderGame.Classes.Gameplay
{
    public class Gun
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public int PelletCount { get; set; }
        public int Spread { get; set; }
        public int RateOfFire { get; set; }
        public int AmmoCount { get; set; }

        public Gun(string name, int damage, int pelletCount, int spread, int rateOfFire)
        {
            Name = name;
            Damage = damage;
            PelletCount = pelletCount;
            Spread = spread;
            RateOfFire = rateOfFire;
            AmmoCount = 300;
        }
    }
}
