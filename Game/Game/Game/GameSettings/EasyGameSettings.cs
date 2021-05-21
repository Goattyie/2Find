using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class EasyGameSettings : IGameSettings
    {
        public int CountDefaultEnemy { get; set; } = 12;
        public int CountGhost { get; set; } = 12;
        public int VisibleRange { get; set; } = 5;
    }
}
