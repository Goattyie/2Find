using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    abstract class Entity
    {
        protected Texture Model;
        protected float Speed { get; set; } = 8.4f;
        public int[] Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Sprite Sprite { get; set; } = new Sprite();
        public float[] Center { get { return new float[2] { (Sprite.Position.X + Width / 2), (Sprite.Position.Y + Height / 2) }; } }
        public int[] Position { get { return new int[2] { (int)(Sprite.Position.X + Width / 2) / WorldTextures.BlockSize[0], (int)(Sprite.Position.Y + Height / 2) / WorldTextures.BlockSize[1] }; } }
        public void Spawn() { Sprite.Position = new Vector2f(150, 150); }
        public void Spawn(int x, int y) { Sprite.Position = new Vector2f(x * WorldTextures.BlockSize[0], y * WorldTextures.BlockSize[1]); }
    }
}
