using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    internal class BasicEnemyWithGun : UnitWithGun
    {
        public BasicEnemyWithGun(int moveSpeed, int bulletSpeed, int bulletDamage, float attackInterval, UnitType type, UnitType typeToHit, Game game) : base(game)
        {
            MoveSpeed = moveSpeed;
            BulletSpeed = bulletSpeed;
            BulletDamage = bulletDamage;
            AttackInterval = attackInterval;
            Countdown = AttackInterval;
            Type = type;
            TypeToHit = typeToHit;
            Health = 1;
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
            if (Countdown > 0)
            {
                Countdown -= (float) gameTime.ElapsedGameTime.TotalSeconds;
            }
            
            else
            {
                if (DeltaDistance.Length() < 700)
                {
                    Shoot(BulletSpeed, BulletDamage, _shotSound);
                    Countdown = AttackInterval;
                }
            }
            CalculateRotation(Player.PlayerPosition);

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            Texture = Game.Content.Load<Texture2D>("BasicEnemy");
            BulletTexture = Game.Content.Load<Texture2D>("laserBlue01");
            _shotSound = Game.Content.Load<SoundEffect>("Laser_Gun");
            _enemyDamage = Game.Content.Load<SoundEffect>("Wound");

            HitBox.Radius = Texture.Width / 2;

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