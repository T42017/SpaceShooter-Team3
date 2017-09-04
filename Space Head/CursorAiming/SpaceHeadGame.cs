using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    public class SpaceHeadGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;

        private UnitWithGun _enemy;
        private UnitWithGun _player;
        private SpriteBatch _spriteBatch;


        public SpaceHeadGame()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            _player = new Player(200, 500, 1, this) {Position = new Vector2(501, 500)};
            _enemy = new BasicEnemyWithGun(this) {Position = new Vector2(500, 500)};
            Components.Add(_player);
            Components.Add(_enemy);

            #region windowSettings

            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();

            IsMouseVisible = true;

            #endregion
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
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

            _enemy.CalculateRotation(_player.Position);

            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            base.Update(gameTime);

            _player.UpdateGraphics(_spriteBatch);
            foreach (var bullet in _player.BulletsInAir)
                bullet.UpdateGraphics(_spriteBatch);

            _player.UpdateGraphics(_spriteBatch);
            _enemy.UpdateGraphics(_spriteBatch);

            foreach (var bullet in _enemy.BulletsInAir)
                bullet.UpdateGraphics(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}