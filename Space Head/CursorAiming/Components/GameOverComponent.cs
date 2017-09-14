using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    internal class GameOverComponent : SpaceHeadBaseComponent
    {
        private readonly string _diedMessage = "You have died..";

        private readonly string _returnMessage = "Press space to return to the main menu!";
        private Vector2 _diedMessageLength;

        private SpriteFont _font;
        private Texture2D _gameOverScreenBackground;
        private Vector2 _returnMessageLength;


        public GameOverComponent(Game game) : base(game)
        {
            DrawOrder = 10;
            UpdatableStates = GameState.GameOver | GameState.Playing;
            DrawableStates = GameState.GameOver;
        }

        protected override void LoadContent()
        {
            _gameOverScreenBackground = Game.Content.Load<Texture2D>("gameOverBackground");
            _font = Game.Content.Load<SpriteFont>("Font");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Player.Health <= 0)
                ResetGame();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(_gameOverScreenBackground, GraphicsDevice.Viewport.Bounds, Color.White);
            SpriteBatch.DrawString(_font, _diedMessage,
                new Vector2(Globals.ScreenWidth / 2 - _font.MeasureString(_diedMessage).X / 2,
                    Globals.ScreenHeight / 2), Color.Green);
            SpriteBatch.DrawString(_font, _returnMessage,
                new Vector2(Globals.ScreenWidth / 2 - _font.MeasureString(_returnMessage).X / 2,
                    Globals.ScreenHeight / 2 + Globals.ScreenHeight * 0.1f), Color.Green);

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        private void ResetGame()
        {
            SpaceHeadGame.ChangeCurrentGameState(GameState.GameOver);

            SpaceHeadGame.player.Remove();
            SpaceHeadGame.player = new Player(310, 5, 0.4f,
                new Gun("PlayerGun1", "laserBlue01", 20, 1500, UnitType.Enemy, Game),
                Game);


            Waves.Reset(Game);
        }
    }
}