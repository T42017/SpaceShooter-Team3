using System;
using System.Collections.Generic;
using CursorAiming.Enemies;
using Microsoft.Xna.Framework;

namespace CursorAiming
{
    internal class Wave : SpaceHeadBaseComponent
    {
        public static int _waveIndex;

        public static List<Enemy> EnemiesOnField = new List<Enemy>();

        private static int _numberOfEnemiesToSpawn = _waveIndex * 2 + 1;
        private static int _enemiesSpawned;
        private static bool _isSpawning;

        private static double _timeBetweenWaves, _timeLeftBetweenWaves;
        private static double _timeBetweenSpawns;
        private static double _timeLeftBetweenSpawns;


        public Wave(Game game) : base(game)
        {
            Game.Components.Add(this);
            UpdatableStates = GameState.Playing;
            _timeBetweenWaves = 4;
            _timeBetweenSpawns = 1;
            _timeLeftBetweenWaves = _timeBetweenWaves;
        }

        private void SpawnMeleeEnemy()
        {
            var rand = new Random();
            if (_waveIndex < 5)
                EnemiesOnField.Add(new MeleeEnemy(200, 2, "MeleeEnemy1", 20, 20, 20, Game)
                {
                    Position = new Vector2(rand.Next(300, 1200), rand.Next(300, 800))
                });
            else if (_waveIndex < 10)
                EnemiesOnField.Add(new MeleeEnemy(200, 6, "MeleeEnemy1", 20, 20, 20, Game)
                {
                    Position = new Vector2(rand.Next(300, 1200), rand.Next(300, 800))
                });
            _enemiesSpawned++;
        }

        private void SpawnGunner()
        {
            var rand = new Random();
            EnemiesOnField.Add(new EnemyWithGun(new Gun("playerGun1", "laserBlue01", 1, 400, UnitType.Player, Game),
                200, 8, 2, "Enemy1", 40, 40, 40, Game)
            {
                Position = new Vector2(rand.Next(300, 1200), rand.Next(300, 800))
            });
            _enemiesSpawned++;
        }

        private void SpawnWave(GameTime gameTime)
        {
            if (_timeLeftBetweenSpawns <= 0)
            {
                _numberOfEnemiesToSpawn = _waveIndex * 2 + 1;

                if (_waveIndex < 2)
                    SpawnMeleeEnemy();
                else if (_waveIndex < 10)
                    SpawnGunner();

                _timeLeftBetweenSpawns = _timeBetweenSpawns;
            }
            else
            {
                _timeLeftBetweenSpawns -= gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (_enemiesSpawned < _numberOfEnemiesToSpawn && _isSpawning)
                SpawnWave(gameTime);
            else if (_enemiesSpawned == _numberOfEnemiesToSpawn) _isSpawning = false;

            if (EnemiesOnField.Count == 0 && !_isSpawning)
                if (_timeLeftBetweenWaves <= 0)
                {
                    _isSpawning = true;
                    _waveIndex++;
                    _enemiesSpawned = 0;
                    _timeLeftBetweenWaves = _timeBetweenWaves;
                }
                else
                {
                    _timeLeftBetweenWaves -= gameTime.ElapsedGameTime.TotalSeconds;
                }

            base.Update(gameTime);
        }

        public static void Reset()
        {
            _waveIndex = 0;
            _timeLeftBetweenSpawns = _timeBetweenSpawns;
            _timeLeftBetweenWaves = _timeBetweenWaves;
            _isSpawning = false;
            _numberOfEnemiesToSpawn = _waveIndex * 2 + 1;
            for (var i = 0; i < EnemiesOnField.Count; i++)
            {
                EnemiesOnField[i].Remove();
                i--;
            }
            EnemiesOnField.Clear();
        }
    }
}