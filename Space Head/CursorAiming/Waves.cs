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
        //private readonly Thread waveThread;
        private int _enemyCount;
        private Random _rng = new Random();
        private double _tempTimeLeft;

        public Waves(Game game) : base(game)
        {
            //waveThread = new Thread(SpawnEnemy);
            //waveThread.Start();

            Game.Components.Add(this);
            UpdatableStates = GameState.Playing;
            _tempTimeLeft = _tempTimer;
        }

        public void SpawnEnemy()
        {
            _rng = new Random();
            var newEnemyPosition = Vector2.Zero;
            newEnemyPosition.X = _rng.Next(0, 1280);
            newEnemyPosition.Y = _rng.Next(0, 1280);

            var spawnedEnemies = new EnemyWithGun(new Gun("PlayerGun1", "laserBlue01", 1, 700, UnitType.Player, Game),
                200, 2, 1d, "BasicEnemy", 100, 100, 100, Game) {Position = newEnemyPosition};

            EnemyUnitsOnField.Add(spawnedEnemies);
        }


        public override void Update(GameTime gameTime)
        {
            if (_enemyCount < 5)
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