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

        private string _currentLevel, _currentGoldAmount, _currentScore;

        public UIComponent(Game game) : base(game)
        {
            _currentLevel = "9000";
            _currentGoldAmount = "2";
            _currentScore = "1000000";
            
            DrawOrder = 2;
            //UpdatableStates = GameState.Playing | GameState.ShopUpgradeMenu;
            //DrawableStates = GameState.Playing | GameState.ShopUpgradeMenu | GameState.Paused;
        }

        protected override void LoadContent()
        {
            _font = Game.Content.Load<SpriteFont>("Font");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _currentScore = Points.Score.ToString();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            
            SpriteBatch.DrawString(_font, "Score: " + _currentScore, new Vector2(Globals.ScreenHeight * 0.01f, Globals.ScreenHeight - (Globals.ScreenHeight * 0.05f)), Color.MidnightBlue);
            SpriteBatch.DrawString(_font, "Gold: " + _currentGoldAmount, new Vector2(Globals.ScreenHeight * 0.01f, Globals.ScreenHeight - (Globals.ScreenHeight * 0.10f)), Color.MidnightBlue);
            SpriteBatch.DrawString(_font, "Level: " + _currentLevel, new Vector2(Globals.ScreenHeight * 0.01f, Globals.ScreenHeight - (Globals.ScreenHeight * 0.15f)), Color.MidnightBlue);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
