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
    class SettingsMenu : IWindow
    {
        public RenderWindow Window { get; set; }
        Label Resolution { get; set; }
        Label ResolutionValue { get; set; }
        Label VSync { get; set; }
        Label Clip { get; set; }
        Label Sound { get; set; }
        Label Scale { get; set; }
        Label Header { get; set; }

        Button SoundSwitch { get; set; }
        Button VSyncSwitch { get; set; }
        Button ClipSwitch { get; set; }
        Button ResolutionChange { get; set; }
        Button Cancel { get; set; }
        Button Apply { get; set; }
        Sprite Background { get; set; } = new Sprite();
        bool Exit { get; set; }
        bool ButtonisDown { get; set; }
        
        public SettingsMenu(RenderWindow window)
        {
            Window = window;
            Background.Texture = new Texture("GameTextures/background.png");
            Background.Scale = new Vector2f((float)IWindow.Settings.WindowWidth / (float)1366, (float)IWindow.Settings.WindowHeight / (float)768);
            SetLabels();
            SetButtons();
            View();
        }

        private void SetLabels()
        {
            Header = new Label("19702.otf", 45, new Vector2f(IWindow.Settings.WindowWidth / 2 - 100, 50), 4, Color.White);
            Header.Text.DisplayedString = "Настройки";
            Resolution = new Label("19702.otf", 40, new Vector2f(IWindow.Settings.WindowWidth / 10, 150), 4, Color.White);
            Resolution.Text.DisplayedString = "Разрешение экрана";
            VSync = new Label("19702.otf", 40, new Vector2f(IWindow.Settings.WindowWidth / 10, 250), 4, Color.White);
            VSync.Text.DisplayedString = "Вертикальная синхронизация";
            Scale = new Label("19702.otf", 40, new Vector2f(IWindow.Settings.WindowWidth / 10, 350), 4, Color.White);
            Scale.Text.DisplayedString = "Масштабирование экрана";
            Sound = new Label("19702.otf", 40, new Vector2f(IWindow.Settings.WindowWidth / 10, 450), 4, Color.White);
            Sound.Text.DisplayedString = "Звук";
            Clip = new Label("19702.otf", 40, new Vector2f(IWindow.Settings.WindowWidth / 10, 550), 4, Color.White);
            Clip.Text.DisplayedString = "Вступительный ролик";
        }

        private void SetButtons()
        {
            Apply = new Button("apply.png", new Vector2f(IWindow.Settings.WindowWidth - 325, IWindow.Settings.WindowHeight - 105));
            Cancel = new Button("back.png", new Vector2f(25, IWindow.Settings.WindowHeight - 105));
            float X = (float)IWindow.Settings.WindowWidth / 1.5f;
            ResolutionValue = new Label("19702.otf", 40, new Vector2f(X, Resolution.Text.Position.Y), 4, Color.White);
            ResolutionValue.Text.DisplayedString = $"{IWindow.Settings.WindowWidth}x{IWindow.Settings.WindowHeight}";
            ResolutionChange= new Button("change.png", new Vector2f(X+175, Resolution.Text.Position.Y));
            if (IWindow.Settings.VSync)
                VSyncSwitch = new Button("switchon.png", new Vector2f(X, VSync.Text.Position.Y));
            else
                VSyncSwitch = new Button("switchoff.png", new Vector2f(X, VSync.Text.Position.Y));
            ///
            SoundSwitch= new Button("switchon.png", new Vector2f(X, Sound.Text.Position.Y));
            ClipSwitch = new Button("switchon.png", new Vector2f(X, Clip.Text.Position.Y));
        }

        public void View()
        {
            while (!Exit)
            {

                Window.DispatchEvents();
                Window.Clear();
                Window.Draw(Background);
                Window.Draw(Header.Text);
                Window.Draw(Resolution.Text);
                Window.Draw(VSync.Text);
                Window.Draw(Scale.Text);
                Window.Draw(Sound.Text);
                Window.Draw(Clip.Text);
                Window.Draw(ResolutionValue.Text);
                ResolutionChange.Draw(Window);
                VSyncSwitch.Draw(Window);
                SoundSwitch.Draw(Window);
                ClipSwitch.Draw(Window);
                Cancel.Draw(Window);
                Apply.Draw(Window);
                ResolutionChange.Draw(Window);
                ButtonActions();
                Window.Display();
            }
        }

        private void ButtonActions()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (ButtonisDown)
                    return;
                if (ResolutionChange.isPicked)
                {
                    if (ResolutionValue.Text.DisplayedString == "1366x768")
                        ResolutionValue.Text.DisplayedString = "1920x1080";
                    else
                        ResolutionValue.Text.DisplayedString = "1366x768";
                }
                else if (VSyncSwitch.isPicked)
                {
                    VSyncSwitch.Sprite.Texture= new Texture($"GameTextures/switchoff.png");
                }
                else if (SoundSwitch.isPicked)
                {
                    SoundSwitch.Sprite.Texture = new Texture($"GameTextures/switchoff.png");
                }
                else if (ClipSwitch.isPicked)
                {
                    ClipSwitch.Sprite.Texture = new Texture($"GameTextures/switchoff.png");
                }
                else if (Cancel.isPicked)
                {
                    Exit = true;
                }
                else if (Apply.isPicked)
                {
                    Exit = true;  // Применение выставленных настроек
                }
                ButtonisDown = true;
            }
            else
                ButtonisDown = false;
        }
    }
}
