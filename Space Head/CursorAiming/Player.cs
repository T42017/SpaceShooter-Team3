using System;
using System.Collections.Generic;
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
        public static RectangleHitBox Hitbox;
        public static int PlayerGoldAmount = 999999;
        public static Gun Gun;

        private readonly double _attackSpeed;
        private readonly int _moveSpeed;
        private Vector2 _aimDirection;
        private double _countDownTilNextAttack;
        private Vector2 _deltaDistance;

        private bool _isShooting;
        private Texture2D _lifeTexture;
        private Vector2 _moveDirection;

        private Texture2D _playerTexture;

        private float _rotation;

        private SpriteBatch _spriteBatch;
        private SoundEffect _takeDamage;
        private Vector2 _velocity;
        private Texture2D Obstacletexture;

        public UnitType Type = UnitType.Player;


        public Player(int moveSpeed, int health, float attackSpeed, Gun gun, Game game) : base(game)
        {
            _moveSpeed = moveSpeed;
            Health = health;
            _attackSpeed = attackSpeed;
            _countDownTilNextAttack = _attackSpeed;
            Gun = gun;

            DrawOrder = 1;
            DrawableStates = GameState.Playing | GameState.Paused;

            PlayerPosition = new Vector2(Globals.ScreenWidth, Globals.ScreenHeight);
            UpdatableStates = GameState.Playing;

            Hitbox = new RectangleHitBox(2);

            game.Components.Add(this);
            game.Components.Add(Gun);
        }

        public static int Xp { get; set; }
        public static int Points { get; set; }
        public static int Coins { get; set; }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _playerTexture = Game.Content.Load<Texture2D>("Player");
            _lifeTexture = Game.Content.Load<Texture2D>("spaceRocketParts_012");
            _takeDamage = Game.Content.Load<SoundEffect>("Jump");
            Obstacletexture = Game.Content.Load<Texture2D>("gameOverBackground");

            Hitbox.Box.Size = new Point(_playerTexture.Width / 2, _playerTexture.Width / 2);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var mouse = Mouse.GetState();
            Hitbox.UpdatePosition(PlayerPosition);

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
            {
                _countDownTilNextAttack -= (float) gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                if (_isShooting)
                {
                    Gun.Shoot();
                    _countDownTilNextAttack = _attackSpeed;
                }
            }

            if (Health <= 0)
            {
                Waves.EnemyUnitsOnField.Clear();
                SpaceHeadGame.ChangeCurrentGameState(GameState.GameOver);
                Health = 5;
            }

            for (var i = 0; i < Gun.bulletsInAir.Count; i++)
            {
                Gun.bulletsInAir[i].UpdatePosition(gameTime);
                if (Gun.bulletsInAir[i].CheckForEnemyCollision(Waves.EnemyUnitsOnField))
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

        private void CheckForEnvironmentCollision(List<RectangleHitBox> Obstacles)
        {
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

            _spriteBatch.Draw(Obstacletexture, SpaceHeadGame.ObstaclesOnField[0], Color.White);
        }

        public void UpdateMovement(GameTime gameTime)
        {
            _moveDirection = Vector2.Zero;
            _velocity = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                _moveDirection.X -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                _moveDirection.X += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                _moveDirection.Y -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                _moveDirection.Y += 1;

            if ((int) _moveDirection.X != 0 && (int) _moveDirection.Y != 0) _moveDirection.Normalize();


            _velocity = Hitbox.CheckMoveDistance(_moveSpeed, _moveDirection,
                (float) gameTime.ElapsedGameTime.TotalSeconds);


            PlayerPosition += _velocity;
        }

        #endregion
    }
}