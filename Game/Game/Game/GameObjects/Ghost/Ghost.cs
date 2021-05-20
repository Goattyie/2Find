using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Ghost : Enemy
    {
        
        protected override float Speed { get; set; } = 1.1f;
        protected override float MaxTriggerTime { get; set; } = 10f;

        public Ghost(string texture, string[] gamefield) : base(texture)
        {
            if(Gamefield == null)
                Gamefield = new string[gamefield.Length];
            Array.Copy(gamefield, Gamefield, gamefield.Length);
           
        }

        public override void AI()
        {
            if (HeroTarget == null || this.Center.X == HeroTarget.Center.X && this.Center.Y == HeroTarget.Center.Y)
                return;

            float KatetX = -this.Center.X + HeroTarget.Center.X;
            float KatetY = -this.Center.Y + HeroTarget.Center.Y;
            double Gipotenuza = Math.Sqrt(Math.Pow(KatetX, 2) + Math.Pow(KatetY, 2));
            double x = this.Center.X + Speed * KatetX / (float)Gipotenuza;
            double y = this.Center.Y + Speed * KatetY / (float)Gipotenuza;
            Sprite.Position = new Vector2f((float)x - Width/2, (float)y - Height/2);

        } 
    }
}
