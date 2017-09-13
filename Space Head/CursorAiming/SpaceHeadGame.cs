using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CursorAiming
{
    public class SpaceHeadGame : Game
    {
        public static Waves Wave;

        public static List<Rectangle> ObstaclesOnField = new List<Rectangle>();

        private readonly GraphicsDeviceManager _graphics;

        private Song _backgroundMusic;
        private SpriteFont _font;
        private GameState _gameState;
        private KeyboardState _previousKeyboardState;

        private SpriteBatch _spriteBatch;
        private string _totalScore;
        private Player _player;
        private Enemy _enemy;
        private Waves _wave;
        

        public SpaceHeadGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
        }

        public void ChangeCurrentGameState(GameState wantedState)
        {
            _gameState = wantedState;

            foreach (var component in Components.Cast<SpaceHeadBaseComponent>())
            {
                component.Visible = component.DrawableStates.HasFlag(_gameState);
                component.Enabled = component.UpdatableStates.HasFlag(_gameState);
            }
        }

        protected override void Initialize()
        {
            _player = new Player(310, 5, 0.4f, new Gun("PlayerGun1", "laserBlue01", 1, 700, UnitType.Enemy, this), this);

            Components.Add(new UIComponent(this));
            Components.Add(new EnviornmentComponent(this));
            Components.Add(new MenuComponent(this));
            Components.Add(new ShopAndUpgradeComponent(this));
            Components.Add(new GameOverComponent(this));
            _wave = new Waves(this);
            Components.Add(_player);
            Components.Add(_player.Gun);


            ObstaclesOnField.Add(new Rectangle(Globals.ScreenWidth / 2 - 100, Globals.ScreenHeight / 2 - 100, 100,
                200));

            #region windowSettings

            _graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            _graphics.ApplyChanges();

            IsMouseVisible = true;

            #endregion

            ChangeCurrentGameState(GameState.MainMenu);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _backgroundMusic = Content.Load<Song>("POL-flight-master-short");
            MediaPlayer.Play(_backgroundMusic);
            MediaPlayer.Volume = 0.3f;
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

            var kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Keys.V) && _previousKeyboardState.IsKeyUp(Keys.V))
                _wave.SpawnEnemy();

            if (kbState.IsKeyDown(Keys.P) && _previousKeyboardState.IsKeyUp(Keys.P))
                if (_gameState == GameState.Paused)
                    ChangeCurrentGameState(GameState.Playing);
                else if (_gameState != GameState.Paused)
                    ChangeCurrentGameState(GameState.Paused);

            if (kbState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyUp(Keys.Space))
                if (_gameState == GameState.GameOver)
                    ChangeCurrentGameState(GameState.MainMenu);

            if (kbState.IsKeyDown(Keys.B) && _previousKeyboardState.IsKeyUp(Keys.B))
                if (_gameState == GameState.ShopUpgradeMenu)
                    ChangeCurrentGameState(GameState.Playing);
                else if (_gameState != GameState.ShopUpgradeMenu)
                    ChangeCurrentGameState(GameState.ShopUpgradeMenu);


            _previousKeyboardState = kbState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            _spriteBatch.Begin();


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    public enum UnitType
    {
        Enemy,
        Player
    }
}