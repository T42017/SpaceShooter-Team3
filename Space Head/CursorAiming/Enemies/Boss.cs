using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursorAiming.Enemies
{
    class Boss : Enemy
    {
        private readonly double _timeBetweenSpawns;
        private double _timeTilSpawn;

        public Boss(int moveSpeed, int health, string texturePath, int pointValue,
            int xpValue, int coinValue, Game game) : base(game)
        {

            MoveSpeed = moveSpeed;
            Health = health;
            TexturePath = texturePath;
            PointValue = pointValue;
            XpValue = xpValue;
            CoinValue = coinValue;
            DrawOrder = 2;
            Game.Components.Add(this);
            _timeBetweenSpawns = 6;
            _timeTilSpawn = _timeBetweenSpawns;

        }

        protected override void LoadContent()
        {
            UnitTexture = Game.Content.Load<Texture2D>(TexturePath);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (_timeTilSpawn > 0)
            {
                _timeTilSpawn -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Wave.SpawnShipSpawn(Game, Position);
                _timeTilSpawn = _timeBetweenSpawns;
            }



            base.Update(gameTime);
        }


    }
}
