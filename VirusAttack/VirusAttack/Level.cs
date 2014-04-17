using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VirusAttack
{
    public class Level
    {
        public const int BlockSize =0x28;
        public const int LevelHeight = 0x2a8;
        public const int LevelWidth = 0x500;
        private int[,] _map;
        public Queue<Vector2> WayPoints { get; private set; } 
        private List<Block> _blocks; 
        private List<Texture2D> _tileTextures;
        public int Number { get; private set; }

        public Level()
        {
            _blocks = new List<Block>();
            _tileTextures=new List<Texture2D>();
        }

        public void LoadMap(string fileName)
        {
            var intMap = File.ReadAllLines(fileName);
            _map = new int[intMap[0].Length, intMap.Count()];
            var x = 0;
            var y = 0;
            var i = 0;
            var j = 0;
            foreach (var s in intMap)
            {
                foreach (var c in s)
                {
                    if (c == '1')
                    {
                        _map[i, j] = 1;
                        _blocks.Add(new Block(_tileTextures[1], new Rectangle(x, y, BlockSize, BlockSize)));
                    }
                    if (c == '0')
                    {
                        _map[i, j] = 0;
                    }
                    x += BlockSize;
                    i++;
                }
                x = 0;
                i = 0;
                y += BlockSize;
                j++;
            }
        }

        public void LoadWayPoints(string fileName)
        {
            WayPoints=new Queue<Vector2>();
            foreach (var item in File.ReadAllLines(fileName))
            {
                WayPoints.Enqueue(ParseWayPoint(item));
            }
        }

        private Vector2 ParseWayPoint(string s)
        {
            var st = s.Split(',');
            return new Vector2(float.Parse(st[0]), float.Parse(st[1])) * BlockSize+new Vector2(5f,5f);
        }

        public int GetIndex(int cellX, int cellY)
        {
            if (cellX < 0 || cellX >LevelWidth/BlockSize || cellY < 0 || cellY > LevelHeight/BlockSize-1)
                return 0;
            return _map[cellX, cellY];
        }

        public void AddTexture(Texture2D texture)
        {
            _tileTextures.Add(texture);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
                spriteBatch.Draw(_tileTextures[0],new Rectangle(0,0,LevelWidth,LevelHeight+ BlockSize),Color.White);
            foreach (var block in _blocks)
            {
                block.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
