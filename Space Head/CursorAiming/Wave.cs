﻿using System;
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
        private static int _numberOfmelee, _numberOfGunner, _numberOfShips;
        private static int _meleesSpawned, _gunnersSpawned, _shipsSpawned;


        public Wave(Game game) : base(game)
        {
            Game.Components.Add(this);
            UpdatableStates = GameState.Playing;
            _timeBetweenWaves = 4;
            _timeBetweenSpawns = 1;
            _meleesSpawned = 0;
            _gunnersSpawned = 0;
            _shipsSpawned = 0;
            _enemiesSpawned = 0;
            _timeLeftBetweenWaves = _timeBetweenWaves;
        }

        public static void SpawnMeleeEnemy(Game game, Vector2 position)
        {
            if (_waveIndex < 5)
                EnemiesOnField.Add(new MeleeEnemy(100, 6, "MeleeEnemy1", 40, 20, 20, game)
                {
                    Position = position
                });
            else /*if (_waveIndex < 10)*/
                EnemiesOnField.Add(new MeleeEnemy(150, 13, "MeleeEnemy1", 80, 50, 50, game)
                {
                    Position = position
                });
            _enemiesSpawned++;
            _meleesSpawned++;
        }

        private void SpawnGunner()
        {
            EnemiesOnField.Add(new EnemyWithGun(new Gun("EnemyGun1","EnemyShot", 1, 300, UnitType.Player, Game), 150, 20, 2,"Enemy1", 100, 120, 60, Game)
            {
                Position = GetGunnerLocation()
            });
            _enemiesSpawned++;
            _gunnersSpawned++;
        }
        private void SpawnShip()
        {
            EnemiesOnField.Add(new SpaceShip1(100, 6, "Ship", 40, 20, 20, Game)
            {
                Position = GetGunnerLocation()
            });
            _enemiesSpawned++;
            _shipsSpawned++;
        }

        public override void Update(GameTime gameTime)
        {
            if (_enemiesSpawned < _numberOfEnemiesToSpawn && _isSpawning)
                if (_timeLeftBetweenSpawns <= 0)
                {
                    if (_meleesSpawned < _numberOfmelee)
                        SpawnMeleeEnemy(Game, GetMeleeLocation());
                    if (_gunnersSpawned < _numberOfGunner)
                        SpawnGunner();
                    if(_shipsSpawned < _numberOfShips)
                        SpawnShip();

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
                    _meleesSpawned = 0;
                    _gunnersSpawned = 0;
                    _timeLeftBetweenWaves = _timeBetweenWaves;

                    if (_waveIndex < 5)
                    {
                        _numberOfmelee = _waveIndex * 2 + 1;
                        _numberOfGunner = 0;
                        _numberOfShips = 0;
                    }
                    else if (_waveIndex < 7)
                    {
                        _numberOfmelee = _waveIndex - 1;
                        _numberOfGunner = _waveIndex - 2;
                        _numberOfShips = 0;

                    }
                    else if (_waveIndex < 10)
                    {
                        _numberOfmelee = _waveIndex - 1;
                        _numberOfGunner = _waveIndex + 2;
                        _numberOfShips = 0;

                    }
                    else
                    {
                        _numberOfmelee = 0;
                        _numberOfGunner = _waveIndex - 2;
                        _numberOfShips = _waveIndex;

                    }
                    _numberOfEnemiesToSpawn = _numberOfGunner + _numberOfmelee + _numberOfShips;
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
            _numberOfGunner = 0;
            _numberOfmelee = 0;
            _numberOfShips = 0;
            _numberOfEnemiesToSpawn = 0;
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

        public static Vector2 GetMeleeLocation()
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