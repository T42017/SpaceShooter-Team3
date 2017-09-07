﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    public class Player : DrawableGameComponent
    {
        public static Vector2 PlayerPosition;
        public static int Health;
        private readonly double _attackSpeed;

        private readonly int _moveSpeed;
        private Vector2 _aimDirection;
        private double _countDownTilNextAttack;

        private SoundEffect _damage;
        private Vector2 _deltaDistance;
        public readonly Gun Gun;

        private CircleHitBox _hitbox;

        private bool _isShooting, _hasShot;
        private Texture2D _lifeTexture;
        private Vector2 _moveDirection;
        private Texture2D _playerTexture;
        private float _rotation;
        private SpriteBatch _spriteBatch;
        private Vector2 _velocity;

        public Player(int moveSpeed, int health, float attackSpeed, Gun gun, Game game) : base(game)
        {
            _moveSpeed = moveSpeed;
            Health = health;
            _attackSpeed = attackSpeed;
            _countDownTilNextAttack = _attackSpeed;
            Gun = gun;
            DrawOrder = 10;

            PlayerPosition = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _playerTexture = Game.Content.Load<Texture2D>("Player");
            _lifeTexture = Game.Content.Load<Texture2D>("spaceRocketParts_012");
            _damage = Game.Content.Load<SoundEffect>("Jump");

            _hitbox.Radius = _playerTexture.Width / 2;
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var mouse = Mouse.GetState();

            _isShooting = false;
            UpdateMovement(gameTime);
            CalculateRotation(new Vector2(mouse.X, mouse.Y));

            Gun.AimDirection = _aimDirection;
            Gun.Position = PlayerPosition;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                _isShooting = true;

            if (_countDownTilNextAttack > 0)
            {
                _countDownTilNextAttack -= (float) gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                if (_isShooting && !_hasShot)
                {
                    Shoot();
                    _countDownTilNextAttack = _attackSpeed;
                }
            }


            _hasShot = _isShooting;
            if (Health <= 0) Game.Exit();
            base.Update(gameTime);
        }

        public void CalculateRotation(Vector2 objectToPointAt)
        {
            _deltaDistance = objectToPointAt - PlayerPosition;
            _rotation = (float) Math.Atan2(_deltaDistance.Y, _deltaDistance.X);
            var tempDeltaDistance = _deltaDistance;
            tempDeltaDistance.Normalize();
            _aimDirection = tempDeltaDistance;
        }

        public void Shoot()
        {           
            Gun.Shoot();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            UpdateGraphics(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        #region OverrideMethods

        public void UpdateGraphics(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_playerTexture,
                new Rectangle((int) PlayerPosition.X, (int) PlayerPosition.Y, _playerTexture.Width,
                    _playerTexture.Height),
                null, Color.White, _rotation, new Vector2(_playerTexture.Width / 2, _playerTexture.Height / 2),
                SpriteEffects.None, 0);

            for (var i = 0; i < Health; i++)
                _spriteBatch.Draw(_lifeTexture, new Vector2(40 + i * 50, Globals.ScreenHeight - 100), Color.White);
        }

        public void UpdateMovement(GameTime gameTime)
        {
            _moveDirection = new Vector2(0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                _moveDirection.X += -1;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                _moveDirection.X += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                _moveDirection.Y += -1;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                _moveDirection.Y += 1;

            if ((int) _moveDirection.X != 0 && (int) _moveDirection.Y != 0) _moveDirection.Normalize();

            _velocity = _moveDirection * (_moveSpeed * gameTime.ElapsedGameTime.Milliseconds / 1000);
            PlayerPosition += _velocity;
        }

        #endregion
    }
}