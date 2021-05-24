using System;
using SFML;
using SFML.Graphics;
using SFML.System;

namespace Game
{
    class Hero:Entity
    {
        

        public RectangleShape Hitbox { get; set; } = new RectangleShape();
        public RectangleShape[] CollisionBlock { get; set; }

        protected override float Speed { get; set; } = 1.4f;
        

        Hero():base() { }
        public Hero(string textureFile):base(textureFile)
        {
            CollisionBlock = new RectangleShape[8];
            for(int i = 0; i < CollisionBlock.Length; i++)
            {
                CollisionBlock[i] = new RectangleShape();
                CollisionBlock[i].Size = new Vector2f(WorldTextures.BlockSize[0], WorldTextures.BlockSize[1]);
            }
            Hitbox.Size = new Vector2f(Width, Height);
            Hitbox.Position = Sprite.Position;
            Width = 70;
            Height = 70;
            Size = new int[] { Width, Height };
            Sprite.Scale = new Vector2f(0.4375f, 0.4375f);
        }
        public void Left(float time) 
        {
            Hitbox.Position = Sprite.Position + new Vector2f(-Speed*time, 0);
            foreach (RectangleShape rs in CollisionBlock)
            {
                if (RectangleCross(Hitbox.Position.X, Hitbox.Position.Y, rs.Position.X, rs.Position.Y))
                {
                    Hitbox.Position = Sprite.Position;
                    return;
                }

            }
            Sprite.Position = Sprite.Position + new Vector2f(-Speed * time, 0);
            Hitbox.Position = Sprite.Position;
            
        }
        public void Right(float time) 
        {
            Hitbox.Position = Sprite.Position + new Vector2f(Speed * time, 0);
            foreach(RectangleShape rs in CollisionBlock)
            {
                if (RectangleCross(Hitbox.Position.X, Hitbox.Position.Y, rs.Position.X, rs.Position.Y))
                {
                    Hitbox.Position = Sprite.Position;
                    return;
                }
                    
            }
            Sprite.Position = Sprite.Position + new Vector2f(Speed * time, 0);
            Hitbox.Position = Sprite.Position;
        }
        public void Forward(float time) 
        {
            Hitbox.Position = Sprite.Position + new Vector2f(0, -Speed * time);
            foreach (RectangleShape rs in CollisionBlock)
            {
                if (RectangleCross(Hitbox.Position.X, Hitbox.Position.Y, rs.Position.X, rs.Position.Y))
                {
                    Hitbox.Position = Sprite.Position;
                    return;
                }

            }
            Sprite.Position = Sprite.Position + new Vector2f(0, -Speed * time);
                Hitbox.Position = Sprite.Position;
            
        }
        public void Back(float time) 
        {
            Hitbox.Position = Sprite.Position + new Vector2f(0, +Speed * time);
            foreach (RectangleShape rs in CollisionBlock)
            {
                if (RectangleCross(Hitbox.Position.X, Hitbox.Position.Y, rs.Position.X, rs.Position.Y))
                {
                    Hitbox.Position = Sprite.Position;
                    return;
                }

            }
            Sprite.Position = Sprite.Position + new Vector2f(0, +Speed * time);
            Hitbox.Position = Sprite.Position;
            
        }   
        public void Collision(char back, char right, char forward, char left, char back_left, char fw_left, char fw_right, char back_right) 
        {
            if (!WorldTextures.IsWay(back))
                CollisionBlock[0].Position = new Vector2f(Position[0] * WorldTextures.BlockSize[0], (Position[1] + 1) * WorldTextures.BlockSize[1]);

            if (!WorldTextures.IsWay(right))
                CollisionBlock[1].Position = new Vector2f((Position[0] + 1) * WorldTextures.BlockSize[0], Position[1] * WorldTextures.BlockSize[1]);

            if (!WorldTextures.IsWay(forward))
                CollisionBlock[2].Position = new Vector2f(Position[0] * WorldTextures.BlockSize[0], (Position[1] - 1) * WorldTextures.BlockSize[1]);

            if (!WorldTextures.IsWay(left))
                CollisionBlock[3].Position = new Vector2f((Position[0] - 1) * WorldTextures.BlockSize[0], Position[1] * WorldTextures.BlockSize[1]);

            if (!WorldTextures.IsWay(back_left))
                CollisionBlock[4].Position = new Vector2f((Position[0] - 1) * WorldTextures.BlockSize[0], (Position[1] + 1) * WorldTextures.BlockSize[1]);

            if (!WorldTextures.IsWay(fw_left))
                CollisionBlock[5].Position = new Vector2f((Position[0] - 1) * WorldTextures.BlockSize[0], (Position[1] - 1) * WorldTextures.BlockSize[1]);

            if (!WorldTextures.IsWay(fw_right))
                CollisionBlock[6].Position = new Vector2f((Position[0] + 1) * WorldTextures.BlockSize[0], (Position[1] - 1) * WorldTextures.BlockSize[1]);

            if (!WorldTextures.IsWay(back_right))
                CollisionBlock[7].Position = new Vector2f((Position[0] + 1) * WorldTextures.BlockSize[0], (Position[1] + 1) * WorldTextures.BlockSize[1]);
        }   
        protected bool RectangleCross(float x1, float y1, float x2, float y2) //Пересечение хитбокса и коллизионного квадрата
        {
            float left = Math.Max(x1, x2);
            float top = Math.Min(y1 + Height, y2 + WorldTextures.BlockSize[1]);
            float right = Math.Min(x1 + Width, x2 + WorldTextures.BlockSize[0]);
            float bottom = Math.Max(y1, y2);

            float width = right - left;
            float height =  top - bottom;

            if (width < 0 || height < 0)
                return false;

            return true;
        }

    }
}
