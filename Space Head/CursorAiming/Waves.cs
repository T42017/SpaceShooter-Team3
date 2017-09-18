using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework;

namespace CursorAiming
{
    public class Waves : SpaceHeadBaseComponent
    {
        public enum EnemyLevels
        {
            Easy,
            Medium,
            Hard
        }

        public static List<Enemy> EnemyUnitsOnField = new List<Enemy>();
        public static int _enemyCount;
        private static readonly Random _rng = new Random();
        public static int _waveRound = 1;
        private readonly double _tempTimer = 1d;
        private int _numberOfEnemiesToSpawn;
        private double _tempTimeLeft;

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

            var spawnedEnemy = new EnemyWithGun(new Gun("PlayerGun1", "laserBlue01", 1, 700, UnitType.Player, Game),
                100, 100, 1d, "BasicEnemy", 100, 100, 100, Game) {Position = newEnemyPosition};

            EnemyUnitsOnField.Add(spawnedEnemy);
        }

        public void SetTimer()
        {
            var aTimer = new Timer(2000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            _numberOfEnemiesToSpawn = _waveRound * 2 + 1;

            while (_enemyCount < _numberOfEnemiesToSpawn)
            {
                SpawnEnemy();
                _enemyCount++;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public static void Reset(Game game)
        {
            foreach (var enemy in EnemyUnitsOnField)
                enemy.Remove();

            EnemyUnitsOnField.Clear();
            _enemyCount = 0;
            _waveRound = 1;
        }
    }
}