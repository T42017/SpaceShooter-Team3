using System.Collections.Generic;
using CursorAiming.Enemies;
using Microsoft.Xna.Framework;

namespace CursorAiming
{
    internal class Wave : SpaceHeadBaseComponent
    {
        public static int _waveIndex;

        public static List<Enemy> EnemiesOnField = new List<Enemy>();
        
        private static int _numberOfEnemies = _waveIndex * 2 + 1;
        static int _enemiesSpawned;
        private static  bool _isSpawning;

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
            if(_waveIndex < 5)
            EnemiesOnField.Add(new MeleeEnemy(300, 1, "spaceAstronauts_red", 20, 20, 20, Game)
            {
                Position = new Vector2(500, 500)
            });
            else if (_waveIndex < 10)
                EnemiesOnField.Add(new MeleeEnemy(300, 2, "spaceAstronauts_red", 20, 20, 20, Game)
                {          
                    Position = new Vector2(500, 500)
                });
            _enemiesSpawned++;
            
        }

        private void SpawnWave(GameTime gameTime)
        {
            if (_timeLeftBetweenSpawns <= 0)
            {
                _numberOfEnemies = _waveIndex * 2 + 1;

                //if (_waveIndex)
                SpawnMeleeEnemy();
                
                _timeLeftBetweenSpawns = _timeBetweenSpawns;
            }
            else
            {
                _timeLeftBetweenSpawns -= gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (_enemiesSpawned < _numberOfEnemies && _isSpawning)
                SpawnWave(gameTime);
            else if (_enemiesSpawned == _numberOfEnemies) _isSpawning = false;

            if (EnemiesOnField.Count == 0 && !_isSpawning)
            {
                if (_timeLeftBetweenWaves <= 0)
                {
                    _isSpawning = true;
                    _waveIndex++;
                    _enemiesSpawned = 0;
                    _timeLeftBetweenWaves = _timeBetweenWaves;
                }
                else _timeLeftBetweenWaves -= gameTime.ElapsedGameTime.TotalSeconds;
            }

            base.Update(gameTime);
        }

        public static void Reset()
        {
            _waveIndex = 0;
            _timeLeftBetweenSpawns = _timeBetweenSpawns;
            _timeLeftBetweenWaves = _timeBetweenWaves;
            _isSpawning = false;
            _numberOfEnemies = _waveIndex * 2 + 1;
            for (int i = 0; i < EnemiesOnField.Count; i++)
            {
                EnemiesOnField[i].Remove();
                i--;
            }
            EnemiesOnField.Clear();
            
        }
    }
}