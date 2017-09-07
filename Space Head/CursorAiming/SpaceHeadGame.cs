using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CursorAiming
{
    public class SpaceHeadGame : Game
    {
        public static List<Enemy> EnemyUnitsOnField = new List<Enemy>();
        private readonly GameState _gameState = GameState.MainMenu;
        private readonly GraphicsDeviceManager _graphics;

        private readonly BasicEnemyWithGun template1;
        private Texture2D _backgroudImage;
        private Song _backgroundMusic;
        private SpriteFont _font;
        private SpriteBatch _spriteBatch;
        private States _state;

        private string _totalScore;
        private Player player;

        public SpaceHeadGame()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            template1 = new BasicEnemyWithGun(new Gun(new Bullet(700, 1), "PlayerGun1", "laserBlue01", this), 400, 10,
                1d, "BasicEnemy");
        }

        protected override void Initialize()
        {
            _totalScore = "0";
            EnemyUnitsOnField.Add(new EnemyWIthGun(template1, this));

            foreach (var unitWithGun in EnemyUnitsOnField)
            {
                Components.Add(unitWithGun);
            }

            player = new Player(400, 5, 0.4f, new Gun(new Bullet(700, 1), "PlayerGun1", "laserBlue01", this), this);
            Components.Add(player);
            Components.Add(player.Gun);

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