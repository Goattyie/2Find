using SFML.Graphics;
using System;
using System.IO;

namespace Game
{
    class World
    {
        public string[] GameField { get; set; }
        int[] Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private WorldTextures Block { get; set; } = new WorldTextures();
        public World() 
        {
            StreamReader sr = new StreamReader("Maps/Default.txt");
            File.ReadAllLines("Maps/Default.txt");
            GameField = new string[File.ReadAllLines("Maps/Default.txt").Length];
            string line;
            int i = 0;
            while((line = sr.ReadLine()) != null)
            {
                GameField[i] = line;
                i += 1;
            }
            Height = GameField.Length;
            Width = GameField[0].ToString().Length;

        }
        public World(string[][]map) { }
        public void Render(RenderWindow window, float[] Position)
        {
            for(int y = 0; y < this.Height; y++)
            {
                for(int x = 0; x < this.Width; x++)
                {
                    RectangleShape blck = Block.View(GameField[y].ToString()[x], x,y);
                    window.Draw(blck);
                }
            }
        }
    }
}
