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
    class Label
    {
        Font Font { get; set; }
        public Text Text { get; set; } = new Text();
        public Label(string file,uint size,Vector2f pos,int spacing,Color color)
        {
            Font = new Font($"Fonts/{file}");
            Text.Font = this.Font;
            Text.CharacterSize = size;
            Text.Position = pos;
            Text.LetterSpacing = spacing;
            Text.FillColor = color;
        }
    }
}
