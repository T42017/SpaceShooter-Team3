using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming.Enemies
{
    class SpaceShipSpawn : MeleeEnemy
    {
        public SpaceShipSpawn(int moveSpeed, int health, string texturePath, int pointValue, int xpValue, int coinValue, Game game) : base(moveSpeed, health, texturePath, pointValue, xpValue, coinValue, game)
        {
            MoveSpeed = moveSpeed;
            Health = health;
            TexturePath = texturePath;
            PointValue = pointValue;
            XpValue = xpValue;
            CoinValue = coinValue;
            DrawOrder = 2;
        }

        protected override void LoadContent()
        {
            UnitTexture = Game.Content.Load<Texture2D>(TexturePath);

            base.LoadContent();
        }

        public override void UpdateMovement(GameTime gameTime)
        {
            MoveDirection = Player.PlayerPosition - Position;
            MoveDirection.Normalize();

                Velocity = Hitbox.CheckFlyingMoveDistance(MoveSpeed, MoveDirection,
                    (float)gameTime.ElapsedGameTime.TotalSeconds);

            Position += Velocity;
        }
    }


}
