using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming.Enemies
{
    internal class SpaceShip1 : Enemy
    {
        private readonly double _timeBetweenSpawns;
        private double _timeTilSpawn;

        public SpaceShip1(int moveSpeed, int health, string texturePath, int pointValue,
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
            _timeBetweenSpawns = 3;
            _timeTilSpawn = _timeBetweenSpawns;
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
            if (DeltaDistance.Length() > 600)
                Velocity = Hitbox.CheckFlyingMoveDistance(MoveSpeed, MoveDirection,
                    (float) gameTime.ElapsedGameTime.TotalSeconds);
            else
                Velocity = Hitbox.CheckFlyingMoveDistance(MoveSpeed, -MoveDirection,
                    (float) gameTime.ElapsedGameTime.TotalSeconds);
            Position += Velocity;
        }

        public override void Update(GameTime gameTime)
        {
            if (_timeTilSpawn > 0)
            {
                _timeTilSpawn -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Wave.SpawnMeleeEnemy(Game, Position);
                _timeTilSpawn = _timeBetweenSpawns;
            }
            base.Update(gameTime);
        }

        public override void Remove()
        {
            Wave.EnemiesOnField.Remove(this);
            base.Remove();
        }
    }
}