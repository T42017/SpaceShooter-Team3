using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    public class Gun : DrawableGameComponent
    {
        private readonly string _bulletTexturePath;
        private readonly int _damage;
        private readonly int _shotSpeed;
        private readonly string _texturePath;
        public List<Bullet> bulletsInAir = new List<Bullet>();
        private Texture2D _texture, _bulletTexture;
        public Vector2 Position, AimDirection;
        private UnitType _typeToHit;

        public float Rotation;
        private SpriteBatch spriteBatch;

        public Gun(string gunTexturePath, string bulletTexturePath, int damage, int shotSpeed, UnitType typeToHit, Game game) : base(game)
        {
            _texturePath = gunTexturePath;
            _bulletTexturePath = bulletTexturePath;
            _damage = damage;
            _shotSpeed = shotSpeed;
            DrawOrder = 10;
            _typeToHit = typeToHit;
        }

        public void Shoot()
        {
            bulletsInAir.Add(new Bullet(_shotSpeed, _damage, AimDirection, Position, Rotation, _bulletTexture, _typeToHit));
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
            _bulletTexture = Game.Content.Load<Texture2D>(_bulletTexturePath);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            UpdateGraphics(spriteBatch);
            foreach (var bullet in bulletsInAir)
                bullet.UpdateGraphics(spriteBatch);


            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}