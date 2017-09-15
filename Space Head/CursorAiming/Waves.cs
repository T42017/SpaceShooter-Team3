using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;

namespace CursorAiming
{
    public class Waves : SpaceHeadBaseComponent
    {
        public static List<Enemy> EnemyUnitsOnField = new List<Enemy>();
        private readonly double _tempTimer = 1d;
        public static int _enemyCount;
        private Random _rng = new Random();
        private double _tempTimeLeft;
        private int _numberOfEnemiesToSpawn;
        public static int _waveRound = 1;

        public enum EnemyLevels
        {
            Easy,
            Medium,
            Hard
        }

        public Waves(Game game) : base(game)
        {
            Game.Components.Add(this);
            UpdatableStates = GameState.Playing;
            _tempTimeLeft = _tempTimer;
            EnemyUnitsOnField.Clear();
        }

        public void SpawnEnemy()
        {
            var newEnemyPosition = Vector2.Zero;
            newEnemyPosition.X = _rng.Next(0, Globals.ScreenWidth);
            newEnemyPosition.Y = _rng.Next(0, Globals.ScreenHeight);

            var spawnedEnemy = new EnemyWithGun(new Gun("PlayerGun1", "laserBlue01", 1, 700, UnitType.Player, Game),
                200, 2, 1d, "BasicEnem", 100, 100, 100, Game) {Position = newEnemyPosition};

            EnemyUnitsOnField.Add(spawnedEnemy);
        }

        public override void Update(GameTime gameTime)
        {
            _numberOfEnemiesToSpawn = _waveRound * 2 + 3;

            if (_enemyCount < _numberOfEnemiesToSpawn)
                if (_tempTimeLeft <= 0)
                {
                    SpawnEnemy();
                    _tempTimeLeft = _tempTimer;
                    _enemyCount++;                    
                }
                else
                {
                    _tempTimeLeft -= gameTime.ElapsedGameTime.TotalSeconds;
                }
            if (Player.Health == 0)
            {
                _enemyCount = 0;
                EnemyUnitsOnField.Clear();
            }
            

            base.Update(gameTime);
        }
    }


    public class EnemySpawn : Enemy
    {
        public EnemySpawn(Vector2 enemyPosistion, Vector2 enemyDirection, Game game) : base(game)
        {
            Position = enemyPosistion;
            AimDirection = enemyDirection;
        }
    }
}