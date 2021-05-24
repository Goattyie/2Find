using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    interface IGameSettings
    {
        int CountDefaultEnemy { get; set; }
        int CountGhost { get; set; }
    }
}
