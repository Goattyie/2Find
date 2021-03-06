using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Game
{
    class MainMenu:IWindow
    {
        Button CreateServerButton { get; set; }
        Button ConnectServerButton { get; set; }
        Button SettingsButton { get; set; }
        Button About { get; set; }
        Button Exit { get; set; }
        ServerLobby Create { get; set; }
        ClientLobby Connect { get; set; }
        SettingsMenu SettingsMenu { get; set; }
        public RenderWindow Window { get; set; }
        Sprite Background { get; set; } = new Sprite();
        Clock Clock { get; set; }
        float time { get; set; }
        float CurrentFrame { get; set; }
        public MainMenu()
        {
            IWindow.Settings = new Settings();
            Window = new RenderWindow(new SFML.Window.VideoMode((uint)IWindow.Settings.WindowWidth, (uint)IWindow.Settings.WindowHeight), Settings.WindowName);
            CreateServerButton = new Button("createserver.png", new Vector2f(50, 100));
            ConnectServerButton = new Button("connect.png", new Vector2f(50, 225));
            SettingsButton = new Button("settings.png", new Vector2f(50, 350));
            About = new Button("about.png", new Vector2f(50, 475));
            Exit = new Button("exit.png", new Vector2f(50, 600));
            Clock = new Clock();
            //Background.Texture = new Texture("GameTextures/background.png");
            Background.Scale = new Vector2f((float)IWindow.Settings.WindowWidth / (float)1920, (float)IWindow.Settings.WindowHeight / (float)1080);
            Window.SetVerticalSyncEnabled(IWindow.Settings.VSync);
            Window.Closed += WindowClose;
            IWindow.Settings.Music = new Music(new string[] { "Music/1.ogg", "Music/2.ogg", "Music/3.ogg" });
            View();
        }
        private void SetInterface()
        {
            Window.Position = new Vector2i(0, 0);
            Window.Size = new Vector2u((uint)IWindow.Settings.WindowWidth, (uint)IWindow.Settings.WindowHeight);
            Background.Scale = new Vector2f(1, 1);
            Vector2f NewScale = new Vector2f((float)IWindow.Settings.WindowWidth / (float)1366, (float)IWindow.Settings.WindowHeight / (float)768);
            Background.Scale = new Vector2f((float)IWindow.Settings.WindowWidth / (float)1920, (float)IWindow.Settings.WindowHeight / (float)1080);
            Window.SetView(new View(new Vector2f(IWindow.Settings.WindowWidth / 2.0f, IWindow.Settings.WindowHeight / 2.0f),
                new Vector2f(IWindow.Settings.WindowWidth, IWindow.Settings.WindowHeight)));
            SettingsMenu.RefreshView(NewScale);
            if (Create != null)
                Create.RefreshView(NewScale);
            if (Connect != null)
                Connect.RefreshView(NewScale);
        }
        public void View()
        {
            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                Window.Clear();
                IWindow.Settings.RenderMusic();
                DrawBackground();
                DrawButtons();
                ButtonActions();
                Window.Display();
            }
        }

        private void DrawBackground()
        {
            time = (float)Clock.ElapsedTime.AsMicroseconds() / 1000f;
            Clock.Restart();
            CurrentFrame += 0.009f * time;
            if (CurrentFrame > 27)
                CurrentFrame = 0;
            Background.Texture = new Texture(String.Format("Menubg/{0}.png", (int)CurrentFrame));
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
                        Create = new ServerLobby(Window);
                    Create.View();
                }
                else if (ConnectServerButton.isPicked)
                {
                    if (Connect == null)
                        Connect = new ClientLobby(Window);
                    Connect.View();
                }
                else if(SettingsButton.isPicked)
                {
                    if (SettingsMenu == null)
                        SettingsMenu = new SettingsMenu(Window);
                    SettingsMenu.View();
                    SetInterface();
                }
                else if (About.isPicked)
                {
                    string url = "https://github.com/Goattyie/2Find/blob/main/README.md";
                    try
                    {
                        Process.Start(url);
                    }
                    catch
                    {
                        url = url.Replace("&", "^&");
                        Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                    }
                }
                else if (Exit.isPicked)
                    Window.Close();
            }
        }

        private void WindowClose(object sender, EventArgs e)
        {
            Window.Close();
        }
    }
}
