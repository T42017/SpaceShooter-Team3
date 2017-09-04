using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursorAiming
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class SpaceHeadGame : Game
    {
        #region Texture2D variables

        private Texture2D playerTexture;
        private Texture2D start;
        private Texture2D exit;

        #endregion

        Player player;
        private Vector2 distanceBetweenPlaterAndMouse;
        private readonly GraphicsDeviceManager graphics;

        bool pushedStartGameButton = false;


        private float rotation;
        private SpriteBatch spriteBatch;

        private GameState _state = GameState.MainMenu;


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
            player = new Player(300, playerTexture);
            player.Position = new Vector2(500, 500);

            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;
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

            start = Content.Load<Texture2D>("start");
            exit = Content.Load<Texture2D>("exit");

            playerTexture = Content.Load<Texture2D>("spaceAstronauts_009");

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
            base.Update(gameTime);
            switch (_state)
            {
                case GameState.MainMenu:
                    UpdateMainMenu();
                    break;

                case GameState.GameIsRunning:
                    UpdateGameIsRunning(gameTime);
                    break;

                case GameState.GameOver:
                    break;
            }


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            player.IsShooting = false;
            var mouse = Mouse.GetState();

            distanceBetweenPlaterAndMouse.X = mouse.X - player.Position.X;
            distanceBetweenPlaterAndMouse.Y = mouse.Y - player.Position.Y;

            
            rotation = (float) Math.Atan2(distanceBetweenPlaterAndMouse.Y, distanceBetweenPlaterAndMouse.X);
            distanceBetweenPlaterAndMouse.Normalize();
            player.AimDirection = distanceBetweenPlaterAndMouse;

            player.MoveDirection = new Vector2(0,0);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.MoveDirection.X += -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.MoveDirection.X += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                player.MoveDirection.Y += -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                player.MoveDirection.Y += 1;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Player.Health = 0;
            }
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                player.IsShooting = true;
            if (player.IsShooting && !player.HasShot) player.Shoot();

            if(player.MoveDirection.X != 0 && player.MoveDirection.Y != 0)player.MoveDirection.Normalize();

            player.Velocity = player.MoveDirection * (player.MoveSpeed * gameTime.ElapsedGameTime.Milliseconds/1000);
            player.Position += player.Velocity;
            foreach (Bullet bullet in player.BulletsInAir)
            {
                bullet.Position += (bullet.Direction * bullet.Speed * (gameTime.ElapsedGameTime.Milliseconds)/1000);
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

            base.Update(gameTime);
            switch (_state)
            {
                case GameState.MainMenu:
                    break;

                case GameState.GameIsRunning:
                    break;

                case GameState.GameOver:
                    spriteBatch.Draw(playerTexture,
                new Rectangle(5, 5, playerTexture.Width, playerTexture.Height),
                null, Color.White, rotation, new Vector2(playerTexture.Width / 2, playerTexture.Height / 2), SpriteEffects.None, 0);
                    break;
            }

            spriteBatch.Draw(playerTexture,
                new Rectangle((int) player.Position.X, (int) player.Position.Y, playerTexture.Width, playerTexture.Height),
                null, Color.White, rotation, new Vector2(playerTexture.Width / 2, playerTexture.Height / 2), SpriteEffects.None, 0);

            foreach (Bullet bullet in player.BulletsInAir)
            {
                spriteBatch.Draw(bullet.Texture, bullet.Position, Color.White);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }

        enum GameState
        {
            MainMenu,
            GameIsRunning,
            EndOfGame,
            GameOver,
        }

        private void UpdateMainMenu()
        {
            throw new NotImplementedException();
        }

        void UpdateGameIsRunning(GameTime deltaTime)
        {
            // Respond to user input for menu selections, etc
            
            if (Player.Health <= 0)
                _state = GameState.GameOver;
        }



    }
}