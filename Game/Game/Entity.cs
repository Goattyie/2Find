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
        public int VisibleRange { get; set; } = 450;
        public int[] Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Sprite Sprite { get; set; } = new Sprite();
        public float[] Center { get { return new float[2] { (Sprite.Position.X + Width / 2), (Sprite.Position.Y + Height / 2) }; } }
        public int[] Position { get { return new int[2] { (int)(Sprite.Position.X + Width / 2) / WorldTextures.BlockSize[0], (int)(Sprite.Position.Y + Height / 2) / WorldTextures.BlockSize[1] }; } }
        public void Spawn() { Sprite.Position = new Vector2f(150, 150); }
        public void Spawn(int x, int y) { Sprite.Position = new Vector2f(x * WorldTextures.BlockSize[0], y * WorldTextures.BlockSize[1]); }
        public bool SeeOtherEntity(Entity entity1, Entity entity2, string[] VisibleChank) 
        {
            float[] dot = entity1.Center;
            float KatetX = -entity1.Center[0] + entity2.Center[0];
            float KatetY = -entity1.Center[1] + entity2.Center[1];
            double Gipotenuza = Math.Sqrt(Math.Pow(KatetX, 2) + Math.Pow(KatetY, 2));
            for (double c = 0; c < entity1.VisibleRange; c += 1)
            {
                double x = entity1.Center[0] + c * KatetX/(float)Gipotenuza;
                double y = entity1.Center[1] + c * KatetY / (float)Gipotenuza;
                if ((VisibleChank[(int)y / WorldTextures.BlockSize[1]][(int)x / WorldTextures.BlockSize[0]] > 47 &&
                    VisibleChank[(int)y / WorldTextures.BlockSize[1]][(int)x / WorldTextures.BlockSize[0]] < 70) || (
                    VisibleChank[(int)y / WorldTextures.BlockSize[1]][(int)x / WorldTextures.BlockSize[0]] > 96 &&
                    VisibleChank[(int)y / WorldTextures.BlockSize[1]][(int)x / WorldTextures.BlockSize[0]] < 102)) 
                    return false;
                else if (Math.Abs((int)x - (int)entity2.Center[0]) < 2 && Math.Abs((int)y - (int)entity2.Center[1]) < 2)
                    return true;
            }
            return false;
        }
    }
}
