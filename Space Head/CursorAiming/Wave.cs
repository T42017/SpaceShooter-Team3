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

        private static int _numberOfEnemiesToSpawn;
        private static int _enemiesSpawned;

        private static bool _isSpawning;

        private static double _timeBetweenWaves, _timeLeftBetweenWaves;
        private static double _timeBetweenSpawns;
        private static double _timeLeftBetweenSpawns;
        private int meleesSpawned, gunnersSpawned;
        private int numberOfmelee, numberOfGunner;


        public Wave(Game game) : base(game)
        {
            Game.Components.Add(this);
            UpdatableStates = GameState.Playing;
            _timeBetweenWaves = 4;
            _timeBetweenSpawns = 1;
            meleesSpawned = 0;
            gunnersSpawned = 0;
            _enemiesSpawned = 0;
            _timeLeftBetweenWaves = _timeBetweenWaves;
        }

        private void SpawnMeleeEnemy()
        {
            if (_waveIndex < 5)
                EnemiesOnField.Add(new MeleeEnemy(100, 6, "MeleeEnemy1", 40, 20, 20, Game)
                {
                    Position = GetMeleeLocation()
                });
            else /*if (_waveIndex < 10)*/
                EnemiesOnField.Add(new MeleeEnemy(150, 13, "MeleeEnemy1", 80, 50, 50, Game)
                {
                    Position = GetMeleeLocation()
                });
            _enemiesSpawned++;
            meleesSpawned++;
        }

        private void SpawnGunner()
        {
            EnemiesOnField.Add(new EnemyWithGun(new Gun("EnemyGun1", "EnemyShot", 1, 400, UnitType.Player, Game),
                200, 20, 2, "Enemy1", 40, 40, 40, Game)
            {
                Position = GetGunnerLocation()
            });
            _enemiesSpawned++;
            gunnersSpawned++;
        }

        public override void Update(GameTime gameTime)
        {
            if (_enemiesSpawned < _numberOfEnemiesToSpawn && _isSpawning)
                if (_timeLeftBetweenSpawns <= 0)
                {
                    if (meleesSpawned < numberOfmelee)
                        SpawnMeleeEnemy();
                    if (gunnersSpawned < numberOfGunner)
                        SpawnGunner();

                    _timeLeftBetweenSpawns = _timeBetweenSpawns;
                }

                else
                {
                    _timeLeftBetweenSpawns -= gameTime.ElapsedGameTime.TotalSeconds;
                }

            else if (_enemiesSpawned == _numberOfEnemiesToSpawn) _isSpawning = false;

            if (EnemiesOnField.Count == 0 && !_isSpawning)
                if (_timeLeftBetweenWaves <= 0)
                {
                    _isSpawning = true;
                    _waveIndex++;
                    _enemiesSpawned = 0;
                    meleesSpawned = 0;
                    gunnersSpawned = 0;
                    _timeLeftBetweenWaves = _timeBetweenWaves;

                    if (_waveIndex < 3)
                    {
                        numberOfmelee = _waveIndex * 2 + 1;
                    }
                    else if (_waveIndex < 7)
                    {
                        numberOfmelee = _waveIndex - 1;
                        numberOfGunner = _waveIndex - 2;
                    }
                    else
                    {
                        numberOfmelee = _waveIndex - 1;
                        numberOfGunner = _waveIndex + 2;
                    }
                    _numberOfEnemiesToSpawn = numberOfGunner + numberOfmelee;
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

        public Vector2 GetGunnerLocation()
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

        public Vector2 GetMeleeLocation()
        {
            var rand = new Random();
            var playerZone = new Rectangle(Player.PlayerPosition.ToPoint() - new Point(150, 150), new Point(300, 300));

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