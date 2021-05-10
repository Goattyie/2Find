using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    abstract class Enemy:Entity, IEnemy
    {
        public Hero HeroTarget { get; set; }
        protected Enemy() { }
        protected Enemy(string textureFile):base(textureFile) { }
        public bool VisibleTarget { get; set; } = false;
        public abstract void AI();
    }
}
