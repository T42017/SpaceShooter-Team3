using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    internal class EnemyWithGun : Enemy
    {
        protected readonly Gun Gun;


        public EnemyWithGun(Gun gun, int moveSpeed, int health, double attackSpeed, string texturePath, int pointValue,
            int xpValue, int coinValue,
            Game game) : base(game)
        {
            Gun = gun;
            MoveSpeed = moveSpeed;
            Health = health;
            AttackSpeed = attackSpeed;
            CountDownTilNextAttack = AttackSpeed;
            TexturePath = texturePath;
            PointValue = pointValue;
            XpValue = xpValue;
            CoinValue = coinValue;
            Game.Components.Add(this);
            Game.Components.Add(Gun);
        }

        protected override void LoadContent()
        {
            UnitTexture = Game.Content.Load<Texture2D>(TexturePath);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch.Begin();

            UpdateGraphics(SpriteBatch);

            SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (Health <= 0)
                Remove();

            Gun.AimDirection = AimDirection;
            Gun.Rotation = Rotation;
            Gun.Position = Position + new Vector2(AimDirection.X * (UnitTexture.Width - 4),
                               AimDirection.Y * (UnitTexture.Width - 4));

            if (CountDownTilNextAttack > 0)
            {
                CountDownTilNextAttack -= (float) gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Gun.Shoot();
                CountDownTilNextAttack = AttackSpeed;
            }

           

            base.Update(gameTime);
        }

        public override void Remove()
        {
            Wave.EnemiesOnField.Remove(this);
            Game.Components.Remove(Gun);
            base.Remove();
        }

        public override void UpdateGraphics(SpriteBatch spriteBatch)
        {
            base.UpdateGraphics(spriteBatch);
        }

        public override void UpdateMovement(GameTime gameTime)
        {
            base.UpdateMovement(gameTime);


            if (DeltaDistance.Length() < 400)
            {
                MoveDirection = Player.PlayerPosition - Position;
                MoveDirection.Normalize();

                Velocity = Hitbox.CheckWalkingMoveDistance(MoveSpeed, -MoveDirection,
                    (float) gameTime.ElapsedGameTime.TotalSeconds);
                Position += Velocity;
            }
        }
    }
}