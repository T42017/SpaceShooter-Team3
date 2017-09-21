using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    public class Player : SpaceHeadBaseComponent
    {
        public static RectangleHitBox Hitbox;
        private static string _youLvldUp;

        public static Gun Gun;

        private Vector2 _aimDirection;
        private double _countDownTilNextAttack;
        private Vector2 _deltaDistance;
        private SpriteFont _font;

        private bool _isShooting;
        private Texture2D _lifeTexture;
        private bool _lvldUp;
        private Vector2 _moveDirection;

        private Texture2D _playerTexture;

        private float _rotation;

        private SoundEffect _takeDamage;
        private Vector2 _velocity;

        private double timer;

        public UnitType Type = UnitType.Player;

        private int xpNeeded;


        public Player(int moveSpeed, int health, float attackSpeed, Gun gun, Game game) : base(game)
        {
            MoveSpeed = moveSpeed;
            Health = health;
            _attackSpeed = attackSpeed;
            _countDownTilNextAttack = _attackSpeed;
            Gun = gun;
            PlayerLevel = 1;
            HealthLevel = 5;

            timer = 2;

            _youLvldUp = "LEVEL++";

            DrawOrder = 1;
            DrawableStates = GameState.Playing | GameState.Paused;

            PlayerPosition = new Vector2(Globals.ScreenWidth - 300, Globals.ScreenHeight / 2);
            UpdatableStates = GameState.Playing;

            Hitbox = new RectangleHitBox(2);

            game.Components.Add(this);
            game.Components.Add(Gun);
        }

        public static int ExpRequiredToLevel { get; private set; }

        public static int Xp { get; set; }
        public static int Points { get; set; }
        public static int Coins { get; set; }

        public override void Remove()
        {
            Game.Components.Remove(Gun);
            base.Remove();
        }

        public static void Reset()
        {
            Coins = 0;
            PlayerExp = 0;
            Points = 0;
            PlayerLevel = 1;
            MoveSpeedLevel = 0;
            HealthLevel = 0;
        }

        protected override void LoadContent()
        {
            _playerTexture = Game.Content.Load<Texture2D>("Player");
            _lifeTexture = Game.Content.Load<Texture2D>("spaceRocketParts_012");
            _font = Game.Content.Load<SpriteFont>("Font");
            //_takeDamage = Game.Content.Load<SoundEffect>("Ljudfiler/Jump");

            Hitbox.Box.Size = new Point(_playerTexture.Width / 2, _playerTexture.Width / 2);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var mouse = Mouse.GetState();

            xpNeeded = CalculateRequiredExpToLevel(PlayerLevel);
            if (Xp >= xpNeeded)
            {
                _lvldUp = true;
                PlayerLevel++;
                PlayerSkillPoints++;
                Xp -= xpNeeded;
            }
            Hitbox.UpdatePosition(PlayerPosition);

            ExpRequiredToLevel = CalculateRequiredExpToLevel(PlayerLevel);

            _isShooting = false;
            UpdateMovement(gameTime);
            CalculateRotation(new Vector2(mouse.X, mouse.Y));

            Gun.AimDirection = _aimDirection;
            Gun.Rotation = _rotation;
            Gun.Position = PlayerPosition + new Vector2(_aimDirection.X * (_playerTexture.Width - 7),
                               _aimDirection.Y * (_playerTexture.Width - 7));

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

            for (var i = 0; i < Gun.bulletsInAir.Count; i++)
            {
                Gun.bulletsInAir[i].UpdatePosition(gameTime);
                if (Gun.bulletsInAir[i].CheckForEnemyCollision(Wave.EnemiesOnField) ||
                    Gun.bulletsInAir[i].CheckForObstacleCollision())
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

        private int CalculateRequiredExpToLevel(int playerLevel)
        {
            float requiredExp;
            int wholeNumber;


            requiredExp = (float) (10 * Math.Pow(playerLevel, 2) + 360 * playerLevel);


            wholeNumber = (int) requiredExp;


            return wholeNumber;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();


            if (_lvldUp)
                if (timer > 0)
                {
                    timer -= gameTime.ElapsedGameTime.TotalSeconds;

                    SpriteBatch.DrawString(_font, _youLvldUp,
                        new Vector2(PlayerPosition.X - _font.MeasureString(_youLvldUp).X / 2, PlayerPosition.Y - 100),
                        Color.Green);
                }
                else
                {
                    _lvldUp = false;
                    timer = 2;
                }


            UpdateGraphics(SpriteBatch);

            SpriteBatch.End();

            base.Draw(gameTime);
        }


        #region Player Variable

        public static int PlayerLevel { get; set; }
        public static int PlayerExp { get; set; }
        public static int Health;
        public static int HealthLevel;
        public static int PlayerSkillPoints;
        public static int MoveSpeed { get; set; }
        public static int MoveSpeedLevel;
        public static double _attackSpeed;
        public static int AttackSpeedLevel = 0;
        public static Vector2 PlayerPosition;
        public static Vector2 PlayerStartPosition = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2);

        #endregion

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
                    new Vector2(Globals.ScreenHeight * 0.01f + i * 50, 0 + Globals.ScreenHeight * 0.01f), Color.Green);
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


            _velocity = Hitbox.CheckWalkingMoveDistance(MoveSpeed, _moveDirection,
                (float) gameTime.ElapsedGameTime.TotalSeconds);


            PlayerPosition += _velocity;
        }

        #endregion
    }
}