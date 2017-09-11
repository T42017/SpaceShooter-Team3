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

    public struct RectangleHitBox
    {
        public Rectangle Box;

        public RectangleHitBox(int size)
        {
            Box = new Rectangle(0, 0, size - 3, size - 3);
        }

        public void UpdatePosition(Vector2 pos)
        {
            Box.Location = new Point((int) pos.X - Box.Width / 2, (int) pos.Y - Box.Height / 2);
        }


        public bool CollidesWith(RectangleHitBox boxToCollideWith)
        {
            return Box.Contains(boxToCollideWith.Box);
        }

        public bool CollidesWith(Vector2 pointToCollideWith)
        {
            return Box.Contains(pointToCollideWith);
        }
    }
}