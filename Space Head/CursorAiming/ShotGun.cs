using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    public class ShotGun : Gun
    {
        public int GunAtkLevel;
        public int GunAtkSpeedLevel;
        private readonly string _bulletTexturePath;
        public int Damage;
        public int ShotSpeed;
        private readonly string _texturePath;
        private readonly UnitType _typeToHit;
        private Texture2D _texture, _bulletTexture;
        public List<Bullet> bulletsInAir = new List<Bullet>();
        public Vector2 Position, AimDirection;

        public ShotGun(string gunTexturePath, string bulletTexturePath, int damage, int shotSpeed, UnitType typeToHit, Game game) : base(gunTexturePath, bulletTexturePath, damage, shotSpeed, typeToHit, game)
        {
            _texturePath = gunTexturePath;
            _bulletTexturePath = bulletTexturePath;
            Damage = damage;
            ShotSpeed = shotSpeed;
            _typeToHit = typeToHit;
        }

        public void Shoot()
        {
            

        }

        protected override void LoadContent()
        {
            _texture = Game.Content.Load<Texture2D>(_texturePath);
            _bulletTexture = Game.Content.Load<Texture2D>(_bulletTexturePath);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            UpdateGraphics(SpriteBatch);
            foreach (var bullet in bulletsInAir)
                bullet.UpdateGraphics(SpriteBatch);

            SpriteBatch.End();
            base.Draw(gameTime);
        }


    }
}
