﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    public class Player : UnitWithGun
    {
        private Texture2D LifeTexture;
        public static Vector2 PlayerPosition;
        public Player(int moveSpeed, int bulletSpeed, int bulletDamage, float attackInterval, UnitType type, UnitType typeToHit, Game game) : base(game)
        {
            MoveSpeed = moveSpeed;
            BulletSpeed = bulletSpeed;
            BulletDamage = bulletDamage;
            Health = 5;
            AttackInterval = attackInterval;
            Countdown = AttackInterval;
            Type = type;
            TypeToHit = typeToHit;
            Position = new Vector2();
        }

        protected override void LoadContent()
        {
            Texture = Game.Content.Load<Texture2D>("Player");
            BulletTexture = Game.Content.Load<Texture2D>("laserBlue01");
            _shotSound = Game.Content.Load<SoundEffect>("Laser_Gun");
            LifeTexture = Game.Content.Load<Texture2D>("spaceRocketParts_012");

            HitBox.Radius = Texture.Width / 2;
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var mouse = Mouse.GetState();

            IsShooting = false;
            UpdateMovement(gameTime);
            CalculateRotation(new Vector2(mouse.X, mouse.Y));
            PlayerPosition = Position;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                IsShooting = true;

            if (Countdown > 0)
            {
                Countdown -= (float) gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                if (IsShooting && !HasShot)
                {
                    Shoot(BulletSpeed, BulletDamage, _shotSound);
                    Countdown = AttackInterval;
                }
            }


            HasShot = IsShooting;
            if (Health <= 0) Game.Exit();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            UpdateGraphics(SpriteBatch);

            foreach (var bullet in BulletsInAir)
                bullet.UpdateGraphics(SpriteBatch);
            SpriteBatch.End();

            base.Draw(gameTime);
            SpriteBatch.End();
        }

        #region OverrideMethods

        public override void UpdateGraphics(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                new Rectangle((int) Position.X, (int) Position.Y, Texture.Width, Texture.Height),
                null, Color.White, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0);

            for (int i = 0; i < Health; i++)
            {
                SpriteBatch.Draw(LifeTexture, new Vector2(40 + i * 50, Globals.ScreenHeight - 100), Color.White);

            }
        }

        public override void UpdateMovement(GameTime gameTime)
        {
            MoveDirection = new Vector2(0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                MoveDirection.X += -1;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                MoveDirection.X += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                MoveDirection.Y += -1;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                MoveDirection.Y += 1;

            if ((int) MoveDirection.X != 0 && (int) MoveDirection.Y != 0) MoveDirection.Normalize();

            Velocity = MoveDirection * (MoveSpeed * gameTime.ElapsedGameTime.Milliseconds / 1000);
            Position += Velocity;
        }

        #endregion
    }
}