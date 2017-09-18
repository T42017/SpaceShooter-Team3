using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CursorAiming
{
    public class Ray2D
    {
        private readonly List<Point> _result;
        public Vector2 EndPos;
        public Vector2 StartPos;

        public Ray2D()
        {
            StartPos = Vector2.Zero;
            EndPos = Vector2.Zero;
            _result = new List<Point>();
        }


        public Vector2 Intersects(Rectangle rectangle)
        {
            var p0 = new Point((int) StartPos.X, (int) StartPos.Y);
            var p1 = new Point((int) EndPos.X, (int) EndPos.Y);

            foreach (var testPoint in BresenhamLine(p0, p1))
                if (rectangle.Contains(testPoint))
                    return new Vector2(testPoint.X, testPoint.Y);
            return Vector2.Zero;
        }


        private void Swap<T>(ref T a, ref T b)
        {
            var c = a;
            a = b;
            b = c;
        }


        private List<Point> BresenhamLine(Point p0, Point p1)
        {
            return BresenhamLine(p0.X, p0.Y, p1.X, p1.Y);
        }


        private List<Point> BresenhamLine(int x0, int y0, int x1, int y1)
        {
            _result.Clear();

            var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);

            if (steep)
            {
                Swap(ref x0, ref y0);
                Swap(ref x1, ref y1);
            }
            var inverted = x0 > x1;
            if (inverted)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }

            var deltaX = x1 - x0;
            var deltaY = Math.Abs(y1 - y0);
            var error = 0;
            int ystep;
            var y = y0;
            if (y0 < y1) ystep = 1;
            else ystep = -1;
            if (inverted)
                for (var x = x1; x >= x0; x--)
                {
                    if (steep) _result.Add(new Point(y, x));
                    else _result.Add(new Point(x, y));
                    error += deltaY;
                    if (2 * error >= deltaX)
                    {
                        y += ystep;
                        error -= deltaX;
                    }
                }
            else
                for (var x = x0; x <= x1; x++)
                 {
                    if (steep) _result.Add(new Point(y, x));
                    else _result.Add(new Point(x, y));
                    error += deltaY;
                     if (2 * error >= deltaX)
                    {
                        y += ystep;
                        error -= deltaX;
                    }
                }


            return _result;
        }
    }
}