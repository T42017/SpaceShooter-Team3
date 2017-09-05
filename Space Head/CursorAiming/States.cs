using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    public enum GameState
    {
        MainMenu,
        GameIsPaused,
        GameIsRunning,
        GameOver,
        Exit
    }

    internal class States : DrawableGameComponent
    {
        private Vector2 _exitBuyyonPos = new Vector2(10, 50);

        private MouseState _mouseState;

        private int _mouseX, _mouseY;
        private MouseState _previousMouseState;
        private Texture2D _start, _exit;

        private Vector2 _startButtonPos = new Vector2(10, 10);

        public States(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            _start = Game.Content.Load<Texture2D>("start");
            _exit = Game.Content.Load<Texture2D>("exit");
            base.LoadContent();
        }

        public void CheckPlayerInput(GameState gameState)
        {
            _mouseState = Mouse.GetState();
            //_previousMouseState = Mouse.GetState();

            _mouseX = _mouseState.X;
            _mouseY = _mouseState.Y;

            if (gameState == GameState.MainMenu)
                if (_mouseState.LeftButton == ButtonState.Pressed &&
                    _previousMouseState.LeftButton != ButtonState.Pressed)
                    if (new Rectangle((int) _startButtonPos.X, (int) _startButtonPos.Y, _start.Width, _start.Height)
                        .Contains(_mouseX, _mouseY))
                        Game.Exit();
            _previousMouseState = _mouseState;
        }
    }
}