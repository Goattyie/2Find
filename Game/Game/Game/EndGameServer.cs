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
    class EndGameServer:IWindow
    {
        public RenderWindow Window { get; set; }
        Button Restart { get; set; }
        Button Exit { get; set; }
        public int Result { get; set; } = 0;//Результат выбора игрока-сервера
        Label GameState { get; set; }
        Sprite Background { get; set; } = new Sprite();
        public EndGameServer(RenderWindow window, int gameState)
        {
            Window = window;
            Background.Texture = new Texture("GameTextures/background.png");
            Background.Scale = new Vector2f((float)IWindow.Settings.WindowWidth / (float)1366, (float)IWindow.Settings.WindowHeight / (float)768);
            GameState = new Label(40, new Vector2f(IWindow.Settings.WindowWidth / 2, IWindow.Settings.WindowHeight + 100));
            if (gameState == 1)
                GameState.Text.DisplayedString = "Победа";
            else GameState.Text.DisplayedString = "Поражение";
            Restart = new Button("start.png", new Vector2f(IWindow.Settings.WindowWidth / 1.4f, IWindow.Settings.WindowHeight * 0.75f));
            Exit = new Button("exit.png", new Vector2f(IWindow.Settings.WindowWidth / 7f, IWindow.Settings.WindowHeight * 0.75f));
            Window.Closed += WindowClose;
        }

        private void WindowClose(object sender, EventArgs e)
        {
            Window.Close();
        }

        public void View()
        {
            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                Window.Clear();
                ButtonClick();
                DrawAll();
                Window.Display();
                if (Result != 0)
                    break;
            }
        }
        private void ButtonClick()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (Restart.isPicked)
                {
                    Result = 1;
                }
                else if (Exit.isPicked)
                {
                    Result = 2;
                }
            }
        }
        private void DrawAll()
        {
            Window.Draw(Background);
            Restart.Draw(Window);
            Exit.Draw(Window);
            Window.Draw(GameState.Text);
        }
    }
}
