using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CursorAiming
{
    public class SpaceHeadGame : Game
    {
        private GameState _gameState;
        private readonly GraphicsDeviceManager _graphics;
        private Texture2D _backgroudImage;

        private KeyboardState _previousKeyboardState;
        private Song _backgroundMusic;

        private UnitWithGun _enemy;
        private UnitWithGun _player;

        private SpriteBatch _spriteBatch;

        //private States _state;

        

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
            Components.Add(new DecorationComponent(this));
            Components.Add(new MenuComponent(this));

            _player = new Player(400, 1000, 1, this) {Position = new Vector2(510, 500)};
            _enemy = new BasicEnemyWithGun(this) {Position = new Vector2(500, 500)};
            Components.Add(_player);
            Components.Add(_enemy);

            //_state = new States(this);
            //Components.Add(_state);

            #region windowSettings

            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();

            IsMouseVisible = true;

            #endregion

            ChangeCurrentGameState(GameState.MainMenu);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _backgroudImage = Content.Load<Texture2D>("Background");
            _backgroundMusic = Content.Load<Song>("POL-flight-master-short");
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

            var kbState = Keyboard.GetState();

            

            if (kbState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyUp(Keys.Space))
            {
                ChangeCurrentGameState(GameState.Playing);
            }

            _previousKeyboardState = kbState;

            //_state.CheckPlayerInput(_gameState);

            _enemy.CalculateRotation(_player.Position);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            //_spriteBatch.Draw(_backgroudImage, GraphicsDevice.Viewport.Bounds, Color.DarkGreen);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}