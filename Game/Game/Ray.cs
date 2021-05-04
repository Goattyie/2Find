using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Ray
    {
        public float X1 { get; set; }
        public float X2 { get; set; }
        public float Y1 { get; set; }
        public float Y2 { get; set; }
        public Ray() { }
        public Ray(float x1, float y1, float x2, float y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }
        public void SetLine(float x1, float y1, float x2, float y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public static bool Crossing(Ray ray1, Ray ray2)//Пересечение двух лучей
        {
            float[] Ray1 = ToArray(ray1);
            float[] Ray2 = ToArray(new Ray(ray1.X1, ray1.Y1, ray2.X1, ray2.Y1));
            float[] Ray3 = ToArray(new Ray(ray1.X1, ray1.Y1, ray2.X2, ray2.Y2));

            float Z1 = VectorMultiply(Ray1, Ray2);
            float Z2 = VectorMultiply(Ray1, Ray3);
            if (Z1 < 0 && Z2 < 0 || Z1 > 0 && Z2 > 0)
                return false;

            float[] Ray4 = ToArray(ray2);
            float[] Ray5 = ToArray(new Ray(ray2.X2, ray2.Y2, ray1.X1, ray1.Y1));
            float[] Ray6 = ToArray(new Ray(ray2.X2, ray2.Y2, ray1.X2, ray1.Y2));

            float Z3 = VectorMultiply(Ray4, Ray5);
            float Z4 = VectorMultiply(Ray4, Ray6);

            if (Z3 < 0 && Z4 < 0 || Z3 > 0 && Z4 > 0)
                return false;

            return true;
        }

        private static float[] ToArray(Ray ray) { return new float[] { ray.X2 - ray.X1, ray.Y2 - ray.Y1 }; }
        private static float VectorMultiply(float[] ray1, float[] ray2) { return ray1[0] * ray2[1] - ray1[1] * ray2[0]; }
    }
}
