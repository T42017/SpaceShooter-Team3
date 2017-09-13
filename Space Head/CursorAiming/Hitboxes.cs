using Microsoft.Xna.Framework;

namespace CursorAiming
{
    public struct CircleHitBox
    {
        public Vector2 MiddlePoint;
        public int Radius;

        public CircleHitBox(Vector2 middlePoint, int radius)
        {
            MiddlePoint = middlePoint;
            Radius = radius;
        }

        public bool Contains(Vector2 point)
        {
            return (point - MiddlePoint).Length() <= Radius;
        }
    }

    public class RectangleHitBox
    {
        private readonly int _amountOfRays;
        public Rectangle Box;
        public Ray2D Ray = new Ray2D();

        public RectangleHitBox(int amountOfRays)
        {
            _amountOfRays = amountOfRays;
        }


        public void UpdatePosition(Vector2 pos)
        {
            Box.Location = new Point((int) pos.X - Box.Width / 2, (int) pos.Y - Box.Height / 2);
        }


        public float CheckForRayCollision(Vector2 direction)
        {
            bool collision;

            float spaceBetweenRays;
            for (var i = 0; i < _amountOfRays; i++)
            {
                if (direction == Vector2.UnitX)
                {
                    spaceBetweenRays = (Box.Height - 2) / (_amountOfRays - 1);

                    Ray.StartPos.X = Box.Right;
                    Ray.StartPos.Y = Box.Bottom - spaceBetweenRays * i - 1;
                    Ray.EndPos.X = Globals.ScreenWidth;
                    Ray.EndPos.Y = Box.Bottom - spaceBetweenRays * i - 1;
                }
                else if (direction == -Vector2.UnitX)
                {
                    spaceBetweenRays = (Box.Height - 2) / (_amountOfRays - 1);

                    Ray.StartPos.X = Box.Left;
                    Ray.StartPos.Y = Box.Bottom - spaceBetweenRays * i - 1;
                    Ray.EndPos.X = 0;
                    Ray.EndPos.Y = Box.Bottom - spaceBetweenRays * i - 1;
                }
                else if (direction == Vector2.UnitY)
                {
                    spaceBetweenRays = (Box.Width - 2) / (_amountOfRays - 1);

                    Ray.StartPos.X = Box.Right - spaceBetweenRays * i - 1;
                    Ray.StartPos.Y = Box.Bottom;
                    Ray.EndPos.X = Box.Right - spaceBetweenRays * i - 1;
                    Ray.EndPos.Y = Globals.ScreenHeight;
                }
                else if (direction == -Vector2.UnitY)
                {
                    spaceBetweenRays = (Box.Width - 2) / (_amountOfRays - 1);

                    Ray.StartPos.X = Box.Right - spaceBetweenRays * i - 1;
                    Ray.StartPos.Y = Box.Top;
                    Ray.EndPos.X = Box.Right - spaceBetweenRays * i - 1;
                    Ray.EndPos.Y = 0;
                }

                var edgePosition = new Vector2(Ray.StartPos.X, Ray.StartPos.Y);

                foreach (var rectangle in SpaceHeadGame.ObstaclesOnField)
                {
                    var hit = Ray.Intersects(rectangle);
                    collision = hit != Vector2.Zero;

                    if (collision) return Vector2.Distance(edgePosition, hit);
                }
            }
            return float.PositiveInfinity;
        }

        public bool CollidesWith(RectangleHitBox boxToCollideWith)
        {
            return Box.Intersects(boxToCollideWith.Box);
        }

        public bool CollidesWith(Vector2 pointToCollideWith)
        {
            return Box.Contains(pointToCollideWith);
        }
    }
}