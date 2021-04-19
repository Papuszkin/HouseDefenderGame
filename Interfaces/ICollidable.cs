using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseDefenderGame.Interfaces
{
    public interface ICollidable
    {
        public Rectangle Hitbox { get; set; }
    }
}
