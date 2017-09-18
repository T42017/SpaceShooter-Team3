using System;
using System.Runtime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    public class Enemy : SpaceHeadBaseComponent
    {
        protected double AttackSpeed;
        protected double CountDownTilNextAttack;
        protected Vector2 DeltaDistance;
        public int Health;

        public RectangleHitBox Hitbox;
        protected Vector2 MoveDirection, Velocity, AimDirection;

        protected int MoveSpeed;
        protected int PointValue, XpValue, CoinValue;
        public Vector2 Position;
        public float Rotation;
        private Texture2D _coneView;

        protected string TexturePath;
        public UnitType Type = UnitType.Enemy;
        protected Texture2D UnitTexture;


        public Enemy(Game game) : base(game)
        {
            DrawOrder = 1;
            Hitbox = new RectangleHitBox(3);
            CanEnemySeePlayer(Player.PlayerPosition, Position, Player.PlayerPosition);
            DrawableStates = GameState.Playing | GameState.Paused;

            UpdatableStates = GameState.Playing;
        }

        protected override void LoadContent()
        {
            Hitbox.Box.Size = new Point(UnitTexture.Width, UnitTexture.Width);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            Hitbox.UpdatePosition(Position);
            //CanEnemySeePlayer(Player.PlayerPosition, Position, Player.PlayerPosition);

            CalculateRotation(Player.PlayerPosition);

            if (Health <= 0)
            {
                Game.Components.Remove(this);
                Waves.EnemyUnitsOnField.Remove(this);
                Die();
                if (Waves.EnemyUnitsOnField.Count == 0)
                {
                    Waves._waveRound++;
                    Waves._enemyCount = 0;
                }
            }
            base.Update(gameTime);
        }

        public void CalculateRotation(Vector2 objectToPointAt)
        {
            DeltaDistance = objectToPointAt - Position;
            Rotation = (float) Math.Atan2(DeltaDistance.Y, DeltaDistance.X);
            var tempDeltaDistance = DeltaDistance;
            tempDeltaDistance.Normalize();
            AimDirection = tempDeltaDistance;
        }

        public bool CanEnemySeePlayer(Vector2 enemyLookAtDirection, Vector2 EnemyPosition, Vector2 PlayerPosition)
        {
            float ConeNithyDegreesDotProduct = (float) Math.Cos(MathHelper.ToRadians(90f / 2f));
            Vector2 directionEnemyToPlayer = PlayerPosition - EnemyPosition;
            directionEnemyToPlayer.Normalize();

            Color[] coneColors = new Color[SpaceHeadGame.Graphics.PreferredBackBufferWidth * SpaceHeadGame.Graphics.PreferredBackBufferHeight];
            for (int x = 0; x < SpaceHeadGame.Graphics.PreferredBackBufferWidth; x++)
            {
                for (int y = 0; y < SpaceHeadGame.Graphics.PreferredBackBufferHeight; y++)
                {
                    Vector2 pixel = new Vector2(x, y);
                    Vector2 directionEnemyToPixel = pixel - EnemyPosition;
                    directionEnemyToPixel.Normalize();
                    if (Vector2.Dot(directionEnemyToPixel, enemyLookAtDirection) > ConeNithyDegreesDotProduct)
                        coneColors[x + y * SpaceHeadGame.Graphics.PreferredBackBufferWidth] = new Color(120, 80, 80, 200);
                    else
                        coneColors[x + y * SpaceHeadGame.Graphics.PreferredBackBufferWidth] = Color.Transparent;
                }
            }

            _coneView = new Texture2D(GraphicsDevice, SpaceHeadGame.Graphics.PreferredBackBufferWidth, SpaceHeadGame.Graphics.PreferredBackBufferHeight, false, SurfaceFormat.Color);
            _coneView.SetData(coneColors);
            return Vector2.Dot(directionEnemyToPlayer, enemyLookAtDirection) > ConeNithyDegreesDotProduct;
        }

        public virtual void UpdateGraphics(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UnitTexture,
                new Rectangle((int) Position.X, (int) Position.Y, UnitTexture.Width,
                    UnitTexture.Height),
                null, Color.White, Rotation, new Vector2(UnitTexture.Width / 2, UnitTexture.Height / 2),
                SpriteEffects.None, 0);
            spriteBatch.Draw(_coneView, Position, Color.Red);
        }

        public virtual void UpdateMovement(GameTime gameTime)
        {
            Velocity = Hitbox.CheckMoveDistance(MoveSpeed, MoveDirection,
                (float) gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void Die()
        {
            Player.Xp += XpValue;
            Player.Coins += CoinValue;
            Player.Points += PointValue;
        }
    }
}