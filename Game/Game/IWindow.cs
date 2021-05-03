using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2._0
{
    interface IWindow
    {
        Settings Settings { get; set; }
        RenderWindow Window { get; set; }
        public void View(); //Открыть окно
    }
}
