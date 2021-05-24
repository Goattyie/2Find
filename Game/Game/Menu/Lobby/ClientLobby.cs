using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Linq;

namespace Game
{
    class ClientLobby : IWindow
    {
        public RenderWindow Window { get; set; }
        TextBox TextBox { get; set; }
        Connection Connection { get; set; }
        Label IpLabel { get; set; }
        Label Status { get; set; }
        Label GameResult { get; set; }
        Sprite Background { get; set; } = new Sprite();
        Button Connect { get; set; }
        Button Cancel { get; set; }
        Game Game { get; set; }
        bool Exit { get; set; }
        bool EndEnter { get; set; }
        bool PaseChecker { get; set; }
        bool ButtonisDown { get; set; }

        public ClientLobby(RenderWindow window)
        {
            Window = window;
            Background.Texture = new Texture("GameTextures/background.png");
            Window.TextEntered += Window_TextEntered;
            Window.KeyPressed += Window_KeyPressed;
            Window.KeyReleased += Window_KeyReleased;
            Window.Closed += WindowClose;
            Background.Scale = new Vector2f((float)IWindow.Settings.WindowWidth / (float)1366, (float)IWindow.Settings.WindowHeight / (float)768);
            IpLabel = new Label(45, new Vector2f(IWindow.Settings.WindowWidth / 3, IWindow.Settings.WindowHeight / 2));
            IpLabel.Text.DisplayedString = "IP:";
            Status = new Label(40, new Vector2f(IWindow.Settings.WindowWidth / 3, 150));
            GameResult = new Label(40, new Vector2f(IWindow.Settings.WindowWidth / 2.2f, IWindow.Settings.WindowHeight/2.15f));
            TextBox = new TextBox(new Vector2f(IpLabel.Text.Position.X + 60, IpLabel.Text.Position.Y), new Vector2f(450, 50), 38);
            Connect = new Button("connect.png", new Vector2f(IWindow.Settings.WindowWidth - 525, IWindow.Settings.WindowHeight - 105));
            Cancel = new Button("back.png", new Vector2f(25, IWindow.Settings.WindowHeight - 105));
        }

        private void NextStep()
        {
            try
            {
                Connection = new Connection(TextBox.String);
                Connection.SendConnection();
                EndEnter = true;
            }
            catch (Exception ex)
            {
                Status.Text.DisplayedString = "Неверный формат IP адреса";
                EndEnter = false;
            }
        }

        public void RefreshView(Vector2f scale)
        {
            Background.Scale = scale;
            IpLabel.Text.Position = new Vector2f(IWindow.Settings.WindowWidth / 3, IWindow.Settings.WindowHeight / 2);
            Status.Text.Position = new Vector2f(200, 200);///!!!
            TextBox = new TextBox(new Vector2f(IpLabel.Text.Position.X + 60, IpLabel.Text.Position.Y), new Vector2f(450, 50), 38);
            Connect.Sprite.Position = new Vector2f(IWindow.Settings.WindowWidth - 525, IWindow.Settings.WindowHeight - 105);
            Cancel.Sprite.Position = new Vector2f(25, IWindow.Settings.WindowHeight - 105);
        }
        private void StartGame()
        {
            if (Game == null)
                GetGameSettings();
            Game.View();
            Connection.StartAsClient = false;
            ShowGameResults();
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
            {
                GameResult.Text.DisplayedString = "";
                Status.Text.DisplayedString = "Второй игрок отключился";
                EndEnter = false;
                Game = null;
                return;
            }
            Connection.SendConnection();
        }

        private void GetGameSettings()
        {
            int mode = Connection.ReceiveInt();
            switch (mode)
            {
                case 1:
                    Game = new Game(Window, new EasyGameSettings(), Connection);
                    break;
                case 2:
                    Game = new Game(Window, new MeniumGameSettings(), Connection); // средние
                    break;
                case 3:
                    Game = new Game(Window, new HardGameSettings(), Connection); // сложные
                    break;
            }
            
        }

        private void CheckStatus()
        {
            if (Connection.Connected)
                Status.Text.DisplayedString = "Ожидание запуска игры";
            else
            {
                GameResult.Text.DisplayedString = "";
                Status.Text.DisplayedString = "Отсутствует подключение к серверу";
                EndEnter = false;
                Game = null;
            }
            if (Connection.StartAsClient)
                StartGame();
        }

        public void View()
        {
            Status.Text.DisplayedString = "";
            GameResult.Text.DisplayedString = "";
            while (Window.IsOpen && !Exit)
            {
                Window.DispatchEvents();
                Window.Clear();
                Window.Draw(Background);
                if (!EndEnter)
                {
                    Window.Draw(IpLabel.Text);
                    Connect.Draw(Window);
                    TextBox.Draw(Window);
                }
                else
                {
                    Connection.Listen();
                    CheckStatus();
                    Window.Draw(GameResult.Text);
                }
                Window.Draw(Status.Text);
                Cancel.Draw(Window);
                ButtonActions();
                Window.Display();
            }
            Exit = false;
            EndEnter = false;
        }

        private void ButtonActions()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (Connect.isPicked && !ButtonisDown)
                {
                    Connect.isPicked = false;
                    NextStep();
                    ButtonisDown = true;
                }
                else if (Cancel.isPicked && !ButtonisDown)
                {
                    Game = null;
                    Exit = true;
                    ButtonisDown = true;
                }
            }else
                ButtonisDown = false;
        }

        private void Window_TextEntered(object sender, TextEventArgs e)
        {
            char key = e.Unicode.Cast<char>().First();
            if (key > 47 && key < 58 || key == 46)
                TextBox.Append(key);
            else if (key == 8)
                TextBox.Backspace();
            else if (key == 13)
                NextStep();
            else if (key == 27)
            {
                Game = null;
                Exit = true;
            }
        }

        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.LControl)
                PaseChecker = true;
            if(e.Code == Keyboard.Key.V)
            {
                if (PaseChecker)
                {
                    TextBox.Append(Clipboard.Contents.ToString());
                }
            }
        }

        private void Window_KeyReleased(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.LControl)
                PaseChecker = false;
        }
        private void WindowClose(object sender, EventArgs e)
        {
            Window.Close();
        }
    }
}
