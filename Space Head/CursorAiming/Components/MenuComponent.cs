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
    class MenuComponent : SpaceHeadBaseComponent
    {
        private Texture2D _survivalModeButton, _exitGameButton;
        

        private Vector2 _survivalModeButtonPos = new Vector2(10, 10);
        private Vector2 _exitGameButtonPos = new Vector2(10, 70);

        private Rectangle _survivalModeButtonArea = new Rectangle();

        private MouseState _mouseState;
        private MouseState _previousMouseState;

        private int _mouseX, _mouseY;

        

        public MenuComponent(Game game) : base(game)
        {
            DrawOrder = 0;
            UpdatableStates = GameState.MainMenu;
            DrawableStates = GameState.MainMenu;
        }

        protected override void LoadContent()
        {
            _survivalModeButton = Game.Content.Load<Texture2D>("survivalModeIcon");
            _exitGameButton = Game.Content.Load<Texture2D>("exitGameIcon");
            base.LoadContent();
        }
        
        public override void Update(GameTime gameTime)
        {
            CheckPlayerInput(GameState.MainMenu);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            
            SpriteBatch.Draw(_survivalModeButton, _survivalModeButtonPos, Color.AliceBlue);
            SpriteBatch.Draw(_exitGameButton, _exitGameButtonPos, Color.AliceBlue);

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        public void CheckPlayerInput(GameState gameState)
        {
            _mouseState = Mouse.GetState();

            _mouseX = _mouseState.X;
            _mouseY = _mouseState.Y;

            if (gameState == GameState.MainMenu)
            {
                if (_mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton != ButtonState.Pressed)
                {
                    if (new Rectangle((int) _survivalModeButtonPos.X, (int) _survivalModeButtonArea.Y, _survivalModeButton.Width, _survivalModeButton.Height).Contains(_mouseX, _mouseY))
                    {
                        SpaceHeadGame.ChangeCurrentGameState(GameState.Playing);
                    }

                    if (new Rectangle((int)_exitGameButtonPos.X, (int)_exitGameButtonPos.Y, _exitGameButton.Width, _exitGameButton.Height).Contains(_mouseX, _mouseY))
                    {
                        Game.Exit();
                    }
                }
            }
        }


    }
}
