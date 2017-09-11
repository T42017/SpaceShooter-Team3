﻿using System.Collections.Generic;
using System.Xml.Schema;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    public class Bullet
    {
        private UnitType _typeToHit;
        public int Damage, Speed;
        public Vector2 Direction, Position;
        public float Rotation;
        public Texture2D Texture;

        public Bullet(int speed, int damage, Vector2 direction, Vector2 position, float rotation, Texture2D texture, UnitType typeToHit)
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
            Position += Direction * Speed * gameTime.ElapsedGameTime.Milliseconds / 1000;
        }

        public bool CheckForEnemyCollision(List<Enemy> UnitsToCollideWith)
        {
            for (int i = 0; i<UnitsToCollideWith.Count; i++)
                if (_typeToHit == UnitsToCollideWith[i].Type)
                    if (UnitsToCollideWith[i].Hitbox.Contains(Position))
                    {
                        UnitsToCollideWith[i].Health -= Damage;

                        return true;
                    }
            return false;
        }

        public bool CheckForPlayerCollision()
        {
            if (Player.Hitbox.Contains(Position))
            {
                //Player.Health--;
                return true;
            }
            return false;
        }
    }
}