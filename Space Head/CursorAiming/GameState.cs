using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    public enum GameState
    {
        MainMenu,
        GameIsRunning,
        GameIsPaused,
        GameOver,
        Exit,
    }

    public class States : DrawableGameComponent
    {
        private Texture2D start;
        private Texture2D exit;
        

        private Vector2 _startButtonPos = new Vector2(10, 10);
        private Vector2 _exitButtonPos = new Vector2(10, 50);

        private MouseState _mouseState;
        
        private MouseState _previousMouseState;

        private int _mouseX;
        private int _mouseY;

        public States(Game game) : base(game)
        {
            _mouseState = new MouseState();
            _mouseState = Mouse.GetState();

            _previousMouseState = new MouseState();
            _previousMouseState = Mouse.GetState();

            _mouseX = _mouseState.X;
            _mouseY = _mouseState.Y;

            start = Game.Content.Load<Texture2D>("start");
            exit = Game.Content.Load<Texture2D>("exit");
        }

        protected override void LoadContent()
        {
            

            base.LoadContent();
        }

        public void CheckPlayerInput(GameState gameState)
        {
            _mouseState = Mouse.GetState();
            _previousMouseState = Mouse.GetState();

            _mouseX = _mouseState.X;
            _mouseY = _mouseState.Y;

            if (gameState == GameState.MainMenu)
            {
                if (_mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Pressed)
                {
                    Game.Exit();
                    if (new Rectangle((int)_startButtonPos.X, (int)_startButtonPos.Y, start.Width, start.Height).Contains(
                            _mouseX, _mouseY))
                    {
                        Game.Exit();
                    }
                }
            }

        }

        public void UpdateGraphics(SpriteBatch spriteBatch, GameState gameState)
        {
            if (gameState == GameState.MainMenu)
            {
                spriteBatch.Draw(start, _startButtonPos, Color.White);
            }
        }
    }
}
