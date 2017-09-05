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

        public Bullet(int speed, int damage, Texture2D texture, Vector2 startPosition, Vector2 direction, float rotation)
        {
            Speed = speed;
            Texture = texture;
            Position = startPosition;
            Direction = direction;
            Rotation = rotation;
            
        }

       

        public void UpdateGraphics(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height),
                null, Color.White, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0);
        }

        public void UpdatePosition(GameTime gameTime)
        {
            Position += Direction * Speed * gameTime.ElapsedGameTime.Milliseconds / 1000;
        }
    }
}
