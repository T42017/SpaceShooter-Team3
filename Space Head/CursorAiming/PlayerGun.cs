using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    public class PlayerGun : Gun
    {
        private KeyboardState _previousKeyboardState;

        private KeyboardState kbState;

        private SpriteFont _font;

        public PlayerGun(string gunTexturePath, string bulletTexturePath, int damage, int shotSpeed, UnitType typeToHit,
            Game game) : base(gunTexturePath, bulletTexturePath, damage, shotSpeed, typeToHit, game)
        {
            UpdatableStates = GameState.ShopUpgradeMenu;
            DrawableStates = GameState.Playing | GameState.Paused;
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
