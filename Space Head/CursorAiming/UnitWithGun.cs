﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    internal class UnitWithGun : DrawableGameComponent
    {
        public Bullet Bullet;
        public List<Bullet> BulletsInAir = new List<Bullet>();
        public int Health;
        public bool IsShooting, HasShot;
        public int MoveSpeed, BulletSpeed, BulletDamage;
        public Vector2 Position, MoveDirection, Velocity, AimDirection;
        public float Rotation;
        public Texture2D Texture, BulletTexture, GunTexture;

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            
        }    

        public UnitWithGun(Game game) : base(game)
        {
 
        }

        public void Shoot(int bulletSpeed, int bulletDamage)
        {
            Bullet = new Bullet(bulletSpeed, bulletDamage, BulletTexture, Position , AimDirection, Rotation);
            BulletsInAir.Add(Bullet);
        }

        public Vector2 DeltaDistance;

        public void CalculateRotation(Vector2 objectToPointAt)
        {
            
            DeltaDistance = objectToPointAt - Position;
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