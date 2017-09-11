﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    public class Player : SpaceHeadBaseComponent
    {
        public static Vector2 PlayerPosition;
        public static int Health;

        public static Rectangle Hitbox;
        private readonly double _attackSpeed;

        private readonly int _moveSpeed;
        public readonly Gun Gun;
        private Vector2 _aimDirection;
        private double _countDownTilNextAttack;

        private SoundEffect _damage;
        private Vector2 _deltaDistance;

        private bool _isShooting;
        private Texture2D _lifeTexture;
        private Vector2 _moveDirection;
        private Texture2D _playerTexture;
        private float _rotation;
        private SpriteBatch _spriteBatch;
        private Vector2 _velocity;

        public UnitType Type = UnitType.Player;

        public Player(int moveSpeed, int health, float attackSpeed, Gun gun, Game game) : base(game)
        {
            _moveSpeed = moveSpeed;
            Health = health;
            _attackSpeed = attackSpeed;
            _countDownTilNextAttack = _attackSpeed;
            Gun = gun;

            DrawOrder = 1;
            DrawableStates = GameState.Playing | GameState.Paused | GameState.ShopUpgradeMenu;

            PlayerPosition = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2);
            UpdatableStates = GameState.Playing;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _playerTexture = Game.Content.Load<Texture2D>("Player");
            _lifeTexture = Game.Content.Load<Texture2D>("spaceRocketParts_012");
            _damage = Game.Content.Load<SoundEffect>("Jump");

            Hitbox = new Rectangle((int) PlayerPosition.X - _playerTexture.Width / 2,
                (int) PlayerPosition.Y - _playerTexture.Height / 2, _playerTexture.Width, _playerTexture.Height);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var mouse = Mouse.GetState();
            Hitbox.Location = new Point((int) PlayerPosition.X - _playerTexture.Width / 2,
                (int) PlayerPosition.Y - _playerTexture.Height / 2);

            _isShooting = false;
            UpdateMovement(gameTime);
            CalculateRotation(new Vector2(mouse.X, mouse.Y));

            Gun.AimDirection = _aimDirection;
            Gun.Rotation = _rotation;
            Gun.Position = PlayerPosition + new Vector2(_aimDirection.X * (_playerTexture.Width + 5),
                               _aimDirection.Y * (_playerTexture.Width + 5));

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                _isShooting = true;

            if (_countDownTilNextAttack > 0)
                _countDownTilNextAttack -= (float) gameTime.ElapsedGameTime.TotalSeconds;
            //else
            {
                if (_isShooting)
                {
                    Gun.Shoot();
                    _countDownTilNextAttack = _attackSpeed;
                }
            }


            if (Health <= 0)
            {
                SpaceHeadGame.ChangeCurrentGameState(GameState.GameOver);
                Health = 5;
            }

            for (var i = 0; i < Gun.bulletsInAir.Count; i++)
            {
                Gun.bulletsInAir[i].UpdatePosition(gameTime);
                if (Gun.bulletsInAir[i].CheckForEnemyCollision(SpaceHeadGame.EnemyUnitsOnField))
                    Gun.bulletsInAir.Remove(Gun.bulletsInAir[i]);
            }

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
                spriteBatch.Draw(_lifeTexture,
                    new Vector2(Globals.ScreenHeight * 0.01f + i * 50, 0 + Globals.ScreenHeight * 0.01f), Color.White);
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