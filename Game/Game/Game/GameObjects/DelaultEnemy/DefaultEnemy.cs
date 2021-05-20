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
        static string[] Gamefield { get; set; } //Карта
        private string[] Map { get; set; }//Копия карты для алгоритма
        private List<Step> Steps = new List<Step>();
        private readonly Clock StopClock = new Clock();
        private Time StopTime { get; set; } //Время перерыва
        protected override float Speed { get; set; } = 12;
        protected override float MaxTriggerTime { get; set; } = 4f;

        DefaultEnemy() { }
        public DefaultEnemy(string texture, string[] gamefield):base(texture)
        {
            Gamefield = new string[gamefield.Length];
            Array.Copy(gamefield, Gamefield, gamefield.Length);
            Map = new string[Gamefield.Length];
            StopTime = StopClock.Restart();
        }

        public override void AI()
        {
            StopTime = StopClock.ElapsedTime;
            if (HeroTarget == null || StopTime.AsSeconds() < 10/Speed || (this.Position[0] == HeroTarget.Position[0] && this.Position[1] == HeroTarget.Position[1]))
                return;

            StopTime = StopClock.Restart();
            Array.Copy(Gamefield, Map, Gamefield.Length);
            char[] charStr = Map[HeroTarget.Position[1]].ToCharArray(); //строку в массив символов
            charStr[HeroTarget.Position[0]] = 'Z';
            Map[HeroTarget.Position[1]] = new string(charStr); //обратно
            AStar(new Step(this.Position[0], this.Position[1], new Step(true)));
        }
        void AStar(Step step)
        {
            char[] charStr = Map[step.Y].ToCharArray(); //строку в массив символов
            charStr[step.X] = 'z';
            Map[step.Y] = new string(charStr); //обратно
            Step.DeleteStep(ref Steps, step);

            for (int i = step.Y - 1; i <= step.Y + 1; i++)
            {
                for (int j = step.X - 1; j <= step.X + 1; j++)
                {
                    if (WorldTextures.IsWay(Map[i][j]) /*&& (i == step.Y || j == step.X)*/)
                        Steps.Add(new Step(j, i, step));
                    if (Map[i][j] == 'Z')
                    {
                        Step final = new Step(j, i, step);
                        while (final.PastStep.Cost != 0)
                        {
                            final = final.PastStep;
                        }
                        Steps.Clear();
                        Spawn(final.X, final.Y);
                        return;
                    }
                }
            }

            if (Steps.Count != 0)
            {
                int[] min = new int[] { Steps[0].Y, Steps[0].X, Steps[0].Cost };
                int min_value = Step.MinCost(Steps[0], HeroTarget.Position);
                Step min_stap = Steps[0];
                int z = 0;
                foreach (Step stepp in Steps)
                {
                    if (Step.MinCost(stepp, HeroTarget.Position) <= min_value)
                    {
                        min_stap = stepp;
                        min_value = Step.MinCost(stepp, HeroTarget.Position);
                    }

                }
                AStar(min_stap);
            }

        }
    }
}
