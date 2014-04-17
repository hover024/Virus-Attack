using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VirusAttack
{
    public class Block
    {
        public Texture2D BlockTexture { get; set; }
        public Rectangle Position { get; set; }

        public Block(Texture2D blockTexture, Rectangle position)
        {
            BlockTexture = blockTexture;
            Position = position;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BlockTexture,Position,Color.White);
        }
    }
}
