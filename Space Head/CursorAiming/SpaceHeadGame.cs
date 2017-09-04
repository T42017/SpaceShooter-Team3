using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace CursorAiming
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class SpaceHeadGame : Game
    {
        private Texture2D playerTexture;
        private Texture2D bulletTexture;
        private Texture2D _backgroundTexture;
        UnitWithGun player;
        private readonly GraphicsDeviceManager graphics;
        private Song _backgroundMusic;
        private SoundEffect _shotSound;
        private float rotation;
        private SpriteBatch spriteBatch;


        public SpaceHeadGame()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        /// <summary>
        ///     Allows the game to perform any initialization it needs to before starting to run.
        ///     This is where it can query for any required services and load any non-graphic
        ///     related content.  Calling base.Initialize will enumerate through any components
        ///     and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            player = new Player(300, playerTexture, this);
            player.BulletTexture = bulletTexture;
            player.Position = new Vector2(500, 500);
            graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
            graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            graphics.ApplyChanges();
            IsMouseVisible = true;
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _backgroundTexture = Content.Load<Texture2D>("Background1");
            playerTexture = Content.Load<Texture2D>("spaceAstronauts_009");
            bulletTexture = Content.Load<Texture2D>("laserBlue01");
            _shotSound = Content.Load<SoundEffect>("Laser_Gun_Sound");
            _backgroundMusic = Content.Load<Song>("POL-flight-master-short");
            MediaPlayer.Play(_backgroundMusic);
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        ///     UnloadContent will be called once per game and is the place to unload
        ///     game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.UpdateMovement(gameTime);
        
            player.IsShooting = false;

            var mouse = Mouse.GetState();
            player.CalculateRotation(new Vector2(mouse.X, mouse.Y));           
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                player.IsShooting = true;
            }
            if (player.IsShooting && !player.HasShot) 
            {
                _shotSound.Play(0.3f, 0f, 0f);
                player.Shoot();
            }                
            foreach (Bullet bullet in player.BulletsInAir)
            {
                bullet.Position += bullet.Direction * bullet.Speed * gameTime.ElapsedGameTime.Milliseconds/1000;
            }  
            
            player.HasShot = player.IsShooting;


            base.Update(gameTime);
        }

        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //for (int x = 0; x < Globals.ScreenWidth; x += _backgroundTexture.Width)
            //{
            //    for (int y = 0; y < Globals.ScreenHeight; y += _backgroundTexture.Height)
            //    {
                    spriteBatch.Draw(_backgroundTexture, GraphicsDevice.Viewport.Bounds, Color.White);
            //    }
            //}

            foreach (Bullet bullet in player.BulletsInAir)
            {
                bullet.UpdateBulletGraphics(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}