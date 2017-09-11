using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    internal class EnemyWIthGun : Enemy
    {
        public EnemyWIthGun(BasicEnemyWithGun template, Game game) : base(game)
        {
            Gun = template.Gun;
            MoveSpeed = template.MoveSpeed;
            Health = template.Health;
            AttackSpeed = template.AttackSpeed;
            CountDownTilNextAttack = AttackSpeed;
            TexturePath = template.TexturePath;
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            UnitTexture = Game.Content.Load<Texture2D>(TexturePath);
            Hitbox = new CircleHitBox(Position, UnitTexture.Width / 2);
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
            Gun.AimDirection = AimDirection;
            Gun.Rotation = Rotation;
            Gun.Position = Position + new Vector2(AimDirection.X * (UnitTexture.Width + 5),
                               AimDirection.Y * (UnitTexture.Width + 5));

            if (Health <= 0)
            {
                Player.PlayerGoldAmount += 2;
            }

            if (CountDownTilNextAttack > 0)
            {
                CountDownTilNextAttack -= (float) gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
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
            base.UpdateMovement(gameTime);
        }
    }
}