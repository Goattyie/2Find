using System;
using SFML.Graphics;
using SFML.System;

namespace Game
{
    class WorldTextures
    {
        Texture Texture { get; set; }
		RectangleShape Block { get; set; } = new RectangleShape();

		Texture roof1, roof2, roof3, roof4, roof1_2, roof2_2, roof5, roof5_1,
			ground1, ground1_2, ground2, ground2_2, ground3, ground3_2, ground4,
			ground4_2, ground5, ground6, ground7, ground7_2, ground8, ground9,
			wall;

		int Angle { get; set; }
        public static int[] BlockSize { get; set; } = new int[] { 120, 120 };
        public WorldTextures(float scale) 
		{
			roof1 = new Texture("MapTextures/roof1.png");
			roof1_2 = new Texture("MapTextures/roof1.2.png");
			roof2 = new Texture("MapTextures/roof2.png");
			roof2_2 = new Texture("MapTextures/roof2.2.png");
			roof3 = new Texture("MapTextures/roof3.png");
			roof4 = new Texture("MapTextures/roof4.png");
			roof5 = new Texture("MapTextures/roof5.png");
			roof5_1 = new Texture("MapTextures/roof5.1.png");
			wall = new Texture("MapTextures/wall.png");
			ground1 = new Texture("MapTextures/ground1.png");
			ground1_2 = new Texture("MapTextures/ground1.2.png");
			ground2 = new Texture("MapTextures/ground2.png");
			ground2_2 = new Texture("MapTextures/ground2.2.png");
			ground3 = new Texture("MapTextures/ground3.png");
			ground3_2 = new Texture("MapTextures/ground3.2.png");
			ground4 = new Texture("MapTextures/ground4.png");
			ground4_2 = new Texture("MapTextures/ground4.2.png");
			ground5 = new Texture("MapTextures/ground5.png");
			ground6 = new Texture("MapTextures/ground6.png");
			ground7 = new Texture("MapTextures/ground7.png");
			ground7_2 = new Texture("MapTextures/ground7.2.png");
			ground8 = new Texture("MapTextures/ground8.png");
			ground9 = new Texture("MapTextures/ground9.png");
			BlockSize[0] = (int)(BlockSize[0] * scale);
			BlockSize[1] = (int)(BlockSize[1] * scale);
		}

        public RectangleShape View(char symbol, int x, int y) 
        {
			SetTexture(symbol);
			
			Block.Texture = Texture;
			Block.Size = new Vector2f(BlockSize[0], BlockSize[1]); 
			Block.Position = new Vector2f(x * BlockSize[0], y * BlockSize[1]);
			if (Angle != 0)
			{
				if (Angle == -90)
					Block.Origin = new Vector2f(BlockSize[0], 0);
				else if (Angle == 90)
					Block.Origin = new Vector2f(0, BlockSize[1]);
				else
					Block.Origin = new Vector2f(BlockSize[0], BlockSize[1]);
				Block.Rotation = Angle;
			}
			else
			{
				Block.Origin = default; //Хз надо это или нет
				Block.Rotation = default;
			}
			return Block;
        }
        public void SetTexture(char symbol)
        {
			if (symbol == '0' || symbol == '1')
			{
				Texture = roof1;
				if (symbol == '1')
					Angle = -90;
			}
			else if (symbol == '2' || symbol == '3')
			{
				Texture = roof1_2;
				if (symbol == '3')
					Angle = -90;
			}
			else if (symbol == '4' || symbol == '5')
			{
				Texture = roof2;
				if (symbol == '5')
					Angle = -90;
			}
			else if (symbol == '6' || symbol == '7')
			{
				Texture = roof2_2;
				if (symbol == '7')
					Angle = -90;
			}
			else if (symbol == '8' || symbol == '9')
			{
				Texture = roof3;
				if (symbol == '9')
					Angle = -90;
			}
			else if (symbol == 'a' || symbol == 'A')
			{
				Texture = roof4;
				if (symbol == 'A')
					Angle = -90;
			}
			else if (symbol == 'b' || symbol == 'B')
			{
				Texture = roof5;
				if (symbol == 'B')
					Angle = -90;
			}
			else if (symbol == 'c' || symbol == 'C')
			{
				Texture = roof5_1;
				if (symbol == 'C')
					Angle = -90;
			}
			else if (symbol == 'd' || symbol == 'D')
			{
				Texture = wall;
				if (symbol == 'D')
					Angle = -90;
			}
			else if (symbol == 'd' || symbol == 'D')
			{
				Texture = wall;
				if (symbol == 'D')
					Angle = -90;
			}
			else if (symbol == 'f' || symbol == 'F')
			{
				Texture = ground1;
				if (symbol == 'F')
					Angle = -90;
			}
			else if (symbol == 'g' || symbol == 'G')
			{
				Texture = ground1_2;
				if (symbol == 'G')
					Angle = -90;
			}
			else if (symbol == 'h' || symbol == 'H' || symbol == 'х' || symbol == 'Х')
			{
				Texture = ground2;
				if (symbol == 'H')
					Angle = -90;
				else if (symbol == 'х')
					Angle = 180;
				else if (symbol == 'Х')
					Angle = 90;
			}
			else if (symbol == 'i' || symbol == 'I' || symbol == 'и' || symbol == 'И')
			{
				Texture = ground1_2;
				if (symbol == 'I')
					Angle = -90;
				else if (symbol == 'и')
					Angle = 180;
				else if (symbol == 'И')
					Angle = 90;
			}
			else if (symbol == 'j' || symbol == 'J')
			{
				Texture = ground3;
				if (symbol == 'J')
					Angle = -90;
			}
			else if (symbol == 'k' || symbol == 'K')
			{
				Texture = ground3_2;
				if (symbol == 'K')
					Angle = -90;
			}
			else if (symbol == 'l' || symbol == 'L' || symbol == 'л' || symbol == 'Л')
			{
				Texture = ground4;
				if (symbol == 'L')
					Angle = -90;
				else if (symbol == 'л')
					Angle = 180;
				else if (symbol == 'Л')
					Angle = 90;
			}
			else if (symbol == 'm' || symbol == 'M' || symbol == 'м' || symbol == 'М')
			{
				Texture = ground4_2;
				if (symbol == 'M')
					Angle = -90;
				else if (symbol == 'м')
					Angle = 180;
				else if (symbol == 'М')
					Angle = 90;
			}
			else if (symbol == 'n' || symbol == 'N' || symbol == 'н' || symbol == 'Н')
			{
				Texture = ground5;
				if (symbol == 'N')
					Angle = -90;
				else if (symbol == 'н')
					Angle = 180;
				else if (symbol == 'Н')
					Angle = 90;
			}
			else if (symbol == 'o' || symbol == 'O' || symbol == 'о' || symbol == 'О')
			{
				Texture = ground6;
				if (symbol == 'O')
					Angle = -90;
				else if (symbol == 'о')
					Angle = 180;
				else if (symbol == 'О')
					Angle = 90;
			}
			else if (symbol == 'p' || symbol == 'P' || symbol == 'п' || symbol == 'П')
			{
				Texture = ground7;
				if (symbol == 'P')
					Angle = -90;
				else if (symbol == 'п')
					Angle = 180;
				else if (symbol == 'П')
					Angle = 90;
			}
			else if (symbol == 'r' || symbol == 'R' || symbol == 'р' || symbol == 'Р')
			{
				Texture = ground7_2;
				if (symbol == 'R')
					Angle = -90;
				else if (symbol == 'р')
					Angle = 180;
				else if (symbol == 'Р')
					Angle = 90;
			}
			else if (symbol == 'q' || symbol == 'Q')
			{
				Texture = ground8;
				if (symbol == 'Q')
					Angle = -90;
			}
			else if (symbol == 's' || symbol == 'S')
			{
				Texture = ground9;
				if (symbol == 'S')
					Angle = 180;
			}
		}
    }
}
