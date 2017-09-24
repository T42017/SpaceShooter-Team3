using System;
using System.Collections.Generic;
using CursorAiming.Enemies;
using Microsoft.Xna.Framework;

namespace CursorAiming
{
    internal class Wave : SpaceHeadBaseComponent
    {
        public static int WaveIndex;

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
            UpdatableStates = GameState.Playing;
            _timeBetweenWaves = 4;
            _timeBetweenSpawns = 1;
            _meleesSpawned = 0;
            _gunnersSpawned = 0;
            _shipsSpawned = 0;
            _enemiesSpawned = 0;
            _timeLeftBetweenWaves = _timeBetweenWaves;
        }

        private void SpawnMeleeEnemy(Game game)
        {
            if (WaveIndex < 5)
                EnemiesOnField.Add(new MeleeEnemy(100, 6, "MeleeEnemy1", 40, 20, 20, game)
                {
                    Position = GetMeleeLocation()
                });
            else if (WaveIndex < 10)
                EnemiesOnField.Add(new MeleeEnemy(150, 12, "MeleeEnemy1", 80, 50, 50, game)
                {
                    Position = GetMeleeLocation()
                });
            else if(WaveIndex < 15)
                EnemiesOnField.Add(new MeleeEnemy(150, 18, "MeleeEnemy1", 100, 80, 80, game)
                {
                    Position = GetMeleeLocation()
                });
            
            _enemiesSpawned++;
            _meleesSpawned++;
        }

        private void SpawnGunner()
        {
            if(WaveIndex < 10)
            EnemiesOnField.Add(new EnemyWithGun(new Gun("EnemyGun1","EnemyShot", 1, 250, UnitType.Player, Game), 150, 20, 1.5,"Enemy1", 100, 120, 60, Game)
            {
                Position = GetGunnerLocation()
            });
            else if (WaveIndex < 10)
                EnemiesOnField.Add(new EnemyWithGun(new Gun("EnemyGun1", "EnemyShot", 1, 300, UnitType.Player, Game), 150, 25, 2, "Enemy1", 120, 160, 100, Game)
                {
                    Position = GetGunnerLocation()
                });
            else if (WaveIndex < 15)
                EnemiesOnField.Add(new EnemyWithGun(new Gun("EnemyGun1", "EnemyShot", 1, 300, UnitType.Player, Game), 150, 30, 2, "Enemy1", 200, 240, 160, Game)
                {
                    Position = GetGunnerLocation()
                });
            else
                EnemiesOnField.Add(new EnemyWithGun(new Gun("EnemyGun1", "EnemyShot", 1, 350, UnitType.Player, Game), 150, WaveIndex*2, 2.5, "Enemy1", 240, 260, 180, Game)
                {
                    Position = GetGunnerLocation()
                });
            _enemiesSpawned++;
            _gunnersSpawned++;
        }
        private void SpawnShip()
        {
            if(WaveIndex < 15)           
                EnemiesOnField.Add(new SpaceShip1(100, 20, "Ship", 120, 150, 180, Game)
                {
                    Position = GetGunnerLocation()
                });

            else 
                EnemiesOnField.Add(new SpaceShip1(100, WaveIndex * 3, "Ship", 200, 210, 240, Game)
                {
                    Position = GetGunnerLocation()
                });
           

            _enemiesSpawned++;
            _shipsSpawned++;
        }
        public static void SpawnShipSpawn(Game game, Vector2 position)
        {
            if (WaveIndex < 15)
                EnemiesOnField.Add(new SpaceShipSpawn(100, 10, "ShipSpawn", 0, 0, 0, game)
                {
                 Position = position
                });
            else 
                EnemiesOnField.Add(new SpaceShipSpawn(100, WaveIndex*2, "ShipSpawn", 0, 0, 0, game)
                {
                    Position = position
                });
        }

        public override void Update(GameTime gameTime)
        {
            if (_enemiesSpawned < _numberOfEnemiesToSpawn && _isSpawning)
                if (_timeLeftBetweenSpawns <= 0)
                {
                    if (_meleesSpawned < _numberOfmelee)
                        SpawnMeleeEnemy(Game);
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
                    WaveIndex++;
                    if (WaveIndex % 5 == 0) Player.Health++;
                    _enemiesSpawned = 0;
                    _meleesSpawned = 0;
                    _gunnersSpawned = 0;
                    _shipsSpawned = 0;
                    _timeLeftBetweenWaves = _timeBetweenWaves;

                    if (WaveIndex < 6)
                    {
                        _numberOfmelee = WaveIndex * 2;
                        _numberOfGunner = 0;
                        _numberOfShips = 0;
                    }
                    else if (WaveIndex < 8)
                    {
                        _numberOfmelee = WaveIndex - 1;
                        _numberOfGunner = WaveIndex /2;
                        _numberOfShips = 0;

                    }
                    else if (WaveIndex < 10)
                    {
                        _numberOfmelee = WaveIndex - 1;
                        _numberOfGunner = WaveIndex -2;
                        _numberOfShips = 0;

                    }
                    else if (WaveIndex < 15)
                    {
                        _numberOfmelee = 0;
                        _numberOfGunner = (int)(WaveIndex / 1.5);
                        _numberOfShips = WaveIndex / 2;

                    }
                    else
                    {
                        _numberOfmelee = WaveIndex/3;
                        _numberOfGunner = (int)(WaveIndex / 1.5);
                        _numberOfShips = WaveIndex / 2;
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
            WaveIndex = 0;
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