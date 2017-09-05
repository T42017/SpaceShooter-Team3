using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    class BasicEnemyWithGun : UnitWithGun
    {

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {

        }

        public BasicEnemyWithGun(Game game) : base(game)
        {
            Texture = this.Game.Content.Load<Texture2D>("spaceAstronauts_009");
            BulletTexture = this.Game.Content.Load<Texture2D>("laserBlue01");
        }



        public override void UpdateGraphics(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height),
                null, Color.White, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0);
        }
    }
}
