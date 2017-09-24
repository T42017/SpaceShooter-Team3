﻿using System;
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
            DrawOrder = 3;
            DrawableStates = GameState.Playing | GameState.Paused;
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

            SpriteBatch.DrawString(_font, "MS: " + Player.MoveSpeed, new Vector2(Globals.ScreenWidth * 0.01f, Globals.ScreenHeight * 0.65f), Color.Green);

            SpriteBatch.DrawString(_font, "DMG: " + Player.Gun.Damage, new Vector2(Globals.ScreenWidth * 0.01f, Globals.ScreenHeight * 0.7f), Color.Green);
            
            SpriteBatch.DrawString(_font, "AS: " + Math.Round(1/Player._attackSpeed, 2), new Vector2(Globals.ScreenWidth * 0.01f, Globals.ScreenHeight * 0.75f), Color.Green);

            SpriteBatch.DrawString(_font, "GOLD: " + Player.Coins, new Vector2(Globals.ScreenWidth * 0.01f, Globals.ScreenHeight * 0.85f), Color.Green  );

            SpriteBatch.DrawString(_font, "SKILL POINTS: " + Player.PlayerSkillPoints, new Vector2(Globals.ScreenWidth * 0.01f, Globals.ScreenHeight * 0.9f), Color.Green);

            SpriteBatch.DrawString(_font, "SCORE: " + Player.Points, new Vector2(Globals.ScreenWidth * 0.01f, Globals.ScreenHeight * 0.95f), Color.Green);

            SpriteBatch.DrawString(_font, "Wave: " + Wave.WaveIndex, new Vector2(Globals.ScreenWidth * 0.45f, Globals.ScreenHeight * 0.01f), Color.Green);
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
