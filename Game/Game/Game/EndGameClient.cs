using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class EndGameClient:IWindow
    {
        public RenderWindow Window { get; set; }
        Label GameState { get; set; }
        Sprite Background { get; set; } = new Sprite();

        public EndGameClient(RenderWindow window, int gameState)
        {
            Window = window;
            Background.Texture = new Texture("GameTextures/background.png");
            Background.Scale = new Vector2f((float)IWindow.Settings.WindowWidth / (float)1366, (float)IWindow.Settings.WindowHeight / (float)768);
            GameState = new Label(40, new Vector2f(IWindow.Settings.WindowWidth / 2, IWindow.Settings.WindowHeight + 100));
            if (gameState == 1)
                GameState.Text.DisplayedString = "Победа";
            else GameState.Text.DisplayedString = "Поражение";
            Window.Closed += WindowClose;
        }

        private void WindowClose(object sender, EventArgs e)
        {
            Window.Close();
        }

        public void View()
        {
            Window.DispatchEvents();
            Window.Clear();
            Window.Draw(GameState.Text);
            Window.Display();
        }
    }
}
