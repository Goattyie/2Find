using System;
using SFML.Graphics;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings settings= new Settings(1366, 768, "Two One", true, true); //в будущем считывание настроек из сохраненного файла
            MainMenu menu = new MainMenu(settings);
        }
    }
}
