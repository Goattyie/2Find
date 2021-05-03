using SFML.Graphics;
using System;


namespace Game
{
    interface IWindow
    {
        Settings Settings { get; set; }
        RenderWindow Window { get; set; }
       
        public void View(); //Открыть окно
    }
}
