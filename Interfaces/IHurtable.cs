using System;
using System.Collections.Generic;
using System.Text;

namespace HouseDefenderGame.Interfaces
{
    public interface IHurtable
    {
        public int Health { get; set; }

        public void Hurt(int damage);
    }
}
