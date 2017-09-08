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
        public Gun Gun;
        public int Health;

        public CircleHitBox Hitbox;

        protected bool IsShooting, HasShot;
        protected int MoveSpeed;
        protected Vector2 Position, MoveDirection, Velocity, AimDirection;
        protected float Rotation;

        protected SpriteBatch SpriteBatch;
        protected string TexturePath;
        public UnitType Type = UnitType.Enemy;
        protected Texture2D UnitTexture;


        public Enemy(Game game) : base(game)
        {
            Position = new Vector2(500, 500);
            DrawOrder = 2;

            DrawableStates = GameState.Playing | GameState.Paused | GameState.ShopUpgradeMenu;

            UpdatableStates = GameState.Playing;
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            Hitbox.MiddlePoint = Position;


            CalculateRotation(Player.PlayerPosition);
            if (Health <= 0)
                SpaceHeadGame.EnemyUnitsOnField.Remove(this);


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
        }
    }

    public struct BasicEnemyWithGun
    {
        public Gun Gun;
        public int MoveSpeed, Health;
        public string TexturePath;
        public double AttackSpeed;

        public BasicEnemyWithGun(Gun gun, int moveSpeed, int health, double attackSpeed, string texturePath)
        {
            Gun = gun;
            MoveSpeed = moveSpeed;
            Health = health;
            AttackSpeed = attackSpeed;
            TexturePath = texturePath;
        }
    }
}