using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VirusAttack
{
    public class WaveController
    {
        private int _numberOfWaves;
        private float _timeSinceLastWave;

        private Queue<Wave> _waves=new Queue<Wave>();

        private const int InitialNumberOfEnemies = 6;

        private Texture2D _enemyTexture;

        private bool _waveFinished = true;
        private Level _level;

        public WaveController(Level level, int numberOfWaves, Texture2D enemyTexture)
        {
            _level = level;
            _numberOfWaves = numberOfWaves;
            _enemyTexture = enemyTexture;
            for (var i = 0; i < _numberOfWaves; i++)
            {
                var numberModifier = (i/InitialNumberOfEnemies) + 1;
                var wave = new Wave(i, InitialNumberOfEnemies*numberModifier, _level, _enemyTexture);
                _waves.Enqueue(wave);
            }
        }

        public Wave CurrentWave
        {
            get { return _waves.Peek(); }
        }

        public List<Enemy> Enemies
        {
            get { return CurrentWave.Enemies; }
        }

        public int RoundNumber
        {
            get { return CurrentWave.RoundNumber + 1; }
        }

        private void StartNextWave()
        {
            if (_waves.Count <= 0) return;
            _waves.Peek().Start();
            _timeSinceLastWave = 0;
            _waveFinished = false;
        }

        public void Update(GameTime gameTime)
        {
            CurrentWave.Update(gameTime);
            if (CurrentWave.IsRoundOver)
            {
                _waveFinished = true;
            }
            if (_waveFinished)
            {
                _timeSinceLastWave += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (_timeSinceLastWave > 15.0f)
            {
                _waves.Dequeue();
                StartNextWave();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentWave.Draw(spriteBatch);
        }
    }
}
