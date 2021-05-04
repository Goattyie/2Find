using SFML.Graphics;
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
        public string[] GameField { get; set; }
        public Hero HeroTarget { get; set; }

        DefaultEnemy() { }
        public DefaultEnemy(string texture, string[] gamefield)
        {
            GameField = gamefield;
            Model = new Texture($"GameTextures/{texture}");
            Sprite.Texture = Model;
            Spawn(2, 1);

        }

        public void AI()
        {
            
        }
    }
}
