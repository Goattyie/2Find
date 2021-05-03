using System;
using SFML.Graphics;

namespace Game2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            Lobby lobby = new Lobby(new Settings(1366, 768, "Two One", true, true));
        }
    }
}
