using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class SwitchButton : Button
    {
        public bool State { get; set; }
        public SwitchButton(string texture, Vector2f pos) : base(texture, pos)
        {
            if (texture == "switchon.png")
                State = true;
            else if(texture == "switchon.png")
                State = false;
        }

        public void Switch()
        {
            if (State == true)
            {
                State = false;
                this.Sprite.Texture = new Texture($"GameTextures/switchoff.png");
            }
            else
            {
                State = true;
                this.Sprite.Texture = new Texture($"GameTextures/switchon.png");
            }

        }
    }
}
