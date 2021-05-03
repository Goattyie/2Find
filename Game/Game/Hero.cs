using System;
using SFML;
using SFML.Graphics;
using SFML.System;

namespace Game
{
    class Hero
    {
        Texture Model;
        public Sprite Sprite { get; set; } = new Sprite();
        float Speed { get; set; } = 11.4f;
        int[] Size { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        Hero() { }
        public Hero(string textureFile)
        {
            Model = new Texture($"GameTextures/{textureFile}");
            Width = (int)Model.Size.X;
            Height = (int)Model.Size.Y;
            Size = new int[]{ Width, Height};
            Sprite.Texture = Model;
        }

        public void Left() { Sprite.Position = Sprite.Position + new Vector2f(-Speed, 0); }
        public void Right() { Sprite.Position = Sprite.Position + new Vector2f(Speed, 0); }
        public void Forward() { Sprite.Position = Sprite.Position + new Vector2f(0, -Speed); }
        public void Back() { Sprite.Position = Sprite.Position + new Vector2f(0, +Speed); }

        void Render() { }
    }
}
