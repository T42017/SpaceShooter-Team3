using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    class Bullet
    {
        public int Damage, Speed;
        public Texture2D Texture;
        public Vector2 Direction, Position;

        public Bullet(int _speed, int _damage, Texture2D _texture, Vector2 _startPosition, Vector2 _direction)
        {
            Speed = _speed;
            Texture = _texture;
            Position = _startPosition;
            Direction = _direction;
        }

    }
}
