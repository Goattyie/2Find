using SFML.Graphics;
using System;
using SFML.Window;

namespace Game
{
    class Game : IWindow
    {
        public int GameStatus { get; set; } = 0;
        public Settings Settings { get; set; }
        public RenderWindow Window { get; set; }
        Hero[] heroes { get; set; }
        Game() { }
        public Game(Settings settings, IGameSettings gameSettings) 
        {
            Settings = settings;
            Window = new RenderWindow(new SFML.Window.VideoMode((uint)Settings.WindowWidth, (uint)Settings.WindowHeight), Settings.WindowName);
            Window.SetVerticalSyncEnabled(Settings.VSync);

            //События
            Window.Closed += WindowClose;
            //Window.KeyPressed += KeyController;
            

            View();
        }
        public void View()
        {
            heroes = new Hero[] { new Hero("Hero1.png") };
            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                Window.Clear(Color.Black);
                KeyController();
                ViewHeroes(); //Показывать героев
                Window.Display();
            }
        }
        public void ViewHeroes()
        {
            foreach (Hero hero in heroes)
                Window.Draw(hero.Sprite);
        }

        public void WindowClose(object sender, EventArgs e)
        {
            Window.Close();
        }
        public void KeyController()
        {
            if(Keyboard.IsKeyPressed(Keyboard.Key.A)) { heroes[0].Left(); }
            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) { heroes[0].Forward(); }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) { heroes[0].Right(); }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) { heroes[0].Back(); }
        }
    }
}
