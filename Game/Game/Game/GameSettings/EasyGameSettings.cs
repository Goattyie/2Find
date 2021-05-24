using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class EasyGameSettings : IGameSettings
    {
        public int CountDefaultEnemy { get; set; } = 5;
        public int CountGhost { get; set; } = 5;
    }
}
