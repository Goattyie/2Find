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
    class Button
    {
        Sprite Sprite { get; set; } = new Sprite();
        public bool isPicked{ get; set; }
        
        public Button(string texture, Vector2f pos)
        {
            Sprite.Texture = new Texture($"GameTextures/{texture}");
            Sprite.Position = pos;
        }

        public void Draw(RenderWindow window)
        {
            Sprite.Color = Color.White;
            isPicked = false;
            if (Sprite.GetGlobalBounds().Contains(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y)) 
            { 
                Sprite.Color= Color.Red;
                isPicked = true;
            }
            window.Draw(Sprite);
        }

    }
}
