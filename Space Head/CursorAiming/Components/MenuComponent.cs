using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    class MenuComponent : SpaceHeadBaseComponent
    {
        SpriteBatch _spriteBatch;
        SpriteFont _normalFont;
        List<MenuChoice> _choices;
        MouseState _previousMouseState;
        Waves _wave;
        
        public MenuComponent(Game game)
            : base(game)
        {
            DrawOrder = 44;
            DrawableStates = GameState.MainMenu;
            UpdatableStates = GameState.MainMenu;
        }

        public override void Initialize()
        {
            _wave = new Waves(Game);
            _choices = new List<MenuChoice>();
            _choices.Add(new MenuChoice() { Text = "START", Selected = true, ClickAction = MenuStartClicked });
            _choices.Add(new MenuChoice() { Text = "QUIT", ClickAction = MenuQuitClicked });

            base.Initialize();
        }

        #region Menu Clicks

        private void MenuStartClicked()
        {
            SpaceHeadGame.ChangeCurrentGameState(GameState.Playing);
            _wave.SetTimer(2000);
        }

        private void MenuQuitClicked()
        {
            Game.Exit();
        }

        #endregion

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _normalFont = Game.Content.Load<SpriteFont>("Font");

            float startY = 0.2f * Globals.ScreenHeight;

            foreach (var choice in _choices)
            {
                Vector2 size = _normalFont.MeasureString(choice.Text);
                choice.Y = startY;
                choice.X = Globals.ScreenWidth / 2.0f - size.X / 2;
                choice.HitBox = new Rectangle((int)choice.X, (int)choice.Y, (int)size.X, (int)size.Y);
                startY += 70;
            }

            _previousMouseState = Mouse.GetState();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            //... Komplettering #3
            foreach (var choice in _choices)
            {
                if (choice.HitBox.Contains(mouseState.X, mouseState.Y))
                {
                    _choices.ForEach(c => c.Selected = false);
                    choice.Selected = true;

                    if (_previousMouseState.LeftButton == ButtonState.Released
                        && mouseState.LeftButton == ButtonState.Pressed)
                        choice.ClickAction.Invoke();
                }
            }

            _previousMouseState = mouseState;

            base.Update(gameTime);
        }
        
        // ... Komplettering #4   
        private void PreviousMenuChoice()
        {
            int selectedIndex = _choices.IndexOf(_choices.First(c => c.Selected));
            _choices[selectedIndex].Selected = false;
            selectedIndex--;
            if (selectedIndex < 0)
                selectedIndex = _choices.Count - 1;
            _choices[selectedIndex].Selected = true;
        }

        private void NextMenuChoice()
        {
            int selectedIndex = _choices.IndexOf(_choices.First(c => c.Selected));
            _choices[selectedIndex].Selected = false;
            selectedIndex++;
            if (selectedIndex >= _choices.Count)
                selectedIndex = 0;
            _choices[selectedIndex].Selected = true;
        }
        // ... Komplettering #4


        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            foreach (var choice in _choices)
            {
                _spriteBatch.DrawString(_normalFont, choice.Text, new Vector2(choice.X, choice.Y), Color.White);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
