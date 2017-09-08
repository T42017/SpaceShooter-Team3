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
            Gun.Position = Position;

            if (Health <= 0)
            {
            }

            Gun.Shoot();
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
            base.UpdateMovement(gameTime);
        }
    }
}