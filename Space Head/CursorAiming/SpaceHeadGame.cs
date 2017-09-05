using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CursorAiming
{
    public class SpaceHeadGame : Game
    {
        private readonly GameState _gameState = GameState.MainMenu;
        private readonly GraphicsDeviceManager _graphics;
        private Song _backgroundMusic;
        private Texture2D _backgroundTexture;
        private UnitWithGun _enemy;
        private UnitWithGun _player;
        private SoundEffect _shotSound;
        private SpriteBatch _spriteBatch;


        private States _state;

        public SpaceHeadGame()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _player = new Player(200, 500, 1, this) {Position = new Vector2(510, 400)};
            _enemy = new BasicEnemyWithGun(this) {Position = new Vector2(500, 500)};
            Components.Add(_player);
            Components.Add(_enemy);
            _state = new States(this);
            Components.Add(_state);


            #region windowSettings

            _graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            _graphics.ApplyChanges();
            IsMouseVisible = true;

            #endregion

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _backgroundTexture = Content.Load<Texture2D>("Background");
            _backgroundMusic = Content.Load<Song>("POL-flight-master-short");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_backgroundMusic);
            MediaPlayer.Volume = 0.05f;
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _state.CheckPlayerInput(_gameState);

            _enemy.CalculateRotation(_player.Position);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_backgroundTexture, GraphicsDevice.Viewport.Bounds, Color.White);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}