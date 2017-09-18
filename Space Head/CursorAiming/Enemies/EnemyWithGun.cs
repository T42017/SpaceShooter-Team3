using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    internal class EnemyWithGun : Enemy
    {
        private readonly Gun Gun;

        private SoundEffect _damage;
        //private Texture2D _astronautMarine;
        //private Texture2D _astronautBlue;
        //private Texture2D _astronautRed;
        //private Texture2D _astronautWhite;
        //private Texture2D _astronautBrown;
        //private Texture2D _astronautYellow;
        //private Texture2D _astronautPink;
        //private Texture2D _astronautGreen;

        public EnemyWithGun(Gun gun, int moveSpeed, int health, double attackSpeed, string texturePath, int pointValue,
            int xpValue, int coinValue,
            Game game) : base(game)
        {
            Gun = gun;
            MoveSpeed = moveSpeed;
            Health = health;
            AttackSpeed = attackSpeed;
            CountDownTilNextAttack = AttackSpeed;
            TexturePath = texturePath;
            PointValue = pointValue;
            XpValue = xpValue;
            CoinValue = coinValue;
            Game.Components.Add(this);
            Game.Components.Add(Gun);
        }

        protected override void LoadContent()
        {
            _damage = Game.Content.Load<SoundEffect>("STRONK");
            UnitTexture = Game.Content.Load<Texture2D>(TexturePath);

            //_astronautGreen = Game.Content.Load<Texture2D>("spaceAstronaut_green");
            //_astronautRed = Game.Content.Load<Texture2D>("spaceAstronaut_red");
            //_astronautBlue = Game.Content.Load<Texture2D>("spaceAstronaut_marineblue");
            //_astronautMarine = Game.Content.Load<Texture2D>("spaceAstronaut_marine");
            //_astronautPink = Game.Content.Load<Texture2D>("spaceAstronaut_pink");
            //_astronautWhite = Game.Content.Load<Texture2D>("spaceAstronaut_002");
            //_astronautBrown = Game.Content.Load<Texture2D>("spaceAstronaut_brown");
            //_astronautYellow = Game.Content.Load<Texture2D>("spaceAstronaut_yellow");

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch.Begin();

            UpdateGraphics(SpriteBatch);

            SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (Health <= 0)
                Remove();

            UpdateMovement(gameTime);

            Gun.AimDirection = AimDirection;
            Gun.Rotation = Rotation;
            Gun.Position = Position + new Vector2(AimDirection.X * (UnitTexture.Width + 5),
                               AimDirection.Y * (UnitTexture.Width + 5));

            if (CountDownTilNextAttack > 0)
            {
                CountDownTilNextAttack -= (float) gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (DeltaDistance.Length() < 780)
            {
                Gun.Shoot();
                CountDownTilNextAttack = AttackSpeed;
            }

            for (var i = 0; i < Gun.bulletsInAir.Count; i++)
            {
                Gun.bulletsInAir[i].UpdatePosition(gameTime);
                if (Gun.bulletsInAir[i].CheckForPlayerCollision())
                {
                    Gun.bulletsInAir.Remove(Gun.bulletsInAir[i]);
                }
            }


            base.Update(gameTime);
        }

        public override void Remove()
        {
            Wave.EnemiesOnField.Remove(this);
            Game.Components.Remove(Gun);
            base.Remove();
        }

        public override void UpdateGraphics(SpriteBatch spriteBatch)
        {
            base.UpdateGraphics(spriteBatch);
        }

        public override void UpdateMovement(GameTime gameTime)
        {
            base.UpdateMovement(gameTime);

            if (DeltaDistance.Length() > 700)
            {
                MoveDirection = Player.PlayerPosition - Position;
                MoveDirection.Normalize();

                Velocity = Hitbox.CheckMoveDistance(MoveSpeed, MoveDirection,
                    (float) gameTime.ElapsedGameTime.TotalSeconds);

                Position += Velocity;
            }

            else if (DeltaDistance.Length() < 500)
            {
                MoveDirection = Player.PlayerPosition - Position;
                MoveDirection.Normalize();
                Velocity = Hitbox.CheckMoveDistance(MoveSpeed, -MoveDirection,
                    (float) gameTime.ElapsedGameTime.TotalSeconds);
                Position += Velocity;
            }
        }
    }
}