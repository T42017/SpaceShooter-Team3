using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    internal class UnitWithGun : DrawableGameComponent
    {
        public Bullet bullet;
        public List<Bullet> BulletsInAir = new List<Bullet>();
        public int Health;
        public bool IsShooting, HasShot;
        public int MoveSpeed;
        public Vector2 Position, MoveDirection, Velocity, AimDirection;
        public float Rotation;
        public Texture2D Texture, BulletTexture, GunTexture;

        public UnitWithGun(Game game) : base(game)
        {
        }

        public void UpdateGraphics(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height),
                null, Color.White, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0);
        }

        public void Shoot()
        {
            bullet = new Bullet(700, 1, BulletTexture, Position , AimDirection, Rotation);
            BulletsInAir.Add(bullet);
        }

        public void CalculateRotation(Vector2 _objectToPointAt)
        {
            Vector2 deltaDistance;
            deltaDistance = _objectToPointAt - Position;
            Rotation = (float)Math.Atan2(deltaDistance.Y, deltaDistance.X);
            deltaDistance.Normalize();
            AimDirection = deltaDistance;
        }

        public virtual void UpdateMovement(GameTime gameTime)
        {            
        }
    }
}