using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    internal class UnitWithGun : DrawableGameComponent
    {
        private Game _game;
        public Bullet Bullet;
        public List<Bullet> BulletsInAir = new List<Bullet>();

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
            for (int i = 0; i < Health; i++)
            {
                SpriteBatch.Draw(lifeTexture, new Vector2(40 + i * 50, 950), Color.White);

            }
        }
        
        public override void Update(GameTime gameTime)
        {
            foreach (var bullet in BulletsInAir)
                bullet.UpdatePosition(gameTime);

            HitBox.Middlepoint = Position;

            base.Update(gameTime);
        }

        #region variables

        public int Health = 3;
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

        public Texture2D Texture, BulletTexture, GunTexture, lifeTexture;
        public SoundEffect _shotSound;
        protected SpriteBatch SpriteBatch { get; private set; }

        #endregion

        #region Methods

        public void Shoot(int bulletSpeed, int bulletDamage, SoundEffect sound)
        {
            Bullet = new Bullet(bulletSpeed, bulletDamage, BulletTexture, Position, AimDirection, Rotation);
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

    internal struct CircleHitBox
    {
        public Vector2 Middlepoint;
        public int Radius;

        public bool Contains(Vector2 point)
        {
            return (point - Middlepoint).Length() <= Radius;
        }
    }
}