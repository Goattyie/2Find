using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Game
{
    class MainMenu:IWindow
    {
        Button CreateServer { get; set; }
        Button ConnectServer { get; set; }
        Button SettingsButton { get; set; }
        Button About { get; set; }
        Button Exit { get; set; }
        public RenderWindow Window { get; set; }
        Sprite Background { get; set; } = new Sprite();//пока что картинкой

        public MainMenu(Settings settings)
        {
            IWindow.Settings = settings;
            CreateServer = new Button("createserver.png", new Vector2f(50,100));
            ConnectServer = new Button("connect.png", new Vector2f(50, 225));
            SettingsButton = new Button("settings.png", new Vector2f(50, 350));
            About = new Button("about.png", new Vector2f(50, 475));
            Exit = new Button("exit.png", new Vector2f(50, 600));
            Background.Texture = new Texture("GameTextures/background.png");
            Background.Scale = new Vector2f((float)IWindow.Settings.WindowWidth / (float)1366,(float)IWindow.Settings.WindowHeight / (float)768);///hgvhjg
            Window = new RenderWindow(new SFML.Window.VideoMode((uint)IWindow.Settings.WindowWidth, (uint)IWindow.Settings.WindowHeight), IWindow.Settings.WindowName);
            Window.SetVerticalSyncEnabled(IWindow.Settings.VSync);
            Window.Closed += WindowClose;
            //Window.KeyPressed += KeyPressed;
            View();
        }

        public void View()
        {
            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                Window.Clear();
                DrawBackground();
                DrawButtons();
                ButtonActions();
                Window.Display();
            }
        }

        private void DrawBackground()
        {
            Window.Draw(Background);
        }
        private void DrawButtons()
        {
            CreateServer.Draw(Window);
            ConnectServer.Draw(Window);
            SettingsButton.Draw(Window);
            About.Draw(Window);
            Exit.Draw(Window);
        }

        private void ButtonActions()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (CreateServer.isPicked)
                {
                    new CreateServer(Window);
                }
                else if (ConnectServer.isPicked)
                {
                    new ConnectServer(Window);
                }
                else if(SettingsButton.isPicked)
                {
                    new SettingsMenu(Window);
                }
                else if (About.isPicked)
                {
                    Console.WriteLine("About");
                }
                else if (Exit.isPicked)
                {
                    Window.Close();
                }
            }
        }

        private void WindowClose(object sender, EventArgs e)
        {
            Window.Close();
        }
    }
}
