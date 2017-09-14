using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Microsoft.Xna.Framework;

namespace CursorAiming
{
    public class Waves : SpaceHeadBaseComponent
    {
        public static List<Enemy> EnemyUnitsOnField = new List<Enemy>();
        public static int EnemyCount;
        private static readonly Random _rng = new Random();
        private int _numberOfEnemiesToSpawn;
        public static int WaveRound = 1;
        private int _enemiesCantSpawnSquared;


        public enum EnemyLevels
        {
            Easy,
            Medium,
            Hard
        }

        
        public Waves(Game game) : base(game)
        {
            Game.Components.Add(this);
            EnemyUnitsOnField.Clear();
        }


        public void SpawnEnemy()
        {
            var newEnemyPosition = Vector2.Zero;
            newEnemyPosition.X = _rng.Next(0, Globals.ScreenWidth);
            newEnemyPosition.Y = _rng.Next(0, Globals.ScreenHeight);

            new EnemyWithGun(new Gun("PlayerGun1", "laserBlue01", 1, 700, UnitType.Player, Game),
                200, 2, 1d, "BasicEnemy", 100, 100, 100, Game) {Position = newEnemyPosition};
        }

        public void SetTimer(int intervalInMilli)
        {
            var aTimer = new System.Timers.Timer(intervalInMilli);
            if (SpaceHeadGame.Instance.GameState != GameState.Playing)
                aTimer.Enabled = false;
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            _numberOfEnemiesToSpawn = WaveRound * 2 + 1;

            while (EnemyCount < _numberOfEnemiesToSpawn)
            {
                SpawnEnemy();
                EnemyCount++;
            }
        }

        public override void Update(GameTime gameTime)
        {
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