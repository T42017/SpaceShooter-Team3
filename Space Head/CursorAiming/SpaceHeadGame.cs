using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CursorAiming
{
    public class SpaceHeadGame : Game
    {
        public static List<UnitWithGun> UnitsOnField = new List<UnitWithGun>();
        private readonly GameState _gameState = GameState.MainMenu;
        private readonly GraphicsDeviceManager _graphics;
        private Texture2D _backgroudImage;
        private Song _backgroundMusic;
        private SpriteFont _font;
        private SpriteBatch _spriteBatch;
        private States _state;

        private string _totalScore;

        public SpaceHeadGame()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _totalScore = "0";
            UnitsOnField.Add(
                new Player(500, 1000, 1, 0.4f, UnitType.Player, UnitType.Enemy,
                    this) {Position = new Vector2(700, 500)});
            UnitsOnField.Add(new BasicEnemyWithGun(200, 1000, 1, 1f, UnitType.Enemy, UnitType.Player, this)
            {
                Position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2)
            });
            foreach (var unitWithGun in UnitsOnField)
                Components.Add(unitWithGun);

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
            _backgroudImage = Content.Load<Texture2D>("Background");
            _backgroundMusic = Content.Load<Song>("POL-flight-master-short");
            _font = Content.Load<SpriteFont>("Font");
            MediaPlayer.Play(_backgroundMusic);
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;
            base.LoadContent();
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

            _state.CheckPlayerInput(_gameState);

            _totalScore = Points.Score.ToString();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroudImage, GraphicsDevice.Viewport.Bounds, Color.DarkGreen);
            _spriteBatch.DrawString(_font, _totalScore, new Vector2(35, 20), Color.Aqua);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}