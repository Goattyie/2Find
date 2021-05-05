using SFML.Graphics;
using System;
using SFML.Window;
using SFML.System;

namespace Game
{
    class Game : IWindow
    {
        public int GameStatus { get; set; } = 0;
        public Settings Settings { get; set; }
        private IGameSettings GameSettings { get; set; }
        public RenderWindow Window { get; set; }
        Hero[] Heroes { get; set; }
        Enemy[] Enemies { get; set; }
        World Map { get; set; }
        Game() { }
        public Game(Settings settings, IGameSettings gameSettings) 
        {
            GameSettings = gameSettings;
            Settings = settings;
            Window = new RenderWindow(new SFML.Window.VideoMode((uint)Settings.WindowWidth, (uint)Settings.WindowHeight), Settings.WindowName);
            Window.SetVerticalSyncEnabled(Settings.VSync);

            //События
            Window.Closed += WindowClose;
            //Window.KeyPressed += KeyController;

            Map = new World(gameSettings);
            SpawnEntityes();
            View();
        }
        private void SpawnEntityes()
        {
            Heroes = new Hero[] { new Hero("Hero1.png", GameSettings.Scale) };
            for(int i = 0; i < Heroes.Length; i++)
            {
                //Heroes[i].RandomSpawn(Map.GameField);
                Heroes[i].Spawn(1, 1);
            }

            Enemies = new Enemy[GameSettings.CountDefaultEnemy];
            for(int i = 0; i < GameSettings.CountDefaultEnemy; i++) 
            {
                Enemies[i] = new DefaultEnemy("DefaultEnemy.png", Map.GameField, GameSettings.Scale);
                Enemies[i].Spawn(4, 4);
            }

        }
        public void View()
        {

            
            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                Window.Clear();
                KeyController();
                RenderMap();//Отрисовка карты
                RenderHeroes(); //Показывать героев
                RenderEnemies(); //Показывать врагов
                Window.Display();
            }
        }
        private void RenderHeroes()
        {
            foreach (Hero hero in Heroes)
            {
                hero.Collision(Map.GameField[hero.Position[1] + 1][hero.Position[0]], Map.GameField[hero.Position[1]][hero.Position[0] + 1], Map.GameField[hero.Position[1] - 1][hero.Position[0]], Map.GameField[hero.Position[1]][hero.Position[0] - 1]);
                Window.Draw(hero.Hitbox);
                Window.Draw(hero.Sprite);
                foreach (RectangleShape rs in hero.CollisionBlock)
                {
                    if(rs != null)
                        Window.Draw(rs);
                }
                foreach (Enemy enemy in Enemies)
                {
                    if (hero.SeeOtherEntity(enemy, Map.GameField))
                        Window.Draw(enemy.Sprite);
                }
            }
        }
        private void RenderMap()
        {
            //WorldTextures Block = new WorldTextures();
            foreach (Hero hero in Heroes)
            {
                Map.Render(Window, hero.Position);
            }
            
        }
        private void RenderEnemies()
        {
            for(int i = 0; i < Enemies.Length; i++)
            {
                //Console.WriteLine(Enemies[i].SeeOtherEntity((Entity)Enemies[i], Heroes[0], Map.GameField));
            }
        }

        public void WindowClose(object sender, EventArgs e)
        {
            Window.Close();
        }
        private void KeyController()
        {
            if(Keyboard.IsKeyPressed(Keyboard.Key.A)) { Heroes[0].Left(); }
            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) { Heroes[0].Forward(); }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) { Heroes[0].Right(); }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) { Heroes[0].Back(); }
        }
    }
}
