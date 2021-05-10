using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
	class Step
	{
		public Step() {
			
		}
		public int X, Y, Cost;
		public Step(bool first)
        {
			X = 0;
			Y = 0;
			Cost = -10;
        }
		Step(int x, int y) { X = x; Y = y; Cost = 0; }
		public Step(int x, int y, Step step)
		{
			X = x;
			Y = y;
			PastStep = step;
			Cost = PastStep.Cost + 10;
		}
		public Step PastStep;
		public static void DeleteStep(ref List<Step> steps, Step step1)
		{
			for (int i = 0; i < steps.Count; i++)
			{
				if (steps[i].X == step1.X && steps[i].Y == step1.Y)
					steps.RemoveAt(i);
			}
		}
		public static int MinCost(Step step, int[] Position)
		{
			return 10 * (Math.Abs(step.Y - Position[1]) + Math.Abs(step.X - Position[0])) + step.Cost;
		}
	}
}
