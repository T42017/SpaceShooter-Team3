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
        public int MoveSpeed, BulletSpeed, BulletDamage;
        public Vector2 Position, MoveDirection, Velocity, AimDirection;
        public float Rotation;
        public Texture2D Texture, BulletTexture, GunTexture;

        public UnitWithGun(Game game) : base(game)
        {
        }

        public void Shoot(int _bulletSpeed, int _bulletDamage)
        {
            bullet = new Bullet(_bulletSpeed, _bulletDamage, BulletTexture, Position , AimDirection, Rotation);
            BulletsInAir.Add(bullet);
        }

        public Vector2 DeltaDistance;

        public void CalculateRotation(Vector2 _objectToPointAt)
        {
            
            DeltaDistance = _objectToPointAt - Position;
            Rotation = (float)Math.Atan2(DeltaDistance.Y, DeltaDistance.X);
            Vector2 tempDeltaDistance = DeltaDistance;
            tempDeltaDistance.Normalize();
            AimDirection = tempDeltaDistance;
        }

        public virtual void UpdateGraphics(SpriteBatch spriteBatch)
        {
            
        }

        public virtual void UpdateMovement(GameTime gameTime)
        {            
        }
    }
}