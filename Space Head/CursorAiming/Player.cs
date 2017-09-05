using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    internal class Player : UnitWithGun
    {
        public Player(int moveSpeed, int bulletSpeed, int bulletDamage, Game game) : base(game)
        {
            MoveSpeed = moveSpeed;
            BulletSpeed = bulletSpeed;
            Texture = Game.Content.Load<Texture2D>("spaceAstronauts_009");
            BulletTexture = Game.Content.Load<Texture2D>("laserBlue01");
        }

        protected override void LoadContent()
        {
            Texture = Game.Content.Load<Texture2D>("spaceAstronauts_009");
            BulletTexture = Game.Content.Load<Texture2D>("laserBlue01");

            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {       

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void UpdateGraphics(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                new Rectangle((int) Position.X, (int) Position.Y, Texture.Width, Texture.Height),
                null, Color.White, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0);
        }

        public override void UpdateMovement(GameTime gameTime)
        {
            MoveDirection = new Vector2(0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                MoveDirection.X += -1;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                MoveDirection.X += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                MoveDirection.Y += -1;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                MoveDirection.Y += 1;

            if (MoveDirection.X != 0 && MoveDirection.Y != 0) MoveDirection.Normalize();

            Velocity = MoveDirection * (MoveSpeed * gameTime.ElapsedGameTime.Milliseconds / 1000);
            Position += Velocity;
        }
    }
}