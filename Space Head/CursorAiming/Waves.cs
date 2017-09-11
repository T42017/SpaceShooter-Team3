using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Timer = System.Timers.Timer;

namespace CursorAiming
{
    public class Waves : SpaceHeadBaseComponent
    {
        public static List<Enemy> EnemyUnitsOnField = new List<Enemy>();
        private Timer _spawnTimer;
        protected new SpriteBatch SpriteBatch;
        private Random _rng = new Random();
        public Texture2D UnitTexture { get; set; }

        public Waves(Game game) : base(game)
        {
            _spawnTimer = new Timer(_rng.Next(500, 500));
            _spawnTimer.Elapsed += SpawnEnemy;
            _spawnTimer.Start();
        }

        public void SpawnEnemy(object sender, ElapsedEventArgs e)
        {
            _rng = new Random();
            var newEnemyPosition = Vector2.Zero;
            newEnemyPosition.X = _rng.Next(0, 1280);    
            newEnemyPosition.Y = _rng.Next(0, 1280);      

            var spawnedEnemies = new EnemyWithGun(new Gun("PlayerGun1", "laserBlue01", 1, 700, UnitType.Player, Game), 200, 2, 1d, "BasicEnemy", Game);
            spawnedEnemies.Position = newEnemyPosition;
            EnemyUnitsOnField.Add(spawnedEnemies);

            _spawnTimer = new Timer(_rng.Next(500, 500));
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            base.LoadContent();
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
