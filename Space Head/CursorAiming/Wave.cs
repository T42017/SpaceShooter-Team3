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
                EnemiesOnField.Add(new MeleeEnemy(200, 6, "MeleeEnemy1", 40, 20, 20, Game)
                {
                    Position = GetSpawnLocation()
                });
            else if (_waveIndex < 10)
                EnemiesOnField.Add(new MeleeEnemy(200, 10, "MeleeEnemy1", 80, 50, 50, Game)
                {
                    Position = GetSpawnLocation()
                });
            _enemiesSpawned++;
        }

        private void SpawnGunner()
        {
            var rand = new Random();
            EnemiesOnField.Add(new EnemyWithGun(new Gun("EnemyGun1", "EnemyShot", 1, 400, UnitType.Player, Game),
                200, 20, 2, "Enemy1", 40, 40, 40, Game)
            {
                Position = GetSpawnLocation()
            });
            _enemiesSpawned++;
        }

        private void SpawnWave(GameTime gameTime)
        {
            if (_timeLeftBetweenSpawns <= 0)
            {
                _numberOfEnemiesToSpawn = _waveIndex * 2 + 2;

                if (_waveIndex < 1)
                    SpawnMeleeEnemy();
                else if (_waveIndex < 1)
                    SpawnGunner();
                else if (_waveIndex < 10)
                {
                    SpawnGunner();

                    SpawnMeleeEnemy();                    
                }

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

        public Vector2 GetSpawnLocation()
        {
            var rand = new Random();
            var playerZone = new Rectangle(Player.PlayerPosition.ToPoint() - new Point(100, 100), new Point(200, 200));

            var spawn = new Vector2(rand.Next(300, Globals.ScreenWidth - 285),
                rand.Next(150, Globals.ScreenHeight - 145));
            for (var i = 0; i < EnviornmentComponent.ObstaclesOnField.Count; i++)
                if (EnviornmentComponent.ObstaclesOnField[i].Contains(spawn) || playerZone.Contains(spawn))
                {
                    spawn = new Vector2(rand.Next(300, Globals.ScreenWidth - 285),
                        rand.Next(150, Globals.ScreenHeight - 145));
                    i = 0;
                }

            return spawn;
        }
    }
}