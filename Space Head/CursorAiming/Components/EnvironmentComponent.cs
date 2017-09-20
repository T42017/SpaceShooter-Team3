using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    internal class EnviornmentComponent : SpaceHeadBaseComponent
    {
        public static List<Rectangle> Borders = new List<Rectangle>();
        public static List<Rectangle> ObstaclesOnField = new List<Rectangle>();
        private Texture2D _backgroundTexture2D, _spaceBackground, _bigRock, _mediumRock, _mediumSmallRock, _smallRock;

        private int _currentPlayerLevel, _currentGoldAmount;
        private SpriteFont _font;

        private string _totalScore;

        public EnviornmentComponent(Game game) : base(game)
        {
            _totalScore = "0";
            DrawOrder = 0;
            UpdatableStates = GameState.Playing;
            DrawableStates = GameState.Playing | GameState.Paused;

            Borders.Add(new Rectangle(0, Globals.ScreenHeight - 145, Globals.ScreenWidth,
                100));
            Borders.Add(new Rectangle(Globals.ScreenWidth - 285, 0, 100,
                Globals.ScreenHeight));
            Borders.Add(new Rectangle(0, 0, Globals.ScreenWidth,
                150));

            Borders.Add(new Rectangle(0, 0, 300,
                Globals.ScreenHeight));
        }

        protected override void LoadContent()
        {
            _font = Game.Content.Load<SpriteFont>("Font");
            _backgroundTexture2D = Game.Content.Load<Texture2D>("AsteroidPlayingField");
            _spaceBackground = Game.Content.Load<Texture2D>("SpaceBackground");
            _bigRock = Game.Content.Load<Texture2D>("BigRock");
            ObstaclesOnField.Add(new Rectangle(700, 450, _bigRock.Width - 10, _bigRock.Height - 20));
            _mediumRock = Game.Content.Load<Texture2D>("MediumRock");
            ObstaclesOnField.Add(new Rectangle(500, 900, _mediumRock.Width, _mediumRock.Height));
            _mediumSmallRock = Game.Content.Load<Texture2D>("MediumSmallRock");
            ObstaclesOnField.Add(new Rectangle(350, 700, _mediumSmallRock.Width, _mediumSmallRock.Height));
            _smallRock = Game.Content.Load<Texture2D>("SmallRock");
            ObstaclesOnField.Add(new Rectangle(950, 300, _smallRock.Width, _smallRock.Height));
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

            SpriteBatch.Draw(_spaceBackground, GraphicsDevice.Viewport.Bounds, Color.White);

            SpriteBatch.Draw(_backgroundTexture2D, GraphicsDevice.Viewport.Bounds, Color.White);
            SpriteBatch.Draw(_bigRock, ObstaclesOnField[0], Color.White);
            SpriteBatch.Draw(_mediumRock, ObstaclesOnField[1], Color.White);
            SpriteBatch.Draw(_mediumSmallRock, ObstaclesOnField[2], Color.White);
            SpriteBatch.Draw(_smallRock, ObstaclesOnField[3], Color.White);
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}