using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


}
