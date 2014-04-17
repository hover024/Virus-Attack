using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VirusAttack
{
    public class Player
    {
        public int Money { get; private set; }
        public int Lives { get; private set; }
        private List<Tower> _towers=new List<Tower>();

        private MouseState _currentState;
        private MouseState _oldState;

        private Level _level;
        private Texture2D _towerTexture;
        private Texture2D _bulletTexture;

        public Player(Level level, Texture2D towerTexture, Texture2D bulletTexture)
        {
            _level = level;
            _towerTexture = towerTexture;
            _bulletTexture = bulletTexture;
        }

        private int _cellX;
        private int _cellY;
        private int _tileX;
        private int _tileY;

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            _currentState = Mouse.GetState();
            _cellX = (int) (_currentState.X/Level.BlockSize);
            _cellY = (int) (_currentState.Y/Level.BlockSize);
            _tileX = _cellX*Level.BlockSize;
            _tileY = _cellY*Level.BlockSize;
            if (_currentState.LeftButton == ButtonState.Released && _oldState.LeftButton == ButtonState.Pressed)
            {
                if (IsCellClear())
                {
                    var tower = new FireTower(_towerTexture, _bulletTexture,new Vector2(_tileX, _tileY));
                    _towers.Add(tower);
                }
            }
            _oldState = _currentState;
            foreach (var tower in _towers)
            {
                if (tower.Target == null)
                {
                    tower.SetTarget(enemies);
                }
                tower.Update(gameTime);
            }
        }

        private bool IsCellClear()
        {
            var inBounds = _cellX >= 0 && _cellX <= Level.LevelWidth/Level.BlockSize && _cellY >= 0 &&
                            _cellY <= (Level.LevelHeight-Level.BlockSize)/Level.BlockSize;
            var spaceClear = true;
            foreach (var tower in _towers)
            {
                spaceClear = (tower.Position != new Vector2(_tileX, _tileY));
                if (!spaceClear)
                    break;
            }

            var onPath = _level.GetIndex(_cellX, _cellY) != 1;
            return inBounds && spaceClear && onPath;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tower in _towers)
            {
                tower.Draw(spriteBatch);
            }
        }
    }
}
