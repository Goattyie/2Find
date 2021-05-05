using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    abstract class Enemy:Entity, IEnemy
    {
        public string[] GameField { get; set; }
        public Hero HeroTarget { get; set; }
        protected Enemy() { }
        protected Enemy(string textureFile, float scale):base(textureFile, scale) { }
        public bool VisibleRarget { get; set; } = false;
        public void AI() { }
    }
}
