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
        Label ScaleValue { get; set; }

        SwitchButton SoundSwitch { get; set; }
        SwitchButton VSyncSwitch { get; set; }
        SwitchButton ClipSwitch { get; set; }
        Button ResolutionChange { get; set; }
        Button ScaleChange { get; set; }
        Button Cancel { get; set; }
        Button Apply { get; set; }
        Sprite Background { get; set; } = new Sprite();
        bool Exit { get; set; }
        bool ButtonisDown { get; set; }
        LinkedList<string> Scales { get; set; }

        public SettingsMenu(RenderWindow window)
        {
            Window = window;
            Window.Closed += WindowClose;
            Background.Texture = new Texture("GameTextures/background.png");
            Background.Scale = new Vector2f((float)IWindow.Settings.WindowWidth / (float)1366, (float)IWindow.Settings.WindowHeight / (float)768);
            Scales = new LinkedList<string>(new[] { "0.5", "0.75", "1", "1.25", "1.5" });
            SetLabels();
            SetButtons();
        }

        private void SetLabels()
        {
            Header = new Label(45, new Vector2f(IWindow.Settings.WindowWidth / 2 - 100, 50));
            Header.Text.DisplayedString = "Настройки";
            Resolution = new Label(40, new Vector2f(IWindow.Settings.WindowWidth / 10, 150));
            Resolution.Text.DisplayedString = "Разрешение экрана";
            VSync = new Label(40, new Vector2f(IWindow.Settings.WindowWidth / 10, 250));
            VSync.Text.DisplayedString = "Вертикальная синхронизация";
            Scale = new Label(40, new Vector2f(IWindow.Settings.WindowWidth / 10, 350));
            Scale.Text.DisplayedString = "Масштабирование экрана";
            Sound = new Label(40, new Vector2f(IWindow.Settings.WindowWidth / 10, 450));
            Sound.Text.DisplayedString = "Звук";
            Clip = new Label(40, new Vector2f(IWindow.Settings.WindowWidth / 10, 550));
            Clip.Text.DisplayedString = "Вступительный ролик";
            ResolutionValue = new Label(40, new Vector2f((float)IWindow.Settings.WindowWidth / 1.5f, Resolution.Text.Position.Y));
            ResolutionValue.Text.DisplayedString = $"{IWindow.Settings.WindowWidth}x{IWindow.Settings.WindowHeight}";//
            ScaleValue= new Label(40, new Vector2f((float)IWindow.Settings.WindowWidth / 1.5f+70, Scale.Text.Position.Y));
            ScaleValue.Text.DisplayedString = "1";
        }

        private void SetButtons()
        {
            Apply = new Button("apply.png", new Vector2f(IWindow.Settings.WindowWidth - 325, IWindow.Settings.WindowHeight - 105));
            Cancel = new Button("back.png", new Vector2f(25, IWindow.Settings.WindowHeight - 105));
            float X = (float)IWindow.Settings.WindowWidth / 1.5f;
            ResolutionChange = new Button("change.png", new Vector2f(X + 175, Resolution.Text.Position.Y));
            ScaleChange = new Button("change.png", new Vector2f(X + 175, Scale.Text.Position.Y));
            if (IWindow.Settings.VSync)
                VSyncSwitch = new SwitchButton("switchon.png", new Vector2f(X, VSync.Text.Position.Y));
            else
                VSyncSwitch = new SwitchButton("switchoff.png", new Vector2f(X, VSync.Text.Position.Y));
            ///
            SoundSwitch = new SwitchButton("switchon.png", new Vector2f(X, Sound.Text.Position.Y));//!
            ClipSwitch = new SwitchButton("switchon.png", new Vector2f(X, Clip.Text.Position.Y));//!
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
                Window.Draw(ScaleValue.Text);
                ResolutionChange.Draw(Window);
                ScaleChange.Draw(Window);
                VSyncSwitch.Draw(Window);
                SoundSwitch.Draw(Window);
                ClipSwitch.Draw(Window);
                Cancel.Draw(Window);
                Apply.Draw(Window);
                ResolutionChange.Draw(Window);
                ButtonActions();
                Window.Display();
            }
            Exit = false;
        }

        private void ButtonActions()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (ButtonisDown)
                    return;
                ButtonisDown = true;
                if (ResolutionChange.isPicked)
                {
                    if (ResolutionValue.Text.DisplayedString == "1366x768")
                        ResolutionValue.Text.DisplayedString = "1920x1080";
                    else
                        ResolutionValue.Text.DisplayedString = "1366x768";
                }
                else if (VSyncSwitch.isPicked)
                    VSyncSwitch.Switch();
                else if (SoundSwitch.isPicked)
                    SoundSwitch.Switch();
                else if (ClipSwitch.isPicked)
                    ClipSwitch.Switch();
                else if (ScaleChange.isPicked)
                {
                    if (Scales.Find(ScaleValue.Text.DisplayedString).Next != null)
                        ScaleValue.Text.DisplayedString = Scales.Find(ScaleValue.Text.DisplayedString).Next.Value;
                    else
                        ScaleValue.Text.DisplayedString = Scales.First.Value;
                }
                else if (Cancel.isPicked)
                    Exit = true;
                else if (Apply.isPicked)
                {
                    Exit = true;  // Применение выставленных настроек
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
