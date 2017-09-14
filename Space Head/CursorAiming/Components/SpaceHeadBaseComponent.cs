using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    public class SpaceHeadBaseComponent : DrawableGameComponent
    {
        public SpaceHeadBaseComponent(Game game) : base(game)
        {
            SpaceHeadGame = (SpaceHeadGame) game;
        }

        public SpriteBatch SpriteBatch { get; private set; }
        public SpaceHeadGame SpaceHeadGame { get; }

        public GameState DrawableStates { get; protected set; }
        public GameState UpdatableStates { get; protected set; }

        public virtual void Remove()
        {
            Game.Components.Remove(this);
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            base.LoadContent();
        }
    }
}