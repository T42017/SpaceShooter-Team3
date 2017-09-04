using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    internal class BasicEnemyWithGun : UnitWithGun
    {
        public BasicEnemyWithGun(Game game) : base(game)
        {
            Texture = Game.Content.Load<Texture2D>("spaceAstronauts_009");
            BulletTexture = Game.Content.Load<Texture2D>("laserBlue01");
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (DeltaDistance.Length() < 700) Shoot(500, 1);

            foreach (var bullet in BulletsInAir)
                bullet.Position += bullet.Direction * bullet.Speed * gameTime.ElapsedGameTime.Milliseconds / 1000;
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }


        public override void UpdateGraphics(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                new Rectangle((int) Position.X, (int) Position.Y, Texture.Width, Texture.Height),
                null, Color.White, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0);
        }
    }
}