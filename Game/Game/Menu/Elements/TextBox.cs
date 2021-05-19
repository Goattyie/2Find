﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Game
{
    class TextBox
    {
        public string String { get; set; }
        Label InputText { get; set; }
        private RectangleShape Form { get; set; } = new RectangleShape();

        public TextBox(Vector2f pos, Vector2f size, uint charsize)
        {
            Form.Position = pos;
            Form.Size = size;
            Form.FillColor = Color.White;
            InputText = new Label("romand__.ttf", charsize, pos, 1, Color.Black);
            String = "";
        }

        public void Draw(RenderWindow window)
        {
            InputText.Text.DisplayedString = this.String;
            window.Draw(Form);
            window.Draw(InputText.Text);
        }

        public void Append(char c)
        {
            if (String.Length < 16)
                String = String.Insert(String.Length, c.ToString());
        }

        public void Append(string s)
        {
            if (s.Length < 16)
            {
                foreach (char c in s)
                    if (!Char.IsNumber(c) && c != '.')
                        return;
                String = s;
            }
        }

        public void Backspace()
        {
            if (String.Length > 0)
                String = String.Remove(String.Length - 1);
        }

    }
}
