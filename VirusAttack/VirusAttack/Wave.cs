using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VirusAttack
{
    public class Wave
    {
        private int _numOfEnemies;
        private int _waveNumber;
        private float _spawnTimer = 0;
        private int _enemiesSpawned = 0;
        private bool enemyAtEnd;
        private bool _spawningEnemies;
        private Level _level;
        private Texture2D _enemyTexture;
        private List<Enemy> _enemies=new List<Enemy>();

        public Wave(int waveNumber, int numOfEnemies, Level level, Texture2D enemyTexture)
        {
            _waveNumber = waveNumber;
            _numOfEnemies = numOfEnemies;
            _level = level;
            _enemyTexture = enemyTexture;
        }

        public bool IsRoundOver
        {
            get { return Enemies.Count == 0 && _enemiesSpawned == _numOfEnemies; }
        }

        public int RoundNumber
        {
            get { return _waveNumber; }
        }

        public bool EnemyAtEnd
        {
            get { return enemyAtEnd; }
            set { enemyAtEnd = value; }
        }

        public List<Enemy> Enemies
        {
            get { return _enemies; }
            set { _enemies = value; }
        }

        private void AddEnemy()
        {
            var enemy = new Enemy(_enemyTexture, _level.WayPoints.Peek(), 50, 1, 2f);
            enemy.SetWayPoints(_level.WayPoints);
            _enemies.Add(enemy);
            _spawnTimer = 0;
            _enemiesSpawned++;
        }

        public void Start()
        {
            _spawningEnemies = true;
        }

        public void Update(GameTime gameTime)
        {
            if (_enemiesSpawned == _numOfEnemies)
                _spawningEnemies = false;
            if (_spawningEnemies)
            {
                _spawnTimer += (float) gameTime.ElapsedGameTime.TotalSeconds;
                if(_spawnTimer>2f)
                    AddEnemy();
            }
            for (int i = 0; i < _enemies.Count; i++)
            {
                var enemy = _enemies[i];
                enemy.Update(gameTime);
                if (enemy.IsDead)
                {
                    if (enemy.CurrentHealth > 0)
                    {
                        enemyAtEnd = true;
                    }
                    _enemies.Remove(enemy);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }
    }
}
