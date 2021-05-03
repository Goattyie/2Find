using System;
using SFML.Graphics;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(new Settings(1366, 768, "Two One", true, true), new EasyGameSettings());
        }
    }
}
