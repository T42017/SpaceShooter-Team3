using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    class GameOverComponent : SpaceHeadBaseComponent
    {
        private Texture2D _gameOverScreenBackground;

        private SpriteFont _font;

        private string _diedMessage = "You have died..";
        private Vector2 _diedMeasure;

        private string _returnMessage = "Press space to return to the main menu!";
        private Vector2 _returnMeasure;


        public GameOverComponent(Game game) : base(game)
        {
            DrawOrder = 10;
            UpdatableStates = GameState.GameOver;
            DrawableStates = GameState.GameOver;
        }

        protected override void LoadContent()
        {
            _gameOverScreenBackground = Game.Content.Load<Texture2D>("gameOverBackground");
            _font = Game.Content.Load<SpriteFont>("Font");
            _diedMeasure = _font.MeasureString(_diedMessage);
            _returnMeasure = _font.MeasureString(_returnMessage);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(_gameOverScreenBackground, GraphicsDevice.Viewport.Bounds, Color.White);
            SpriteBatch.DrawString(_font, _diedMessage, new Vector2(Globals.ScreenWidth/2 - _diedMeasure.X/2, Globals.ScreenHeight/2), Color.Green );
            SpriteBatch.DrawString(_font, _returnMessage, new Vector2(Globals.ScreenWidth / 2 - _returnMeasure.X / 2, Globals.ScreenHeight / 2 + Globals.ScreenHeight * 0.1f), Color.Green);

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {


            base.Update(gameTime);
        }
    }
}
