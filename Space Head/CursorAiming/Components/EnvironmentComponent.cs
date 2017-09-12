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
        private Texture2D _room1;
        private Texture2D _room2;
        private Texture2D _room3;
        private Texture2D _room4;


        private int _currentPlayerLevel, _currentGoldAmount;

        private string _totalScore;

        public EnviornmentComponent(Game game) : base(game)
        {
            _totalScore = "0";
            DrawOrder = 1;
            UpdatableStates = GameState.Playing;
            DrawableStates = GameState.Playing | GameState.Paused | GameState.ShopUpgradeMenu;
        }

        protected override void LoadContent()
        {
            _font = Game.Content.Load<SpriteFont>("Font");
            _backgroundTexture2D = Game.Content.Load<Texture2D>("Background");
            _room1 = Game.Content.Load<Texture2D>("rum1");
            _room2 = Game.Content.Load<Texture2D>("rum2");
            _room3 = Game.Content.Load<Texture2D>("rum3");
            _room4 = Game.Content.Load<Texture2D>("rum4");
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
            SpriteBatch.Draw(_room1, GraphicsDevice.Viewport.Bounds, Color.White);
            SpriteBatch.Draw(_room2, GraphicsDevice.Viewport.Bounds, Color.White);
            SpriteBatch.Draw(_room3, GraphicsDevice.Viewport.Bounds, Color.White);
            SpriteBatch.Draw(_room4, GraphicsDevice.Viewport.Bounds, Color.White);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
