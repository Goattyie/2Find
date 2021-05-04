using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    interface IEnemy
    {
        string[] GameField { get; set; }
        Hero HeroTarget { get; set; }
        void AI();
    }
}
