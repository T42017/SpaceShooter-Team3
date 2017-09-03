using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class SpaceHeadGame : Game
    {
        private Texture2D playerTexture;
        Player player; 
        private Vector2 distance;
        private readonly GraphicsDeviceManager graphics;


        private float rotation;
        private SpriteBatch spriteBatch;
        //private Vector2 spritePosition = new Vector2(500, 500);


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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var mouse = Mouse.GetState();

            distance.X = mouse.X - player.Position.X;
            distance.Y = mouse.Y - player.Position.Y;

            rotation = (float) Math.Atan2(distance.Y, distance.X);

            player.Direction = new Vector2(0,0);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.Direction.X += -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.Direction.X += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                player.Direction.Y += -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                player.Direction.Y += 1;
            }
            if(player.Direction.X != 0 && player.Direction.Y != 0)player.Direction.Normalize();

            player.Velocity = player.Direction * (player.MoveSpeed * gameTime.ElapsedGameTime.Milliseconds/1000);
            player.Position += player.Velocity;

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

            spriteBatch.Draw(playerTexture,
                new Rectangle((int) player.Position.X, (int) player.Position.Y, playerTexture.Width, playerTexture.Height), null,
                Color.White, rotation, new Vector2(playerTexture.Width / 2, playerTexture.Height / 2), SpriteEffects.None, 0);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}