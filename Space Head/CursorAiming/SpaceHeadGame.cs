﻿using System.Collections.Generic;
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
        public static List<Enemy> EnemyUnitsOnField = new List<Enemy>();
        private readonly GraphicsDeviceManager _graphics;

        private readonly BasicEnemyWithGun template1;
        private Texture2D _backgroudImage;
        private KeyboardState _previousKeyboardState;
        private Song _backgroundMusic;
        private SpriteFont _font;
        static public GameState _state = GameState.MainMenu;

        private SpriteBatch _spriteBatch;


        private string _totalScore;
        private Player player;
        

        public SpaceHeadGame()
        {
            

            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            template1 = new BasicEnemyWithGun(new Gun("PlayerGun1", "laserBlue01", 1, 700, UnitType.player,  this), 400, 10,
                1d, "BasicEnemy");
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
            EnemyUnitsOnField.Add(new EnemyWIthGun(template1, this));

            foreach (var unitWithGun in EnemyUnitsOnField)
           {
                Components.Add(unitWithGun);
                Components.Add(unitWithGun.Gun);
            }

            Components.Add(new UIComponent(this));
            Components.Add(new EnviornmentComponent(this));
            Components.Add(new MenuComponent(this));
            Components.Add(new ShopAndUpgradeComponent(this));
            Components.Add(new GameOverComponent(this));
            
            

            player = new Player(400, 5, 0.4f, new Gun("PlayerGun1", "laserBlue01", 1, 700, UnitType.enemy, this), this);
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

    public enum UnitType
    {
        enemy,
        player
    }
}