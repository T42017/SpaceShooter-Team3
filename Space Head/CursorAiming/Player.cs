﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    internal class Player : UnitWithGun
    {
       
        
            Texture = this.Game.Content.Load<Texture2D>("spaceAstronauts_009"); 
            BulletTexture = this.Game.Content.Load<Texture2D>("laserBlue01");

        public Player(int _moveSpeed, int _bulletSpeed, int _bulletDamage, Game game) : base(game)
        {
            MoveSpeed = _moveSpeed;
            BulletSpeed = _bulletSpeed;
            Texture = this.Game.Content.Load<Texture2D>("spaceAstronauts_009"); 
            BulletTexture = this.Game.Content.Load<Texture2D>("laserBlue01");
            BulletDamage = _bulletDamage;
            Texture = this.Game.Content.Load<Texture2D>("spaceAstronauts_009");
            BulletTexture = this.Game.Content.Load<Texture2D>("laserBlue01");
        }

        public override void UpdateGraphics(SpriteBatch spriteBatch)
        public Player(int _moveSpeed, int _bulletSpeed, int _bulletDamage, Game game) : base(game)
        {
            spriteBatch.Draw(Texture,
            BulletSpeed = _bulletSpeed;
            BulletDamage = _bulletDamage;
                new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height),
                null, Color.White, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0);
            Texture = this.Game.Content.Load<Texture2D>("spaceAstronauts_009");
            BulletTexture = this.Game.Content.Load<Texture2D>("laserBlue01");
        }

        public Player(int _moveSpeed, int _bulletSpeed, int _bulletDamage, Game game) : base(game)
        {
            spriteBatch.Draw(Texture,
            BulletSpeed = _bulletSpeed;
            BulletDamage = _bulletDamage;
            Texture = this.Game.Content.Load<Texture2D>("spaceAstronauts_009");
            BulletTexture = this.Game.Content.Load<Texture2D>("laserBlue01");
        }
        public override void UpdateGraphics(SpriteBatch spriteBatch)
        public Player(int _moveSpeed, int _bulletSpeed, int _bulletDamage, Game game) : base(game)
        {
            spriteBatch.Draw(Texture,
            BulletSpeed = _bulletSpeed;
            BulletDamage = _bulletDamage;
            Texture = this.Game.Content.Load<Texture2D>("spaceAstronauts_009");
            BulletTexture = this.Game.Content.Load<Texture2D>("laserBlue01");
        }
        public override void UpdateGraphics(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height),
                null, Color.White, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0);
        }
        public override void UpdateGraphics(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height),
                null, Color.White, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0);
        }
        public override void UpdateMovement(GameTime gameTime)
        {
            MoveDirection = new Vector2(0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                MoveDirection.X += -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                MoveDirection.X += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                MoveDirection.Y += -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                MoveDirection.Y += 1;
            }
 
            if (MoveDirection.X != 0 && MoveDirection.Y != 0) MoveDirection.Normalize();

            Velocity = MoveDirection * (MoveSpeed * gameTime.ElapsedGameTime.Milliseconds / 1000);
            Position += Velocity;
        }
        
    }
}