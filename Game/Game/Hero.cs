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
        public float[] Position { get { return new float[2] { (Sprite.Position.X + Hitbox.Width / 2), (Sprite.Position.Y + Hitbox.Height /2)  }; }}
 
        float Speed { get; set; } = 1.4f;
        public Hitbox Hitbox { get; set; }
        
        Hero() { }
        public Hero(string textureFile)
        {
            Model = new Texture($"GameTextures/{textureFile}");
            Hitbox = new Hitbox((int)Model.Size.X, (int)Model.Size.Y);
            Sprite.Texture = Model;
            Spawn();
        }

        public void Left() 
        {
            Hitbox.SetDirection(4);
            if(!Ray.Crossing(Hitbox.Direction, Hitbox.CollisionLine[3]))
                Sprite.Position = Sprite.Position + new Vector2f(-Speed, 0);
        }
        public void Right() 
        {
            Hitbox.SetDirection(2);
            if (!Ray.Crossing(Hitbox.Direction, Hitbox.CollisionLine[1]))
                Sprite.Position = Sprite.Position + new Vector2f(Speed, 0);
        }
        public void Forward() 
        {
            Hitbox.SetDirection(3);
            if (!Ray.Crossing(Hitbox.Direction, Hitbox.CollisionLine[2]))
                Sprite.Position = Sprite.Position + new Vector2f(0, -Speed); 
        }
        public void Back() 
        {
            Hitbox.SetDirection(1);
            if (!Ray.Crossing(Hitbox.Direction, Hitbox.CollisionLine[0]))
                Sprite.Position = Sprite.Position + new Vector2f(0, +Speed); 
        }

        void Spawn()
        {
            Sprite.Position = new Vector2f(150, 150);
        }
        void Render() { }
        
        public void Collision(char back, char right, char forward, char left) 
        {
            Hitbox.Center = Position;
            Hitbox.Update();
            Hitbox.CollisionClear();
            if ((back > 47 && back < 70) || (back > 96 && back < 102))
                Hitbox.CreateLine((int)(Position[0] / WorldTextures.BlockSize[0]), (int)(Position[1] / WorldTextures.BlockSize[1]) + 1, 1);

            if ((right > 47 && right < 70) || (right > 96 && right < 102))
               Hitbox.CreateLine((int)(Position[0] / WorldTextures.BlockSize[0]) + 1, (int)(Position[1] / WorldTextures.BlockSize[1]), 2);

            if ((forward > 47 && forward < 70) || (forward > 96 && forward < 102))
                Hitbox.CreateLine((int)(Position[0]/WorldTextures.BlockSize[0]), (int)(Position[1] / WorldTextures.BlockSize[1]) - 1, 3);

            if ((left > 47 && left < 70) || (left > 96 && left < 102))
                Hitbox.CreateLine((int)(Position[0] / WorldTextures.BlockSize[0]) - 1, (int)(Position[1] / WorldTextures.BlockSize[1]), 4);

            
            if (Hitbox.CollisionLine[0].X1 != 0)
                Console.WriteLine("back");
            if (Hitbox.CollisionLine[1].X1 != 0)
                Console.WriteLine("right");
            if (Hitbox.CollisionLine[2].X1 != 0)
                Console.WriteLine("forward");
            if (Hitbox.CollisionLine[3].X1 != 0)
                Console.WriteLine("left");
            
        }
 
        
    }
}
