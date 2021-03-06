using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    abstract class Enemy : Entity, IEnemy
    {
        public static string[] Gamefield { get; set; } //Карта
        protected Time TriggerTime { get; set; }//Время работы AI после потери цели из виду
        protected abstract float MaxTriggerTime { get; set; } //Максимальное время, за которое может работать AI во время потери цели
        protected Clock Clock { get; set; }//Часы для обновления TriggerTime
        public Hero HeroTarget { get; set; }
        protected Enemy() { }
        protected Enemy(string textureFile):base(textureFile)
        { 
            this.Clock = new Clock(); TriggerTime = Clock.Restart();
            this.Sprite.Scale = new Vector2f(0.1953125f, 0.1953125f);//
            Width = 100;
            Height = 100;
            Size = new int[] { Width, Height };
        }

        public bool VisibleTarget { get; set; } = false;
        public abstract void AI(float time);
        public override bool SeeOtherEntity(Entity entity2, string[] VisibleChank)
        {
            if (HeroTarget == null)
                Clock.Restart();
            TriggerTime = Clock.ElapsedTime;
            bool SeeTarget = base.SeeOtherEntity(entity2, VisibleChank);
            if (SeeTarget)
                TriggerTime = Clock.Restart();
            else
            {
                if (TriggerTime.AsSeconds() >= MaxTriggerTime)
                    HeroTarget = null;
            }
            return SeeTarget;

        }
        public static Random RandomGenerator { get; set; }
        public override void RandomSpawn(string[] GameField)
        {
            int x = 0;
            int y = 0;
            while (true)
            {
                y = RandomGenerator.Next(1, GameField.Length - 2);
                x = RandomGenerator.Next(1, GameField[0].Length - 2);
                if (WorldTextures.IsWay(GameField[y][x]))
                    break;
            }
            Spawn(x, y);
        }
        public void SetSpeed(float time)
        {
            Console.WriteLine(time);
            Speed *= time/2;
        }
    }
}
