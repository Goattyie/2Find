﻿using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;

namespace Game
{
    class CreateServer : IWindow
    {
        public RenderWindow Window { get; set; }
        Button Start { get; set; }
        Button Cancel { get; set; }
        Label Status { get; set; }
        Label IpLabel { get; set; }
        Label ModeHeader { get; set; }
        Label CurrentMode { get; set; }
        Button ModeChange { get; set; }
        Sprite Background { get; set; } = new Sprite();
        Connection Connection { get; set; }
        LinkedList<string> Modes { get; set; }
        bool ButtonisDown { get; set; }
        bool Canceled { get; set; }
        
        public CreateServer(RenderWindow window)
        {
            Window = window;
            Window.TextEntered += Window_TextEntered;
            Connection = new Connection();
            Connection.ReceiveIp();
            Background.Texture = new Texture("GameTextures/background.png");
            Background.Scale = new Vector2f((float)IWindow.Settings.WindowWidth / (float)1366, (float)IWindow.Settings.WindowHeight / (float)768);
            Modes = new LinkedList<string>(new[] { "Лёгкий", "Средний","Сложный" });
            SetLabels();
            SetButtons();
            View();
        }

        private void SetLabels()
        {
            ModeHeader = new Label("19702.otf", 40, new Vector2f(IWindow.Settings.WindowWidth / 3, IWindow.Settings.WindowHeight / 3), 4, Color.White);
            ModeHeader.Text.DisplayedString = "Выбор уровня сложности";
            CurrentMode = new Label("19702.otf", 40, new Vector2f(ModeHeader.Text.Position.X + 150, ModeHeader.Text.Position.Y + 100), 4, Color.White);
            CurrentMode.Text.DisplayedString = Modes.First.Value;
            Status = new Label("19702.otf", 40, new Vector2f(IWindow.Settings.WindowWidth / 3, 125), 4, Color.White);
            Status.Text.DisplayedString = "Ожидание второго игрока...";
            IpLabel = new Label("19702.otf", 45, new Vector2f(25, 25), 4, Color.White);
            IpLabel.Text.DisplayedString = "IP: " + Connection.Ip.ToString();
        }
        private void SetButtons()
        {
            Start = new Button("start.png", new Vector2f(IWindow.Settings.WindowWidth - 325, IWindow.Settings.WindowHeight - 105));
            Cancel = new Button("back.png", new Vector2f(25, IWindow.Settings.WindowHeight - 105));
            ModeChange = new Button("change.png", new Vector2f(CurrentMode.Text.Position.X + 150, CurrentMode.Text.Position.Y));
        }

        public void View()
        {
            while (!Canceled)
            {
                
                Window.DispatchEvents();
                Window.Clear();
                Window.Draw(Background);
                Window.Draw(IpLabel.Text);
                Window.Draw(Status.Text);
                Window.Draw(ModeHeader.Text);
                Window.Draw(CurrentMode.Text);
                ModeChange.Draw(Window);
                Start.Draw(Window);
                Cancel.Draw(Window);
                ButtonActions();
                Window.Display();
            }
        }

        private void Window_TextEntered(object sender, TextEventArgs e)
        {
            char key = e.Unicode.Cast<char>().First();
            if (key == 13)
            {
                StartGame();
            }
            else if (key == 27)
            {
                Canceled = true;
            }
        }

        private void StartGame()
        {
            Game game = new Game(Window, new EasyGameSettings());
        }

        private void ButtonActions()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (ModeChange.isPicked && !ButtonisDown)
                {
                    if (Modes.Find(CurrentMode.Text.DisplayedString).Next != null)
                        CurrentMode.Text.DisplayedString = Modes.Find(CurrentMode.Text.DisplayedString).Next.Value;
                    else
                        CurrentMode.Text.DisplayedString = Modes.First.Value;
                    ButtonisDown = true;
                }
                else if (Start.isPicked)
                {
                    StartGame();
                }
                else if (Cancel.isPicked)
                {
                    Canceled = true;
                }
            }
            else
                ButtonisDown = false;
        }
    }
}
