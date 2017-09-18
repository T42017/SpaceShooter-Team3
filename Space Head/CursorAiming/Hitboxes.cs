using System;
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


        public Vector2 CheckMoveDistance (int movespeed, Vector2 direction, float deltaTime)
        {
            float distanceToCollision;
            float x = 0, y = 0;

            if (direction.X > 0)
            {
                distanceToCollision = Math.Abs(CheckForRayCollision(Vector2.UnitX));
                if (distanceToCollision < Math.Abs(direction.X *
                                                   movespeed * deltaTime))
                {
                    x = distanceToCollision;
                }
                else
                    x = direction.X *movespeed * deltaTime;
            }
            else if (direction.X < 0)
            {
                distanceToCollision = Math.Abs(CheckForRayCollision(-Vector2.UnitX));
                if (distanceToCollision < Math.Abs(direction.X *
                                                   movespeed * deltaTime))
                    x = -distanceToCollision;
                else
                    x = direction.X * movespeed * deltaTime;
            }

            if (direction.Y > 0)
            {
                distanceToCollision = Math.Abs(CheckForRayCollision(Vector2.UnitY));
                if (distanceToCollision < Math.Abs(direction.Y *
                                                   movespeed * deltaTime))
                    y = distanceToCollision;
                else
                    y = direction.Y * movespeed * deltaTime;
            }
            else if (direction.Y < 0)
            {
                distanceToCollision = Math.Abs(CheckForRayCollision(-Vector2.UnitY));
                if (distanceToCollision < Math.Abs(direction.Y *
                                                   movespeed * deltaTime))
                    y = -distanceToCollision;
                else
                    y= direction.Y * movespeed * deltaTime;
            }

            return new Vector2(x,y);
        }

        public float CheckForRayCollision(Vector2 direction)
        {
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
                    var collision = hit != Vector2.Zero;

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