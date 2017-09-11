using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    class EnviornmentComponent : SpaceHeadBaseComponent
    {
        private Texture2D _backgroundTexture2D;
        private SpriteFont _font;

        private int _currentPlayerLevel, _currentGoldAmount;

        private string _totalScore;

        public EnviornmentComponent(Game game) : base(game)
        {
            _totalScore = "0";
            DrawOrder = 1;
            UpdatableStates = GameState.Playing;
            DrawableStates = GameState.Playing | GameState.Paused;
        }

        protected override void LoadContent()
        {
            _font = Game.Content.Load<SpriteFont>("Font");
            _backgroundTexture2D = Game.Content.Load<Texture2D>("Background");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _totalScore = Points.Score.ToString();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(_backgroundTexture2D, GraphicsDevice.Viewport.Bounds, Color.White);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
