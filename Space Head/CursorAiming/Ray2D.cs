using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace CursorAiming
{
    public class Ray2D
    {
        public Vector2 StartPos;
        public Vector2 EndPos;
        private readonly List<Point> result;

        public Ray2D()
        {
            this.StartPos = Vector2.Zero;
            this.EndPos = Vector2.Zero;
            result = new List<Point>();
        }

         
        public Vector2 Intersects(Rectangle rectangle)
        {
            Point p0 = new Point((int)StartPos.X, (int)StartPos.Y);
            Point p1 = new Point((int)EndPos.X, (int)EndPos.Y);

            foreach (Point testPoint in BresenhamLine(p0, p1))
            {
                if (rectangle.Contains(testPoint))
                    return new Vector2((float)testPoint.X, (float)testPoint.Y);
            }
            return Vector2.Zero;
        }


        private void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }


        private List<Point> BresenhamLine(Point p0, Point p1)
        {
            return BresenhamLine(p0.X, p0.Y, p1.X, p1.Y);
        }


        private List<Point> BresenhamLine(int x0, int y0, int x1, int y1)
        {
              

            result.Clear();

            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                Swap(ref x0, ref y0);
                Swap(ref x1, ref y1);
            }
            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }

            int deltax = x1 - x0;
            int deltay = Math.Abs(y1 - y0);
            int error = 0;
            int ystep;
            int y = y0;
            if (y0 < y1) ystep = 1; else ystep = -1;
            for (int x = x0; x <= x1; x++)
            {
                if (steep) result.Add(new Point(y, x));
                else result.Add(new Point(x, y));
                error += deltay;
                if (2 * error >= deltax)
                {
                    y += ystep;
                    error -= deltax;
                }
            }

            return result;
        }
    }
}
