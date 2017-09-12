﻿using System;
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
        public Rectangle Box;
        public Ray2D Ray = new Ray2D();
        private int _amountOfRays;
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

            for (int i = 1; i < _amountOfRays+1; i++)
            {
                if (direction == Vector2.UnitX)
                {
                    Ray.StartPos.X = Box.Right;
                    Ray.StartPos.Y = Box.Bottom + Box.Height / i;
                    Ray.EndPos.X = Globals.ScreenWidth;
                    Ray.EndPos.Y = Box.Bottom + Box.Height / i;
                }
                else if (direction == -Vector2.UnitX)
                {
                    Ray.StartPos.X = Box.Left;
                    Ray.StartPos.Y = Box.Bottom + Box.Height / i;
                    Ray.EndPos.X = 0;
                    Ray.EndPos.Y = Box.Bottom + Box.Height / i;
                }
                else if (direction == Vector2.UnitY)
                {
                    Ray.StartPos.X = Box.Left + Box.Width / i;
                    Ray.StartPos.Y = Box.Bottom;
                    Ray.EndPos.X = Box.Left + Box.Width / i;
                    Ray.EndPos.Y = Globals.ScreenHeight;
                }
                else if (direction == -Vector2.UnitY)
                {
                    Ray.StartPos.X = Box.Left + Box.Width / i;
                    Ray.StartPos.Y = Box.Top;
                    Ray.EndPos.X = Box.Left + Box.Width / i;
                    Ray.EndPos.Y = 0;
                }

                var edgePosition = new Vector2(Ray.StartPos.X, Ray.StartPos.Y);

                foreach (var rectangle in SpaceHeadGame.ObstaclesOnField)
                {
                    Vector2 hit = Ray.Intersects(rectangle);
                    collision = hit != Vector2.Zero;

                    if(collision) return Vector2.Distance(edgePosition, hit);
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