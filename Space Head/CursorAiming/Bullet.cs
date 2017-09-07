using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    public class Bullet
    {
        public int Damage, Speed;
        public Vector2 Direction, Position;
        public float Rotation;
        public Texture2D Texture;

        public Bullet(int speed, int damage)
        {
            Speed = speed;
            Damage = damage;
        }


        public void UpdateGraphics(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                new Rectangle((int) Position.X, (int) Position.Y, Texture.Width, Texture.Height),
                null, Color.White, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None,
                0);
        }

        public void UpdatePosition(GameTime gameTime)
        {
            Position += Direction * Speed * gameTime.ElapsedGameTime.Milliseconds / 1000;
        }

        


        //            if (unitToCollideWith.HitBox.Contains(Position))
        //        {
        //        if (_unitToHit == unitToCollideWith.Type)
        //    {
        //    foreach (var unitToCollideWith in UnitsToCollideWith)
        //{

        //public bool CheckForCollision(List<UnitWithGun> UnitsToCollideWith)
        //            {
        //                unitToCollideWith.Health -= Damage;
        //                return true;
        //            }

        //        }

        //    }
        //    return false;
        //}
    }
}