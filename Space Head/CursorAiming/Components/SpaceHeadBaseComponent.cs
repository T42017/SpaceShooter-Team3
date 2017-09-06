using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming
{
    class SpaceHeadBaseComponent : DrawableGameComponent
    {
        public SpriteBatch SpriteBatch { get; private set; }
        public SpaceHeadGame SpaceHeadGame { get; private set; }

        public GameState DrawableStates { get; protected set; }
        public GameState UpdatableStates { get; protected set; }

        public SpaceHeadBaseComponent(Game game) : base(game)
        {
            SpaceHeadGame = (SpaceHeadGame)game;
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            base.LoadContent();
        }
    }
}
