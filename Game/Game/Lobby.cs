using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2._0
{
    class Lobby:IWindow
    {
        public Settings Settings { get; set; }
        public RenderWindow Window { get; set; }
        private Lobby() { }
        public Lobby(Settings settings)
        {
            this.Settings = settings;
            View();
        }
        public void View()
        {
            Window = new RenderWindow(new SFML.Window.VideoMode((uint)Settings.WindowWidth, (uint)Settings.WindowHeight), Settings.WindowName);
            Window.SetVerticalSyncEnabled(Settings.VSync);
            Window.Closed += WindowClose;

            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                Window.Clear(Color.Black);
                Window.Display();
            }
        }

        public void WindowClose(object sender, EventArgs e)
        {
            Window.Close();
        }
    }
}
