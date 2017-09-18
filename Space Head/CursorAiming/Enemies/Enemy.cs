using System;
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


        protected string TexturePath;
        public UnitType Type = UnitType.Enemy;
        protected Texture2D UnitTexture;


        public Enemy(Game game) : base(game)
        {
            DrawOrder = 2;
            Hitbox = new RectangleHitBox(3);

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
            SpriteBatch.Begin();
            UpdateGraphics(SpriteBatch);
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            Hitbox.UpdatePosition(Position);

            CalculateRotation(Player.PlayerPosition);

            if (Health <= 0)
            {
                Game.Components.Remove(this);
                //Waves.EnemyUnitsOnField.Remove(this);
                Die();
                //if (Waves.EnemyUnitsOnField.Count == 0)
                //{
                //    Waves._waveRound++;
                //    Waves._enemyCount = 0;
                //}
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


        public virtual void UpdateGraphics(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UnitTexture,
                new Rectangle((int) Position.X, (int) Position.Y, UnitTexture.Width,
                    UnitTexture.Height),
                null, Color.White, Rotation, new Vector2(UnitTexture.Width / 2, UnitTexture.Height / 2),
                SpriteEffects.None, 0);
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