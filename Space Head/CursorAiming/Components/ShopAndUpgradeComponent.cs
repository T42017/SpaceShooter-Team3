using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    class ShopAndUpgradeComponent : SpaceHeadBaseComponent
    {
        private int _currentPlayerLevel, _currentGoldAmount;
        private SpriteFont _font;

        public ShopAndUpgradeComponent(Game game) : base(game)
        {
            _currentPlayerLevel = 5;
            _currentGoldAmount = 5123;

            DrawOrder = 3;
            DrawableStates = GameState.ShopUpgradeMenu;
            UpdatableStates = GameState.ShopUpgradeMenu;
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

            SpriteBatch.DrawString(_font, "Gold:" + _currentGoldAmount, new Vector2(0, 50), Color.Khaki);
            SpriteBatch.DrawString(_font, "Level:" + _currentPlayerLevel, new Vector2(0, 100), Color.Khaki);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
