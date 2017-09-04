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
        public float Rotation;
        public Texture2D Texture;
        public Vector2 Direction, Position;

        public Bullet(int _speed, int _damage, Texture2D _texture, Vector2 _startPosition, Vector2 _direction, float _rotation)
        {
            Speed = _speed;
            Texture = _texture;
            Position = _startPosition;
            Direction = _direction;
            Rotation = _rotation;
        }

        public void UpdateGraphics(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height),
                null, Color.White, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0);
        }

    }
}
