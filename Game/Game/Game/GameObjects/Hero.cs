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

        protected override float Speed { get; set; } = 3f;
        

        Hero():base() { }
        public Hero(string textureFile):base(textureFile)
        {
            CollisionBlock = new RectangleShape[4];
            for(int i = 0; i < CollisionBlock.Length; i++)
            {
                CollisionBlock[i] = new RectangleShape();
                CollisionBlock[i].Size = new Vector2f(WorldTextures.BlockSize[0], WorldTextures.BlockSize[1]);
            }

           
            Hitbox.Size = new Vector2f(Width, Height);
            Hitbox.Position = Sprite.Position;
        }
        public void Left() 
        {
            Hitbox.Position = Sprite.Position + new Vector2f(-Speed, 0);
            foreach (RectangleShape rs in CollisionBlock)
            {
                if (RectangleCross(Hitbox.Position.X, Hitbox.Position.Y, rs.Position.X, rs.Position.Y))
                {
                    Hitbox.Position = Sprite.Position;
                    return;
                }

            }
            Sprite.Position = Sprite.Position + new Vector2f(-Speed, 0);
            Hitbox.Position = Sprite.Position;
            
        }
        public void Right() 
        {
            Hitbox.Position = Sprite.Position + new Vector2f(Speed, 0);
            foreach(RectangleShape rs in CollisionBlock)
            {
                if (RectangleCross(Hitbox.Position.X, Hitbox.Position.Y, rs.Position.X, rs.Position.Y))
                {
                    Hitbox.Position = Sprite.Position;
                    return;
                }
                    
            }
            Sprite.Position = Sprite.Position + new Vector2f(Speed, 0);
            Hitbox.Position = Sprite.Position;
        }
        public void Forward() 
        {
            Hitbox.Position = Sprite.Position + new Vector2f(0, -Speed);
            foreach (RectangleShape rs in CollisionBlock)
            {
                if (RectangleCross(Hitbox.Position.X, Hitbox.Position.Y, rs.Position.X, rs.Position.Y))
                {
                    Hitbox.Position = Sprite.Position;
                    return;
                }

            }
            Sprite.Position = Sprite.Position + new Vector2f(0, -Speed);
                Hitbox.Position = Sprite.Position;
            
        }
        public void Back() 
        {
            Hitbox.Position = Sprite.Position + new Vector2f(0, +Speed);
            foreach (RectangleShape rs in CollisionBlock)
            {
                if (RectangleCross(Hitbox.Position.X, Hitbox.Position.Y, rs.Position.X, rs.Position.Y))
                {
                    Hitbox.Position = Sprite.Position;
                    return;
                }

            }
            Sprite.Position = Sprite.Position + new Vector2f(0, +Speed);
            Hitbox.Position = Sprite.Position;
            
        }

        void Render() { }
        
        public void Collision(char back, char right, char forward, char left) 
        {
            if (!WorldTextures.IsWay(back))
                CollisionBlock[0].Position = new Vector2f(Position[0] * WorldTextures.BlockSize[0], (Position[1] + 1) * WorldTextures.BlockSize[1]);

            if (!WorldTextures.IsWay(right))
                CollisionBlock[1].Position = new Vector2f((Position[0] + 1) * WorldTextures.BlockSize[0], Position[1] * WorldTextures.BlockSize[1]);

            if (!WorldTextures.IsWay(forward))
                CollisionBlock[2].Position = new Vector2f(Position[0] * WorldTextures.BlockSize[0], (Position[1] - 1) * WorldTextures.BlockSize[1]);

            if (!WorldTextures.IsWay(left))
                CollisionBlock[3].Position = new Vector2f((Position[0] - 1) * WorldTextures.BlockSize[0], Position[1] * WorldTextures.BlockSize[1]);
        }   


        protected bool RectangleCross(float x1, float y1, float x2, float y2) //Пересечение хитбокса и коллизионного квадрата
        {
            /*
            x1, y1 - левая нижняя точка первого прямоугольника
            x2, y2 - правая верхняя точка первого прямоугольника
            x3, y3 - левая нижняя точка второго прямоугольника
            x4, y4 - правая верхняя точка второго прямоугольника
            */

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
