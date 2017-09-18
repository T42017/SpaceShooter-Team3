using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Timer = System.Timers.Timer;

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
        private int _numberOfEnemiesToSpawn;

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
                200, 10, 1d, "BasicEnemy", 100, 100, 100, Game) {Position = newEnemyPosition};

           //EnemyUnitsOnField.Add(spawnedEnemy);
        }

        public void SetTimer(GameState gameState)
        {
            SpaceHeadGame.ChangeCurrentGameState(gameState);
            var aTimer = new Timer(3000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;
        }

        public void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            _numberOfEnemiesToSpawn = _waveRound * 2 + 1;

            while (_enemyCount < _numberOfEnemiesToSpawn && GameState.Playing == SpaceHeadGame.GameState)
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