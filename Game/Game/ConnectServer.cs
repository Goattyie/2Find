using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class ConnectServer : IWindow
    {
        public RenderWindow Window { get; set; }
        TextBox TextBox { get; set; }
        Connection Connection { get; set; }
        Label IpLabel { get; set; }
        Label Status { get; set; }
        Sprite Background { get; set; } = new Sprite();
        Button Connect { get; set; }
        Button Cancel { get; set; }

        bool Canceled { get; set; }
        bool EndEnter { get; set; }

        public ConnectServer(RenderWindow window)
        {
            Window = window;
            Background.Texture = new Texture("GameTextures/background.png");
            Window.TextEntered += Window_TextEntered;
            //Window.KeyPressed += Window_KeyPressed;
            Background.Scale = new Vector2f((float)IWindow.Settings.WindowWidth / (float)1366, (float)IWindow.Settings.WindowHeight / (float)768);
            IpLabel = new Label("19702.otf", 45, new Vector2f(IWindow.Settings.WindowWidth / 3, IWindow.Settings.WindowHeight / 2), 4, Color.White);
            IpLabel.Text.DisplayedString = "IP:";
            Status = new Label("19702.otf", 40, new Vector2f(200, 200), 4, Color.White);
            TextBox = new TextBox(new Vector2f(IpLabel.Text.Position.X + 60, IpLabel.Text.Position.Y), new Vector2f(450, 50), "romand__.ttf", 38);
            Connect = new Button("connect.png", new Vector2f(IWindow.Settings.WindowWidth - 525, IWindow.Settings.WindowHeight - 105));
            Cancel = new Button("cancel.png", new Vector2f(25, IWindow.Settings.WindowHeight - 105));
            View();
        }

        pprivate void NextStep()
        {
            EndEnter = true;
            Connection = new Connection(TextBox.String);
        }
        private void Window_TextEntered(object sender, TextEventArgs e)
        {
            char key = e.Unicode.Cast<char>().First();
            if (key > 47 && key < 58 || key == 46)
            {
                TextBox.Append(key);
            }
            else if (key == 8)
            {
                TextBox.Backspace();
            }
            else if (key == 13)
            {
                NextStep();
            }
            else if (key == 27)
            {
                Canceled = true;
            }
        }

        public void View()
        {
            while (!Canceled)
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
                    Window.Draw(Status.Text);
                }
                Cancel.Draw(Window);
                ButtonActions();
                Window.Display();
            }
        }

        private void ButtonActions()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (Connect.isPicked)
                {
                    Connect.isPicked = false;
                    NextStep();
                }
                else if (Cancel.isPicked)
                {
                    Canceled = true;
                }
            }
        }
    }
}
