using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    internal class EnemyWIthGun : Enemy
    {
        public Gun Gun;


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
            Hitbox.MiddlePoint = Position;


            if (Health <= 0)
            {
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