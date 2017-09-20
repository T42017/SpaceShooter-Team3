using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    public class Bullet
    {
        private readonly UnitType _typeToHit;
        public int Damage, Speed;
        public Vector2 Direction, Position;
        public float Rotation;
        public Texture2D Texture;


        public Bullet(int speed, int damage, Vector2 direction, Vector2 position, float rotation, Texture2D texture,
            UnitType typeToHit)
        {
            Speed = speed;
            Damage = damage;
            Direction = direction;
            Position = position;
            Rotation = rotation;
            Texture = texture;
            _typeToHit = typeToHit;
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
            Position += Direction * (int) (Speed * gameTime.ElapsedGameTime.TotalSeconds);
        }

        public bool CheckForEnemyCollision(List<Enemy> unitsToCollideWith)
        {
            for (var i = 0; i < unitsToCollideWith.Count; i++)
                if (_typeToHit == unitsToCollideWith[i].Type)
                    if (unitsToCollideWith[i].Hitbox.CollidesWith(Position))
                    {
                        unitsToCollideWith[i].Health -= Damage;

                        return true;
                    }
            return false;
        }

        public bool CheckForPlayerCollision()
        {
            if (Player.Hitbox.CollidesWith(Position))
            {
                Player.Health--;
                return true;
            }
            return false;
        }

        public bool CheckForObstacleCollision()
        {
            foreach (var rectangle in EnviornmentComponent.Borders)
                if (rectangle.Contains(Position)) return true;
            foreach (var rectangle in EnviornmentComponent.ObstaclesOnField)
                if (rectangle.Contains(Position)) return true;

            return false;
        }
    }
}