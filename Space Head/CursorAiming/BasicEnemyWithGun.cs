using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    internal class BasicEnemyWithGun : UnitWithGun
    {
        public BasicEnemyWithGun(Game game) : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            UpdateGraphics(SpriteBatch);

            foreach (var bullet in BulletsInAir)
                bullet.UpdateGraphics(SpriteBatch);

            SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (DeltaDistance.Length() < 700) Shoot(500, 1, _shotSound);


            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            Texture = Game.Content.Load<Texture2D>("spaceAstronauts_009");
            BulletTexture = Game.Content.Load<Texture2D>("laserBlue01");
            _shotSound = Game.Content.Load<SoundEffect>("Laser_Gun_Sound");

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