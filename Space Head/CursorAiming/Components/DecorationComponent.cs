using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    class DecorationComponent : SpaceHeadBaseComponent
    {
        private Texture2D _backgroundTexture2D;

        public DecorationComponent(Game game) : base(game)
        {
            DrawOrder = 1;
            UpdatableStates = GameState.Playing;
            DrawableStates = GameState.Playing | GameState.Paused;
        }

        protected override void LoadContent()
        {
            _backgroundTexture2D = Game.Content.Load<Texture2D>("Background");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(_backgroundTexture2D, GraphicsDevice.Viewport.Bounds, Color.AliceBlue);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
