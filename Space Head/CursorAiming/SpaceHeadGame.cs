using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    
    public class SpaceHeadGame : Game
    {
        private readonly GraphicsDeviceManager graphics;

        private UnitWithGun enemy;        
        private UnitWithGun player;

        private SpriteBatch spriteBatch;


        public SpaceHeadGame()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            player = new Player(400, 1000, 1, this) {Position = new Vector2(500, 500)};
            enemy = new BasicEnemyWithGun(this) {Position = new Vector2(500, 500)};
            Components.Add(player);

            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();

            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.UpdateMovement(gameTime);

            player.IsShooting = false;

            var mouse = Mouse.GetState();

            player.CalculateRotation(new Vector2(mouse.X, mouse.Y));


            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                player.IsShooting = true;

            if (player.IsShooting && !player.HasShot) player.Shoot(player.BulletSpeed, player.BulletDamage);

            foreach (var bullet in player.BulletsInAir)
                bullet.Position += bullet.Direction * bullet.Speed * gameTime.ElapsedGameTime.Milliseconds / 1000;
            foreach (var bullet in enemy.BulletsInAir)
                bullet.Position += bullet.Direction * bullet.Speed * gameTime.ElapsedGameTime.Milliseconds / 1000;

            enemy.CalculateRotation(player.Position);

            if (enemy.DeltaDistance.Length() < 700) enemy.Shoot(700, 1);

            player.HasShot = player.IsShooting;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            base.Update(gameTime);

            player.UpdateGraphics(spriteBatch);
            foreach (var bullet in player.BulletsInAir)
                bullet.UpdateGraphics(spriteBatch);

            player.UpdateGraphics(spriteBatch);
            enemy.UpdateGraphics(spriteBatch);

            foreach (var bullet in enemy.BulletsInAir)
                bullet.UpdateGraphics(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}