using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming.Enemies
{
    internal class MeleeEnemy : Enemy
    {
        public MeleeEnemy(int moveSpeed, int health, string texturePath, int pointValue,
            int xpValue, int coinValue, Game game) : base(game)
        {
            MoveSpeed = moveSpeed;
            Health = health;
            TexturePath = texturePath;
            PointValue = pointValue;
            XpValue = xpValue;
            CoinValue = coinValue;
            DrawOrder = 2;
            Game.Components.Add(this);

        }


        protected override void LoadContent()
        {
            UnitTexture = Game.Content.Load<Texture2D>(TexturePath);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            UpdateGraphics(SpriteBatch);
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            UpdateMovement(gameTime);

            base.Update(gameTime);
        }

        public override void UpdateMovement(GameTime gameTime)
        {
            if (DeltaDistance.Length() < 700)
            {
                MoveDirection = Player.PlayerPosition - Position;
                MoveDirection.Normalize();

                Velocity = Hitbox.CheckMoveDistance(MoveSpeed, MoveDirection,
                    (float) gameTime.ElapsedGameTime.TotalSeconds);

                Position += Velocity;
            }
            if (DeltaDistance.Length() < 30)
                Attack();

            base.UpdateMovement(gameTime);
        }

        public override void UpdateGraphics(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UnitTexture,
                new Rectangle((int) Position.X, (int) Position.Y, UnitTexture.Width,
                    UnitTexture.Height),
                null, Color.White, Rotation, new Vector2(UnitTexture.Width / 2, UnitTexture.Height / 2),
                SpriteEffects.None, 0);
            base.UpdateGraphics(spriteBatch);
        }

        private void Attack()
        {
            Player.Health--;
            Remove();
        }
    }
}