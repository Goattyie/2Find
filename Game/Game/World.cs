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
        private int RenderRange { get; set; }

        private WorldTextures Block { get; set; }
        public World(IGameSettings igs) 
        {
            Block = new WorldTextures();
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
            RenderRange = igs.VisibleRange;

        }
        public World(string[][]map) { }
        public void Render(RenderWindow window, int[] Position)
        {
            for(int y = 0; y < GameField.Length; y++)
            {
                for(int x = 0; x < GameField[y].Length; x++)
                {
                    RectangleShape blck = Block.View(GameField[y].ToString()[x], x,y);
                    window.Draw(blck);
                }
            }
        }
    }
}
