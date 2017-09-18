using System.Collections.Generic;
using CursorAiming.Enemies;
using Microsoft.Xna.Framework;

namespace CursorAiming
{
    internal class Wave : SpaceHeadBaseComponent
    {
        private static int _waveIndex;

        public static List<Enemy> EnemiesOnField = new List<Enemy>();
        private readonly double _timeBetweenSpawns;
        private readonly int numberOfEnemies = _waveIndex * 2 + 1;
        private bool _isSpawning;

        private double _timeBetweenWaves, _timeLeftBetweenWaves;
        private double _timeLeftBetweenSpawns;


        public Wave(Game game) : base(game)
        {
            Game.Components.Add(this);
            UpdatableStates = GameState.Playing;
            _timeBetweenWaves = 2;
            _timeBetweenSpawns = .7;
        }

        private void SpawnEnemy()
        {
            EnemiesOnField.Add(new MeleeEnemy(300, 1, "spaceAstronauts_red", 20, 20, 20, Game));
            EnemiesOnField.Add(new EnemyWithGun(new Gun("PlayerGun1", "laserBlue01", 1, 500, UnitType.Player, Game),
                100, 100, 1d, "BasicEnemy", 100, 100, 100, Game));
        }

        private void SpawnWave(GameTime gameTime)
        {
            if (_timeLeftBetweenSpawns <= 0)
            {
                SpawnEnemy();
                _timeLeftBetweenSpawns = _timeBetweenSpawns;
            }
            else
            {
                _timeLeftBetweenSpawns -= gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (EnemiesOnField.Count < numberOfEnemies)
                SpawnWave(gameTime);
            base.Update(gameTime);
        }
    }
}