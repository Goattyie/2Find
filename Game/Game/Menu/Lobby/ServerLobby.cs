using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using SFML.System;
using SFML.Window;

namespace Game
{
    class ServerLobby : IWindow
    {
        public RenderWindow Window { get; set; }
        Button Start { get; set; }
        Button Cancel { get; set; }
        Label Status { get; set; }
        Label IpLabel { get; set; }
        Label ModeHeader { get; set; }
        Label CurrentMode { get; set; }
        Label GameResult { get; set; }
        Button ModeChange { get; set; }
        Sprite Background { get; set; } = new Sprite();
        Game Game { get; set; }
        public Connection Connection { get; set; }
        LinkedList<string> Modes { get; set; }
        bool ButtonisDown { get; set; }
        bool Exit { get; set; }

        public ServerLobby(RenderWindow window)
        {
            Window = window;
            Window.Closed += WindowClose;
            Window.TextEntered += Window_TextEntered;
            Connection = new Connection();
            Background.Texture = new Texture("GameTextures/background.png");
            Background.Scale = new Vector2f((float)IWindow.Settings.WindowWidth / (float)1366, (float)IWindow.Settings.WindowHeight / (float)768);
            Modes = new LinkedList<string>(new[] { "Лёгкий", "Средний", "Сложный" });
            SetLabels();
            SetButtons();
        }

        private void SetLabels()
        {
            ModeHeader = new Label(40, new Vector2f(IWindow.Settings.WindowWidth / 3, IWindow.Settings.WindowHeight / 2));
            ModeHeader.Text.DisplayedString = "Выбор уровня сложности";
            CurrentMode = new Label(40, new Vector2f(ModeHeader.Text.Position.X + 150, ModeHeader.Text.Position.Y + 100));
            CurrentMode.Text.DisplayedString = Modes.First.Value;
            Status = new Label(40, new Vector2f(IWindow.Settings.WindowWidth / 3, 125));
            Status.Text.DisplayedString = "Ожидание второго игрока...";
            GameResult= new Label(40, new Vector2f(IWindow.Settings.WindowWidth / 2.28f, IWindow.Settings.WindowHeight / 3));
            IpLabel = new Label(45, new Vector2f(25, 25));
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
            Connection.StartLobbyThread();
            while (Window.IsOpen && !Exit)
            {
                Window.DispatchEvents();
                Window.Clear();
                Window.Draw(Background);
                CheckStatus();
                Window.Draw(IpLabel.Text);
                if (Game == null)
                {
                    Window.Draw(ModeHeader.Text);
                    Window.Draw(CurrentMode.Text);
                    ModeChange.Draw(Window);
                }
                else
                    Window.Draw(GameResult.Text);
                Start.Draw(Window);
                Cancel.Draw(Window);
                ButtonActions();
                Window.Display();
            }
            Connection.ThreadStop = true;
            Exit = false;
        }

        private void CheckStatus()
        {
            if (Connection.Connected && Status.Text.DisplayedString == "Ожидание второго игрока...")
            {
                Status.Text.DisplayedString = "Второй игрок подключен";
                Status.Text.FillColor = Color.Green;
            }
            else if (!Connection.Connected && Status.Text.DisplayedString == "Второй игрок подключен")
            {
                Status.Text.DisplayedString = "Ожидание второго игрока...";
                Status.Text.FillColor = Color.White;
            }
            Window.Draw(Status.Text);
        }

        private void Window_TextEntered(object sender, TextEventArgs e)
        {
            char key = e.Unicode.Cast<char>().First();
            if (key == 13)
                StartGame();
            else if (key == 27)
            {
                Game = null;
                Exit = true;
            }
        }

        public void RefreshView(Vector2f scale)
        {
            Background.Scale = scale;
            ModeHeader.Text.Position = new Vector2f(IWindow.Settings.WindowWidth / 3, IWindow.Settings.WindowHeight / 3);
            CurrentMode.Text.Position = new Vector2f(ModeHeader.Text.Position.X + 150, ModeHeader.Text.Position.Y + 100);
            Status.Text.Position = new Vector2f(IWindow.Settings.WindowWidth / 3, 125);
            Start.Sprite.Position = new Vector2f(IWindow.Settings.WindowWidth - 325, IWindow.Settings.WindowHeight - 105);
            Cancel.Sprite.Position = new Vector2f(25, IWindow.Settings.WindowHeight - 105);
            ModeChange.Sprite.Position = new Vector2f(CurrentMode.Text.Position.X + 150, CurrentMode.Text.Position.Y);
        }

        private void SetGameSettings()
        {
            switch (CurrentMode.Text.DisplayedString)
            {
                case "Лёгкий":
                    Connection.Send(1);
                    Game = new Game(Window, new EasyGameSettings(), Connection);
                    break;
                case "Средний":
                    Connection.Send(2);
                    Game = new Game(Window, new EasyGameSettings(), Connection); // Средние настройки
                    break;
                case "Сложный":
                    Connection.Send(3);
                    Game = new Game(Window, new EasyGameSettings(), Connection); // Сложные настройки
                    break;
            }
        }

        private void StartGame()
        {
            if (!Connection.Connected)
            {
                Status.Text.FillColor = Color.Red;
                return;
            }
            Connection.SendStart();
            if (Game == null)
                SetGameSettings();
            Game.View();
            ShowGameResults();
            Connection.StartLobbyThread();
        }
        private void ShowGameResults()
        {
            if (Game.GameStatus == 1)
            {
                GameResult.Text.DisplayedString = "Победа";
                GameResult.Text.FillColor = Color.Green;
            }
            else if (Game.GameStatus == 2)
            {
                GameResult.Text.DisplayedString = "Поражение";
                GameResult.Text.FillColor = Color.Red;
            }
            else
                Game = null;
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
                else if (Start.isPicked && !ButtonisDown)
                {
                    StartGame();
                    ButtonisDown = true;
                }
                else if (Cancel.isPicked && !ButtonisDown)
                {
                    Game = null;
                    Exit = true;
                    ButtonisDown = true;
                }
            }
            else
                ButtonisDown = false;
        }

        private void WindowClose(object sender, EventArgs e)
        {
            Window.Close();
        }
    }
}
