using SFML.Graphics;
using System;
using System.IO;

namespace Game
{
    class World
    {
        public string[] GameField { get; set; }
        public static int[][] ClosedSpawnPoint { get; set; }
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

        }
        public World(string[][]map) { }
        public void Render(RenderWindow window, int[] Position)
        {
            for(int y = Position[1] - 10; y <= Position[1] + 10; y++)
            {
                for(int x = Position[0] - 10; x <= Position[0] + 10; x++)
                {
                    if (x < 0 || y < 0 || y >= GameField.Length || x >= GameField[y].Length)
                        continue;
                    RectangleShape blck = Block.View(GameField[y].ToString()[x], x,y);
                    window.Draw(blck);
                }
            }
        }
    }
}
