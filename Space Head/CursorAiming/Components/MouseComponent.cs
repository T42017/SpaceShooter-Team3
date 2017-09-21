using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    internal class MouseComponent : SpaceHeadBaseComponent
    {
        private Texture2D _gameCursor;
        private Vector2 _gameCursorPos;
        private MouseState _mouseState;

        public MouseComponent(Game game) : base(game)
        {
            DrawOrder = 99;
            DrawableStates = GameState.All;
            UpdatableStates = GameState.All;
        }

        protected override void LoadContent()
        {
            _gameCursor = Game.Content.Load<Texture2D>("Cursor");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _mouseState = Mouse.GetState();

            _gameCursorPos = new Vector2(_mouseState.X, _mouseState.Y);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(_gameCursor, _gameCursorPos - new Vector2(_gameCursor.Width / 2, _gameCursor.Height / 2),
                Color.Green);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}