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
        private Random rnd = new Random();

        private Texture2D _survivalModeButton, _exitGameButton;
        private Waves _wave;
        private Vector2 _survivalModeButtonPos = Vector2.Zero;
        private Vector2 _exitGameButtonPos = Vector2.Zero;

        private MouseState _mouseState;
        private MouseState _previousMouseState;

        private SpriteFont _font;

        private Vector2 _titleTextMeasure;
        private string _titleText = "Space Head";

        private int _mouseX, _mouseY;
        
        public MenuComponent(Game game) : base(game)
        {
            _wave = new Waves(Game);
            DrawOrder = 10;
            UpdatableStates = GameState.MainMenu;
            DrawableStates = GameState.MainMenu;
        }

        protected override void LoadContent()
        {
            _survivalModeButton = Game.Content.Load<Texture2D>("survivalModeIcon");
            _exitGameButton = Game.Content.Load<Texture2D>("exitGameIcon");
            _font = Game.Content.Load<SpriteFont>("Font");

            _titleTextMeasure = _font.MeasureString(_titleText);
            
            base.LoadContent();
        }
        
        public override void Update(GameTime gameTime)
        {
            CheckPlayerInput(GameState.MainMenu);
            
            _survivalModeButtonPos = new Vector2(Globals.ScreenWidth/2 - _survivalModeButton.Width/2, Globals.ScreenHeight * 0.75f);
            _exitGameButtonPos = new Vector2(Globals.ScreenWidth / 2 - _exitGameButton.Width / 2, Globals.ScreenHeight * 0.8f);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            
            SpriteBatch.DrawString(_font, "Space Head", new Vector2(Globals.ScreenWidth/2 - _titleTextMeasure.X/2, Globals.ScreenHeight * 0.3f), Color.Green);
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
                    if (new Rectangle((int) _survivalModeButtonPos.X, (int) _survivalModeButtonPos.Y, _survivalModeButton.Width, _survivalModeButton.Height).Contains(_mouseX, _mouseY))
                    {
                        SpaceHeadGame.ChangeCurrentGameState(GameState.Playing);
                        _wave.SetTimer();
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
