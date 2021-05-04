using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Hitbox
    {
        Ray[] Lines { get; set; } = new Ray[4];
        public int[] Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float[] Center { get; set; }
        public Ray[] HitboxLine { get; set; } = new Ray[4];
        public Ray[] CollisionLine { get; set; } = new Ray[4];
        public Ray Direction { get; set; } = new Ray(); //Направление вгзляда

        Hitbox() { }
        public Hitbox(int width, int height)
        {
            Width = width;
            Height = height;
            Size = new int[] { Width, Height };
            HitboxLine = new Ray []{ new Ray(), new Ray(), new Ray(), new Ray() };
        }

        public void Update()
        {
            HitboxLine[0].X1 = Center[0]  - Width / 2;
            HitboxLine[0].Y1 = Center[1]  + Height / 2;
            HitboxLine[0].X2 = Center[0]  + Width / 2;
            HitboxLine[0].Y2 = Center[1]  + Height / 2;

            HitboxLine[1].X1 = Center[0]  + Width / 2;
            HitboxLine[1].Y1 = Center[1]  + Height / 2;
            HitboxLine[1].X2 = Center[0]  + Width / 2;
            HitboxLine[1].Y2 = Center[1]  - Height / 2;

            HitboxLine[2].X1 = Center[0]  + Width / 2;
            HitboxLine[2].Y1 = Center[1]  - Height / 2;
            HitboxLine[2].X2 = Center[0]  - Width / 2;
            HitboxLine[2].Y2 = Center[1]  - Height / 2;

            HitboxLine[3].X1 = Center[0]  - Width / 2;
            HitboxLine[3].Y1 = Center[1]  - Height / 2;
            HitboxLine[3].X2 = Center[0]  - Width / 2;
            HitboxLine[3].Y2 = Center[1]  + Height / 2;

        }
        public void CreateLine(int x, int y, int status)
        {
            if (status == 1)
                CollisionLine[0].SetLine(x * WorldTextures.BlockSize[0], y * WorldTextures.BlockSize[1], x * WorldTextures.BlockSize[0] + WorldTextures.BlockSize[0], y * WorldTextures.BlockSize[1]);
            if (status == 2)
                CollisionLine[1].SetLine(x * WorldTextures.BlockSize[0], y * WorldTextures.BlockSize[1], x * WorldTextures.BlockSize[0], y * WorldTextures.BlockSize[1] + WorldTextures.BlockSize[1]);
            if (status == 3)
                CollisionLine[2].SetLine(x * WorldTextures.BlockSize[0], y * WorldTextures.BlockSize[1] + WorldTextures.BlockSize[1], x * WorldTextures.BlockSize[0] + WorldTextures.BlockSize[0], y * WorldTextures.BlockSize[1] + WorldTextures.BlockSize[1]);
            if (status == 4)
                CollisionLine[3].SetLine(x * WorldTextures.BlockSize[0] + WorldTextures.BlockSize[0], y * WorldTextures.BlockSize[1], x * WorldTextures.BlockSize[0] + WorldTextures.BlockSize[0], y * WorldTextures.BlockSize[1] + +WorldTextures.BlockSize[1]);

        }
        public void CollisionClear()
        {
            for (int i = 0; i < CollisionLine.Length; i++)
                CollisionLine[i] = new Ray();
        }
        public void SetDirection(int status)
        {
            if (status == 1)
                Direction.SetLine(Center[0], Center[1], Center[0], Center[1] + Height/2);
            else if (status == 2)
                Direction.SetLine(Center[0], Center[1], Center[0] + Width/2, Center[1]);
            else if(status == 3)
                Direction.SetLine(Center[0], Center[1], Center[0], Center[1] - Height/2);
            else if(status == 4)
                Direction.SetLine(Center[0], Center[1], Center[0] - Width/2, Center[1]);

        }


    }
}
