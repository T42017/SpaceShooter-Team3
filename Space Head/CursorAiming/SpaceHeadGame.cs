using System;
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
        private GameState _gameState;
        private readonly GraphicsDeviceManager _graphics;
        private Texture2D _backgroudImage;
        private KeyboardState _previousKeyboardState;
        private Song _backgroundMusic;
        public static List<UnitWithGun> UnitsOnField = new List<UnitWithGun>();

        
        static public GameState _state = GameState.MainMenu;

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
            UnitsOnField.Add(new Player(300, 700, 1, 0.4f, UnitType.Player, UnitType.Enemy, this) { Position = new Vector2(700, 500) });
            UnitsOnField.Add(new BasicEnemyWithGun(150, 400, 1, 1f, UnitType.Enemy, UnitType.Player, this) { Position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2) });
            Components.Add(new UIComponent(this));
            Components.Add(new EnviornmentComponent(this));
            Components.Add(new MenuComponent(this));
            Components.Add(new ShopAndUpgradeComponent(this));
            Components.Add(new GameOverComponent(this));
            
            foreach (var unitWithGun in UnitsOnField)
            {           
                Components.Add(unitWithGun);
            }
            
            //_state = new States(this);
            //Components.Add(_state);

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
            _backgroudImage = Content.Load<Texture2D>("Background");
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
            {
                if (_gameState == GameState.Paused)
                {
                    ChangeCurrentGameState(GameState.Playing);
                }
                else if (_gameState != GameState.Paused)
                {
                    ChangeCurrentGameState(GameState.Paused);
                }
            }

            if (kbState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyUp(Keys.Space))
            {
                if (_gameState == GameState.GameOver)
                {
                    ChangeCurrentGameState(GameState.MainMenu);
                }
            }

            if (kbState.IsKeyDown(Keys.B) && _previousKeyboardState.IsKeyUp(Keys.B))
            {
                if (_gameState == GameState.ShopUpgradeMenu)
                {
                    ChangeCurrentGameState(GameState.Playing);
                }
                else if (_gameState != GameState.ShopUpgradeMenu)
                {
                    ChangeCurrentGameState(GameState.ShopUpgradeMenu);
                }
            }



            _previousKeyboardState = kbState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);



            _spriteBatch.Begin();

            //_spriteBatch.Draw(_backgroudImage, GraphicsDevice.Viewport.Bounds, Color.DarkGreen);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}