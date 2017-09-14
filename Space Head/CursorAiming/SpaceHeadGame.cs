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

        public GameState GameState { get; private set; }
        private KeyboardState _previousKeyboardState;

        private SpriteBatch _spriteBatch;

        private string _totalScore;
        private Player player;
        
        public SpaceHeadGame()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            template1 = new BasicEnemyWithGun(new Gun("PlayerGun1", "laserBlue01", 1, 700, UnitType.Player, this), 200,
                50, 1d, "BasicEnemy");
        }

        public void ChangeCurrentGameState(GameState wantedState)
        {
            GameState = wantedState;

            foreach (var component in Components.Cast<SpaceHeadBaseComponent>())
            {
                component.Visible = component.DrawableStates.HasFlag(GameState);
                component.Enabled = component.UpdatableStates.HasFlag(GameState);
            }
        }

        protected override void Initialize()
        {
            player = new Player(400, 5, 0.4f, new PlayerGun("PlayerGun1", "laserBlue01", 1, 700, UnitType.Enemy, this), this);
            EnemyUnitsOnField.Add(new EnemyWIthGun(template1, this));

            foreach (var unitWithGun in EnemyUnitsOnField)
            {
                Components.Add(unitWithGun);
                Components.Add(unitWithGun.Gun);
            }

            #region Components
            Components.Add(new UIComponent(this));
            Components.Add(new EnviornmentComponent(this));
            Components.Add(new MenuComponent(this));
            Components.Add(new ShopAndUpgradeComponent(this));
            Components.Add(new GameOverComponent(this));
            Components.Add(new MouseComponent(this));
            Components.Add(player);
            Components.Add(Player.Gun);
            #endregion

            #region windowSettings

            _graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            _graphics.ApplyChanges();

            IsMouseVisible = false;

            #endregion

            ChangeCurrentGameState(GameState.MainMenu);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _backgroundMusic = Content.Load<Song>("heman");
            MediaPlayer.Play(_backgroundMusic);
            MediaPlayer.Volume = 1f;
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
                if (GameState == GameState.Playing)
                    ChangeCurrentGameState(GameState.Paused);
                else if (GameState == GameState.Paused)
                    ChangeCurrentGameState(GameState.Playing);

            if (kbState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyUp(Keys.Space))
                if (GameState == GameState.GameOver)
                    ChangeCurrentGameState(GameState.MainMenu);

            if (kbState.IsKeyDown(Keys.B) && _previousKeyboardState.IsKeyUp(Keys.B))
                if (GameState == GameState.Playing)
                    ChangeCurrentGameState(GameState.ShopUpgradeMenu);
                else if (GameState == GameState.ShopUpgradeMenu)
                    ChangeCurrentGameState(GameState.Playing);

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