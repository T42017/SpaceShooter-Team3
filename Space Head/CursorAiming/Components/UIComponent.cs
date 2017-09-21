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
            DrawableStates = GameState.Playing | GameState.Paused | GameState.ShopUpgradeMenu;
            UpdatableStates = GameState.Playing | GameState.ShopUpgradeMenu;
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

            SpriteBatch.DrawString(_font, "Gold: " + Player.Coins, new Vector2(Globals.ScreenWidth * 0.01f, Globals.ScreenHeight * 0.95f), Color.Green  );

            SpriteBatch.DrawString(_font, "Skill Points: " + Player.PlayerSkillPoints, new Vector2(Globals.ScreenWidth * 0.01f, Globals.ScreenHeight * 0.9f), Color.Green);

            SpriteBatch.DrawString(_font, "Score: " + Player.Points, new Vector2(Globals.ScreenWidth * 0.01f, Globals.ScreenHeight * 0.85f), Color.Green);


            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
