using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    internal class EnemyWithGun : Enemy
    {
        private readonly Gun Gun;

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
                Game.Components.Remove(Gun);

            UpdateMovement(gameTime);

            Gun.AimDirection = AimDirection;
            Gun.Rotation = Rotation;
            Gun.Position = Position + new Vector2(AimDirection.X * (UnitTexture.Width + 5),
                               AimDirection.Y * (UnitTexture.Width + 5));

            if (CountDownTilNextAttack > 0)
            {
                CountDownTilNextAttack -= (float) gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (DeltaDistance.Length() < 300)
            {
                Gun.Shoot();
                CountDownTilNextAttack = AttackSpeed;
            }

            for (var i = 0; i < Gun.bulletsInAir.Count; i++)
            {
                Gun.bulletsInAir[i].UpdatePosition(gameTime);
                if (Gun.bulletsInAir[i].CheckForPlayerCollision())
                    Gun.bulletsInAir.Remove(Gun.bulletsInAir[i]);
            }


            base.Update(gameTime);
        }

        public override void UpdateGraphics(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UnitTexture,
                new Rectangle((int) Position.X, (int) Position.Y, UnitTexture.Width,
                    UnitTexture.Height),
                null, Color.White, Rotation, new Vector2(UnitTexture.Width / 2, UnitTexture.Height / 2),
                SpriteEffects.None, 0);

            base.UpdateGraphics(spriteBatch);
        }

        public override void UpdateMovement(GameTime gameTime)
        {
            if (DeltaDistance.Length() > 200)
            {
                MoveDirection = Player.PlayerPosition - Position;
                MoveDirection.Normalize();

                Velocity = MoveDirection * (int) (MoveSpeed * gameTime.ElapsedGameTime.TotalSeconds);
                Position += Velocity;
            }
            else if (DeltaDistance.Length() < 190)
            {
                MoveDirection = Player.PlayerPosition - Position;
                MoveDirection.Normalize();

                Velocity = MoveDirection * (int) (MoveSpeed * gameTime.ElapsedGameTime.TotalSeconds);
                Position -= Velocity;
            }
            base.UpdateMovement(gameTime);
        }
    }
}