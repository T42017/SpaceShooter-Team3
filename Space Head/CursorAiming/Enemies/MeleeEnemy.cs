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

        public override void UpdateMovement(GameTime gameTime)
        {
            CalculateRotation(Player.PlayerPosition);

            MoveDirection = Player.PlayerPosition - Position;
            MoveDirection.Normalize();

            Velocity = Hitbox.CheckWalkingMoveDistance(MoveSpeed, MoveDirection,
                (float) gameTime.ElapsedGameTime.TotalSeconds);

            Position += Velocity;

            if (DeltaDistance.Length() < 30)
                Attack();

            base.UpdateMovement(gameTime);
        }

        protected void Attack()
        {
            Player.Health--;
            Remove();
        }

        public override void Remove()
        {
            Wave.EnemiesOnField.Remove(this);
            base.Remove();
        }
    }
}