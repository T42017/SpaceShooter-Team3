using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    public enum SelectedWeapon
    {
        AssaultRifle,
        RocketLauncher
    }


    public class Gun : SpaceHeadBaseComponent
    {
        private readonly string _bulletTexturePath;
        private readonly string _texturePath;
        private readonly UnitType _typeToHit;
        private Texture2D _texture, _bulletTexture;
        public int Damage;
        public static int GunAtkLevel = 1;
        public static int GunAtkSpeedLevel = 1;
        public static int GunLevel = 1;
        public Vector2 Position, AimDirection;

        public float Rotation;
        public int ShotSpeed;
        private SpriteBatch spriteBatch;

        public Gun(string gunTexturePath, string bulletTexturePath, int damage, int shotSpeed, UnitType typeToHit,
            Game game) : base(game)
        {
            _texturePath = gunTexturePath;
            _bulletTexturePath = bulletTexturePath;
            Damage = damage;
            ShotSpeed = shotSpeed;
            _typeToHit = typeToHit;

            DrawOrder = 3;

            DrawableStates = GameState.Playing | GameState.Paused;

            UpdatableStates = GameState.Playing;
        }


        public void Shoot()
        {
            EnviornmentComponent.BulletsInAir.Add(
                new Bullet(ShotSpeed, Damage, AimDirection, Position, Rotation, _bulletTexture, _typeToHit));
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
            

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}