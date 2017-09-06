﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    public class Bullet
    {
        public int Damage, Speed;
        public float Rotation;
        public Texture2D Texture;
        public Vector2 Direction, Position;
        private UnitType _unitToHit;

        public Bullet(int speed, int damage, Texture2D texture, Vector2 startPosition, Vector2 direction, UnitType unitToHit, float rotation)
        {
            Speed = speed;
            Damage = damage;
            Texture = texture;
            Position = startPosition;
            Direction = direction;
            Rotation = rotation;
            _unitToHit = unitToHit;
            
        }

        public void UpdateGraphics(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height),
                null, Color.White, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0);
        }

        public void UpdatePosition(GameTime gameTime)
        {
            Position += Direction * Speed * gameTime.ElapsedGameTime.Milliseconds / 1000;
        }

        public bool CheckForCollision(List<UnitWithGun> UnitsToCollideWith)
        {
            foreach (var unitToCollideWith in UnitsToCollideWith)
            {
                if (_unitToHit == unitToCollideWith.Type)
                {
                    if (unitToCollideWith.HitBox.Contains(Position))
                    {
                        unitToCollideWith.Health -= Damage;
                        return true;
                    }
                    
                }
                
            }
            return false;
        }


    }
}
