using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CursorAiming
{
    
    public class SpaceHeadGame : Game
    {
        private Texture2D _backgroundTexture;
        private readonly GraphicsDeviceManager _graphics;
        private Song _backgroundMusic;
        private SoundEffect _shotSound;
        private UnitWithGun _enemy;        
        private UnitWithGun _player;
        private SpriteBatch _spriteBatch;


        public SpaceHeadGame()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            _player = new Player(400, 1000, 1, this) {Position = new Vector2(500, 500)};
            _enemy = new BasicEnemyWithGun(this) {Position = new Vector2(500, 500)};
            Components.Add(_player);
            _graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            _graphics.ApplyChanges();
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _backgroundTexture = Content.Load<Texture2D>("Background");
            _shotSound = Content.Load<SoundEffect>("Laser_Gun_Sound");
            _backgroundMusic = Content.Load<Song>("POL-flight-master-short");
            MediaPlayer.Play(_backgroundMusic);
            MediaPlayer.Volume = 0.05f;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.UpdateMovement(gameTime);

            _player.IsShooting = false;

            var mouse = Mouse.GetState();

            _player.CalculateRotation(new Vector2(mouse.X, mouse.Y));


            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                _player.IsShooting = true;

            if (_player.IsShooting && !_player.HasShot)
            {
                _shotSound.Play(0.05f, 0f, 0f);
                _player.Shoot(_player.BulletSpeed, _player.BulletDamage);
            }
            foreach (var bullet in _player.BulletsInAir)
                bullet.Position += bullet.Direction * bullet.Speed * gameTime.ElapsedGameTime.Milliseconds / 1000;
            foreach (var bullet in _enemy.BulletsInAir)
                bullet.Position += bullet.Direction * bullet.Speed * gameTime.ElapsedGameTime.Milliseconds / 1000;

            _enemy.CalculateRotation(_player.Position);

            if (_enemy.DeltaDistance.Length() < 700) _enemy.Shoot(700, 1);

            _player.HasShot = _player.IsShooting;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_backgroundTexture, GraphicsDevice.Viewport.Bounds, Color.White);

            foreach (var bullet in _player.BulletsInAir)
                bullet.UpdateGraphics(_spriteBatch);

            _player.UpdateGraphics(_spriteBatch);
            _enemy.UpdateGraphics(_spriteBatch);
            foreach (var bullet in _enemy.BulletsInAir)
                bullet.UpdateGraphics(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}