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
        Hero[] Heroes { get; set; }
        World Map { get; set; }
        Game() { }
        public Game(Settings settings, IGameSettings gameSettings) 
        {
            Settings = settings;
            Window = new RenderWindow(new SFML.Window.VideoMode((uint)Settings.WindowWidth, (uint)Settings.WindowHeight), Settings.WindowName);
            Window.SetVerticalSyncEnabled(Settings.VSync);

            //События
            Window.Closed += WindowClose;
            //Window.KeyPressed += KeyController;

            Map = new World();
            View();
        }
        public void View()
        {

            Heroes = new Hero[] { new Hero("Hero1.png") };
            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                Window.Clear();
                KeyController();
                RenderMap();//Отрисовка карты
                RenderHeroes(); //Показывать героев
                Window.Display();
            }
        }
        public void RenderHeroes()
        {
            foreach (Hero hero in Heroes)
            {
                hero.Collision();
                Window.Draw(hero.Sprite);
            }
        }
        public void RenderMap()
        {
            //WorldTextures Block = new WorldTextures();
            foreach (Hero hero in Heroes)
            {
                Map.Render(Window, hero.Position);
            }
            
        }

        public void WindowClose(object sender, EventArgs e)
        {
            Window.Close();
        }
        public void KeyController()
        {
            if(Keyboard.IsKeyPressed(Keyboard.Key.A)) { Heroes[0].Left(); }
            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) { Heroes[0].Forward(); }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) { Heroes[0].Right(); }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) { Heroes[0].Back(); }
        }
    }
}
