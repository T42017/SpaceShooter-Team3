using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    class UIComponent : SpaceHeadBaseComponent
    {
        private SpriteFont _font;


        public UIComponent(Game game) : base(game)
        {
            
            DrawOrder = 2;
            
        }

        protected override void LoadContent()
        {
            _font = Game.Content.Load<SpriteFont>("Font");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
