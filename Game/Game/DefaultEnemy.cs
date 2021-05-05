﻿using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class DefaultEnemy : Enemy
    {

        DefaultEnemy() { }
        public DefaultEnemy(string texture, string[] gamefield, float scale):base(texture, scale)
        {
            GameField = gamefield;
        }

        public void AI()
        {
            
        }
    }
}
