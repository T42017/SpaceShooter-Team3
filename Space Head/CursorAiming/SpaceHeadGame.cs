﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CursorAiming
{
    public class SpaceHeadGame : Game
    {
        private Texture2D _backgroundTexture;
        private readonly GameState _gameState = GameState.MainMenu;
        private readonly GraphicsDeviceManager _graphics;
        private Song _backgroundMusic;
        private SoundEffect _shotSound;
        private UnitWithGun _enemy;        
        private UnitWithGun _player;
        private SpriteBatch _spriteBatch;
        private UnitWithGun _player;

        private SpriteBatch _spriteBatch;

        private States _state;

        public SpaceHeadGame()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _player = new Player(200, 500, 1, this) {Position = new Vector2(510, 500)};
            _enemy = new BasicEnemyWithGun(this) {Position = new Vector2(500, 500)};
            Components.Add(_player);
            Components.Add(_enemy);
            _graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            _graphics.ApplyChanges();
            #region windowSettings

            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            IsMouseVisible = true;

            #endregion

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _backgroundTexture = Content.Load<Texture2D>("Background");
            _shotSound = Content.Load<SoundEffect>("Laser_Gun_Sound");
            _backgroundMusic = Content.Load<Song>("POL-flight-master-short");
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
            {
                _shotSound.Play(0.05f, 0f, 0f);
                _player.Shoot(_player.BulletSpeed, _player.BulletDamage);
            }
            foreach (var bullet in _player.BulletsInAir)
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_backgroundTexture, GraphicsDevice.Viewport.Bounds, Color.White);
            _spriteBatch.End();
                bullet.UpdateGraphics(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}