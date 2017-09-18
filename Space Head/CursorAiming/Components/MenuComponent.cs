using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CursorAiming
{
    class MenuComponent : SpaceHeadBaseComponent
    {
        SpriteFont _font;
        List<MenuChoice> _choices;
        MouseState _previousMouseState;
        private string _title;
        
        public MenuComponent(Game game)
            : base(game)
        {
            DrawOrder = 44;
            DrawableStates = GameState.MainMenu;
            UpdatableStates = GameState.MainMenu;
        }

        public override void Initialize()
        {
            _choices = new List<MenuChoice>
            {
                new MenuChoice {Text = "START", Selected = true, ClickAction = MenuStartClicked},
                new MenuChoice {Text = "QUIT", ClickAction = MenuQuitClicked}
            };

            _title = "SPACE HEAD";

            base.Initialize();
        }

        #region Menu Clicks

        private void MenuStartClicked()
        {
            SpaceHeadGame.ChangeCurrentGameState(GameState.Playing);
           
        }

        private void MenuQuitClicked()
        {
            Game.Exit();
        }

        #endregion

        protected override void LoadContent()
        {
            _font = Game.Content.Load<SpriteFont>("Font");

            float startY = 0.6f * Globals.ScreenHeight;

            foreach (var choice in _choices)
            {
                Vector2 size = _font.MeasureString(choice.Text);
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
            SpriteBatch.Begin();

            SpriteBatch.DrawString(_font, _title, new Vector2(Globals.ScreenWidth/2 - _font.MeasureString(_title).X/2 , Globals.ScreenHeight * 0.2f), Color.Green);

            foreach (var choice in _choices)
            {
                SpriteBatch.DrawString(_font, choice.Text, new Vector2(choice.X, choice.Y), Color.Green);
            }

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
