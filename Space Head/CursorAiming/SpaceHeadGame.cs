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
        public static List<Enemy> EnemyUnitsOnField = new List<Enemy>();
        private readonly GraphicsDeviceManager _graphics;

        private readonly BasicEnemyWithGun template1;
        private Song _backgroundMusic;
        private SpriteFont _font;
        private GameState _gameState;
        private KeyboardState _previousKeyboardState;

        private SpriteBatch _spriteBatch;


        private string _totalScore;
        private Player player;


        public SpaceHeadGame()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            template1 = new BasicEnemyWithGun(new Gun("PlayerGun1", "laserBlue01", 1, 700, UnitType.Player, this), 200,
                10, 1d, "BasicEnemy");
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
            player = new Player(400, 5, 0.4f, new Gun("PlayerGun1", "laserBlue01", 1, 700, UnitType.Enemy, this), this);

            EnemyUnitsOnField.Add(new EnemyWIthGun(template1, this));

            EnemyUnitsOnField.Add(new EnemyWIthGun(template1, this));


            Components.Add(new UIComponent(this));
            Components.Add(new EnviornmentComponent(this));
            Components.Add(new MenuComponent(this));
            Components.Add(new ShopAndUpgradeComponent(this));
            Components.Add(new GameOverComponent(this));


            Components.Add(player);
            Components.Add(player.Gun);


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
            MediaPlayer.Volume = 0.5f;
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