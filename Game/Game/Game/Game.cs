﻿using SFML.Graphics;
using System;
using SFML.Window;
using SFML.System;

namespace Game
{
    class Game : IWindow
    {
        public int GameStatus { get; set; } = 0;
        private IGameSettings GameSettings { get; set; }
        public RenderWindow Window { get; set; }
        private Connection Connection { get; set; }
        Hero[] Heroes { get; set; }
        Enemy[] Enemies { get; set; }
        World Map { get; set; }
        View Camera1 { get; set; }
        View Camera2 { get; set; }
        Clock Clock { get; set; }
        float time { get; set; }
        Game() { }
        public Game(RenderWindow window, IGameSettings gameSettings, Connection connection)
        {
            GameSettings = gameSettings;
            Window = window;
            Connection = connection;
            Window.Closed += WindowClose;
            Clock = new Clock();
            Map = new World(gameSettings);
            SpawnEntityes();
            SetCameras();
            View();
        }
        private void SpawnEntityes()
        {
            Heroes = new Hero[] { new Hero("Hero1.png"), new Hero("Hero1.png") };
            Heroes[0].RandomSpawn(Map.GameField);
            Enemies = new Enemy[GameSettings.CountDefaultEnemy];
            for (int i = 0; i < GameSettings.CountDefaultEnemy; i++)
            {
                Enemies[i] = new DefaultEnemy("DefaultEnemy.png", Map.GameField);
                Enemies[i].Spawn(6, 6);
            }

        }

        private void SetCameras()
        {
            Camera1 = new View(new Vector2f(IWindow.Settings.WindowWidth / 4.0f, IWindow.Settings.WindowHeight / 2.0f),
                new Vector2f(IWindow.Settings.WindowWidth / 2.0f, IWindow.Settings.WindowHeight));
            Camera2 = new View(new Vector2f(IWindow.Settings.WindowWidth - (IWindow.Settings.WindowWidth / 4.0f),
                IWindow.Settings.WindowHeight / 2.0f), new Vector2f(IWindow.Settings.WindowWidth / 2.0f, IWindow.Settings.WindowHeight));
            Camera1.Viewport = new FloatRect(0f, 0f, 0.5f, 1f);
            Camera2.Viewport = new FloatRect(0.5f, 0f, 0.5f, 1f);
        }

        public void View()
        {
            Connection.StartReceiving();
            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                time = (float)Clock.ElapsedTime.AsMicroseconds()/10000f;
                Clock.Restart();
                KeyController();
                Camera1.Center = Heroes[0].Center;
                Connection.Send(Heroes[0].Sprite.Position.X, Heroes[0].Sprite.Position.Y);
                Window.Clear();
                Window.SetView(Camera1);
                RenderMap(0);
                Heroes[0].Collision(Map.GameField[Heroes[0].Position[1] + 1][Heroes[0].Position[0]], Map.GameField[Heroes[0].Position[1]][Heroes[0].Position[0] + 1], 
                    Map.GameField[Heroes[0].Position[1] - 1][Heroes[0].Position[0]], Map.GameField[Heroes[0].Position[1]][Heroes[0].Position[0] - 1]);
                RenderHeroes(false);
                RenderEnemies(0);
                Heroes[1].Sprite.Position = Connection.ReceivedPos;
                Camera2.Center = Heroes[1].Center;
                Window.SetView(Camera2);
                RenderMap(1);
                RenderHeroes(true);
                RenderEnemies(1);
                Window.Display();
            }
        }
        private void RenderHeroes(bool hero)
        {
            int i = Convert.ToInt32(hero);
            Window.Draw(Heroes[i].Hitbox);
            Window.Draw(Heroes[i].Sprite);
            if (Heroes[i].SeeOtherEntity(Heroes[Convert.ToInt32(!hero)], Map.GameField))
                Window.Draw(Heroes[Convert.ToInt32(!hero)].Sprite);
            foreach (RectangleShape rs in Heroes[i].CollisionBlock)
            {
                if (rs != null)
                    Window.Draw(rs);
            }
            foreach (Enemy enemy in Enemies)
            {
                if (Heroes[i].SeeOtherEntity(enemy, Map.GameField))
                    Window.Draw(enemy.Sprite);
            }

        }
        private void RenderMap(int i)
        {
            Map.Render(Window, Heroes[i].Position);
        }

        private void RenderEnemies(int hero)
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i].HeroTarget == null && Enemies[i].SeeOtherEntity(Heroes[hero], Map.GameField))
                    Enemies[i].HeroTarget = Heroes[hero];
                Enemies[i].AI();
            }
        }

        public void WindowClose(object sender, EventArgs e)
        {
            Window.Close();
        }
        private void KeyController()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) { Heroes[0].Left(time); }
            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) { Heroes[0].Forward(time); }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) { Heroes[0].Right(time); }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) { Heroes[0].Back(time); }
        }
    }
}
