using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    public class Gun : DrawableGameComponent
    {
        private readonly Bullet _bullet;
        private readonly string _bulletTexturePath;
        private readonly string _texturePath;
        private readonly List<Bullet> bulletsInAir = new List<Bullet>();
        private Texture2D _texture;
        public Vector2 Position, AimDirection;

        public float Rotation;
        private SpriteBatch spriteBatch;

        public Gun(Bullet bullet, string gunTexturePath, string bulletTexturePath, Game game) : base(game)
        {
            _bullet = bullet;
            _texturePath = gunTexturePath;
            _bulletTexturePath = bulletTexturePath;
        }

        public void Shoot()
        {
            _bullet.Direction = AimDirection;
            _bullet.Position = Position;
            bulletsInAir.Add(_bullet);
        }

        public void UpdateGraphics(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture,
                new Rectangle((int) Position.X, (int) Position.Y, _texture.Width, _texture.Height),
                null, Color.White, Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), SpriteEffects.None,
                0);
        }

        public void UpdatePosition()
        {
            
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _texture = Game.Content.Load<Texture2D>(_texturePath);
            _bullet.Texture = Game.Content.Load<Texture2D>(_bulletTexturePath);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Begin();

            UpdateGraphics(spriteBatch);
            foreach (Bullet bullet in bulletsInAir)
            {
                bullet.UpdateGraphics(spriteBatch);
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Bullet bullet in bulletsInAir)
            {
                bullet.UpdatePosition(gameTime);
            }

            base.Update(gameTime);
        }
    }
}