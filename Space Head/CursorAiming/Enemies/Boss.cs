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
        private readonly double _timeBetweenSpawns, _timeBetweenShot1, _timeBetweenShot2;
        private double _timeTilSpawn, _timeTilShot1, _timeTilShot2;
        int _shot2Index;

        Texture2D _shot1Txt, _shot2Txt;

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
            _timeBetweenSpawns = 2;
            _timeTilSpawn = _timeBetweenSpawns;
            _timeBetweenShot1 = 2;
            _timeTilShot1 = _timeBetweenShot1;
            _timeBetweenShot2 = .1;
            _timeTilShot2 = _timeBetweenShot2;

        }

        protected override void LoadContent()
        {
            UnitTexture = Game.Content.Load<Texture2D>(TexturePath);
            _shot1Txt = Game.Content.Load<Texture2D>("BossShot1");
            _shot2Txt = Game.Content.Load<Texture2D>("BossShot2");
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
                Wave.SpawnBossSpawn(Game, Position);
                _timeTilSpawn = _timeBetweenSpawns;
            }

            if (_timeTilShot1 > 0)
            {
                _timeTilShot1 -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Shoot1();
                _timeTilShot1 = _timeBetweenShot1;
            }

            if (_timeTilShot2 > 0)
            {
                _timeTilShot2 -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Shoot2(ref _shot2Index);
                _timeTilShot2 = _timeBetweenShot2;
            }

            
            base.Update(gameTime);
        }

        void Shoot1()
        {
            Random rand = new Random();
            int directionIndex = rand.Next(0,4);

            if(directionIndex == 0)
                for (int i = 0; i < 10; i++)
                {
                    Vector2 direction = new Vector2(-i, 10 - i);
                    direction.Normalize();
                    EnviornmentComponent.BulletsInAir.Add(new Bullet(200, 1, direction, Position, (float)Math.Atan2(direction.Y, direction.X), _shot1Txt, UnitType.Player));

                }

            else if(directionIndex == 1)
                for (int i = 0; i < 10; i++)
                {
                    Vector2 direction = new Vector2(i, -10 + i);
                    direction.Normalize();
                    EnviornmentComponent.BulletsInAir.Add(new Bullet(200, 1, direction, Position, (float)Math.Atan2(direction.Y, direction.X), _shot1Txt, UnitType.Player));

                }

            else if (directionIndex == 2)
                for (int i = 0; i < 10; i++)
                {
                    Vector2 direction = new Vector2(-10 +i, -i);
                    direction.Normalize();
                    EnviornmentComponent.BulletsInAir.Add(new Bullet(200, 1, direction, Position, (float)Math.Atan2(direction.Y, direction.X), _shot1Txt, UnitType.Player));

                }

            else if (directionIndex == 3)

                for (int i = 0; i < 10; i++)
                {
                    Vector2 direction = new Vector2(10 - i, i);
                    direction.Normalize();
                    EnviornmentComponent.BulletsInAir.Add(new Bullet(200, 1, direction, Position, (float)Math.Atan2(direction.Y, direction.X), _shot1Txt, UnitType.Player));

                }

        }

        void Shoot2(ref int index)
        {
            if (index < 30)
            {
                Vector2 direction = new Vector2(-index % 30, 30 - index % 30);
                direction.Normalize();
                EnviornmentComponent.BulletsInAir.Add(new Bullet(200, 1, direction, Position, (float)Math.Atan2(direction.Y, direction.X), _shot2Txt, UnitType.Player));
            }
            
            
            else if (index < 60)
            {
                Vector2 direction = new Vector2(-30 + index % 30, -index % 30);
                direction.Normalize();
                EnviornmentComponent.BulletsInAir.Add(new Bullet(200, 1, direction, Position, (float)Math.Atan2(direction.Y, direction.X), _shot2Txt, UnitType.Player));
            }
            else if (index < 90)
            {
                Vector2 direction = new Vector2(index % 30, -30 + index % 30);
                direction.Normalize();
                EnviornmentComponent.BulletsInAir.Add(new Bullet(200, 1, direction, Position, (float)Math.Atan2(direction.Y, direction.X), _shot2Txt, UnitType.Player));
            }
            else if (index < 120)
            {
                Vector2 direction = new Vector2(30 - index % 30, index % 30);
                direction.Normalize();
                EnviornmentComponent.BulletsInAir.Add(new Bullet(200, 1, direction, Position, (float)Math.Atan2(direction.Y, direction.X), _shot2Txt, UnitType.Player));
            }
            else index = -1;
            
            index++;
        }

        public override void Remove()
        {
            Wave.EnemiesOnField.Remove(this);
            base.Remove();
        }
    }
}
