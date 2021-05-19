﻿using SFML.Graphics;
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
        protected abstract float Speed { get; set; }
        protected Entity() { }
        protected Entity(string textureFile)
        {
            Model = new Texture($"GameTextures/{textureFile}");
            Sprite.Texture = Model;
            Size = new int[] { Width, Height };
            Width = (int)Model.Size.X;
            Height = (int)Model.Size.Y;
            Size = new int[] { Width, Height };
        }
        public int VisibleRange { get; set; } = 450;
        public int[] Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Sprite Sprite = new Sprite();
        public Vector2f Center { get { return new Vector2f((Sprite.Position.X + Width / 2), (Sprite.Position.Y + Height / 2)); } }
        public int[] Position { get { return new int[2] { (int)(Sprite.Position.X + Width / 2) / WorldTextures.BlockSize[0], (int)(Sprite.Position.Y + Height / 2) / WorldTextures.BlockSize[1]}; } }
        public void RandomSpawn(string[] GameField)
        {
            int x = 0;
            int y = 0;
            while (true)
            {
                y = new Random().Next(1, GameField.Length - 2);
                x = new Random().Next(1, GameField[0].Length - 2);
                if (!(GameField[y][x] > 47 && GameField[y][x] < 70) && !(GameField[y][x] > 47 && GameField[y][x] < 70))
                    break;
            }
            Spawn(x, y); 
        }
        public void Spawn(int x, int y) 
        { 
            Sprite.Position = new Vector2f(x * WorldTextures.BlockSize[0] + WorldTextures.BlockSize[0]/2 - Width/2, y * WorldTextures.BlockSize[1] + WorldTextures.BlockSize[1] / 2 - Height / 2); 
        }
        public bool SeeOtherEntity(Entity entity2, string[] VisibleChank) 
        {
            if (this.Position[0] == entity2.Position[0] && this.Position[1] == entity2.Position[1])
                return true;

            float KatetX = -this.Center.X + entity2.Center.X;
            float KatetY = -this.Center.Y + entity2.Center.Y;
            double Gipotenuza = Math.Sqrt(Math.Pow(KatetX, 2) + Math.Pow(KatetY, 2));
            for (double c = 0; c < this.VisibleRange; c += 1)
            {
                double x = this.Center.X + c * KatetX/(float)Gipotenuza;
                double y = this.Center.Y + c * KatetY / (float)Gipotenuza;
                
                if (!WorldTextures.IsWay(VisibleChank[(int)y / WorldTextures.BlockSize[1]][(int)x / WorldTextures.BlockSize[0]]))
                    return false;
                else if (Math.Abs((int)x - (int)entity2.Center.X) < 2 && Math.Abs((int)y - (int)entity2.Center.Y) < 2)
                    return true;
            }
            return false;
        }
    }
}
