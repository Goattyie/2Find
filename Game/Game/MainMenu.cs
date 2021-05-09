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
        Button CreateServerButton { get; set; }
        Button ConnectServerButton { get; set; }
        Button SettingsButton { get; set; }
        Button About { get; set; }
        Button Exit { get; set; }
        CreateServer Create { get; set; }
        ConnectServer Connect { get; set; }
        SettingsMenu Settings { get; set; }
        public RenderWindow Window { get; set; }
        Sprite Background { get; set; } = new Sprite();//пока что картинкой

        public MainMenu(Settings settings)
        {
            IWindow.Settings = settings;
            CreateServerButton = new Button("createserver.png", new Vector2f(50,100));
            ConnectServerButton = new Button("connect.png", new Vector2f(50, 225));
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
            CreateServerButton.Draw(Window);
            ConnectServerButton.Draw(Window);
            SettingsButton.Draw(Window);
            About.Draw(Window);
            Exit.Draw(Window);
        }

        private void ButtonActions()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (CreateServerButton.isPicked)
                {
                    if (Create == null)
                        Create = new CreateServer(Window);
                    Create.View();
                }
                else if (ConnectServerButton.isPicked)
                {
                    if (Connect == null)
                        Connect = new ConnectServer(Window);
                    Connect.View();
                }
                else if(SettingsButton.isPicked)
                {
                    if (Settings == null)
                        Settings = new SettingsMenu(Window);
                    Settings.View();
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
