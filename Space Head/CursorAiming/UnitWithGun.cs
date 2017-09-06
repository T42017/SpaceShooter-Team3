using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    public class UnitWithGun : DrawableGameComponent
    {
        private Game _game;
        public Bullet Bullet;
        public List<Bullet> BulletsInAir = new List<Bullet>();
        public UnitType Type, TypeToHit;

        public UnitWithGun(Game game) : base(game)
        {
            _game = game;
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch.Begin();
            
        }
        
        public override void Update(GameTime gameTime)
        {
            for(int i = 0; i < BulletsInAir.Count; i++)
            {
                BulletsInAir[i].UpdatePosition(gameTime);
                if (BulletsInAir[i].CheckForCollision(SpaceHeadGame.UnitsOnField))
                {
                    BulletsInAir.Remove(BulletsInAir[i]);
                    i--;
                }
            }

            HitBox.Middlepoint = Position;

            if (Health <= 0)
            {
                SpaceHeadGame.UnitsOnField.Remove(this);
            }

            base.Update(gameTime);
        }

        #region variables

        public int Health;
        public int MoveSpeed, BulletSpeed, BulletDamage;
        protected bool IsShooting, HasShot;
        public float AttackInterval;
        protected float Countdown;
        public CircleHitBox HitBox;

        #endregion

        #region MovementVariables

        public Vector2 Position, MoveDirection, Velocity, AimDirection;
        public Vector2 DeltaDistance;
        public float Rotation;

        #endregion

        #region Sound and textures

        public Texture2D Texture, BulletTexture, GunTexture;
        public SoundEffect _shotSound;
        protected SpriteBatch SpriteBatch { get; private set; }

        #endregion

        #region Methods

        public void Shoot(int bulletSpeed, int bulletDamage, SoundEffect sound)
        {
            Bullet = new Bullet(bulletSpeed, bulletDamage, BulletTexture, Position, AimDirection, TypeToHit, Rotation);
            BulletsInAir.Add(Bullet);
            sound.Play(0.3f, 0f, 0f);
        }

        public void CalculateRotation(Vector2 objectToPointAt)
        {
            DeltaDistance = objectToPointAt - Position;
            Rotation = (float) Math.Atan2(DeltaDistance.Y, DeltaDistance.X);
            var tempDeltaDistance = DeltaDistance;
            tempDeltaDistance.Normalize();
            AimDirection = tempDeltaDistance;
        }

        #endregion

        #region Virtual methods

        public virtual void UpdateGraphics(SpriteBatch spriteBatch)
        {
        }

        public virtual void UpdateMovement(GameTime gameTime)
        {
        }

        #endregion
    }

    public struct CircleHitBox
    {
        public Vector2 Middlepoint;
        public int Radius;

        public bool Contains(Vector2 point)
        {
            return (point - Middlepoint).Length() <= Radius;
        }
    }

    public enum UnitType
    {
        Enemy,
        Player
    }
}