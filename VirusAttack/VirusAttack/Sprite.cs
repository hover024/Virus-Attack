using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VirusAttack
{
    public class Sprite
    {
        protected Texture2D Texture;

        public Vector2 Position { get; protected set; }
        protected Vector2 Velocity;

        protected Vector2 Origin;
        public Vector2 Сenter { get; protected set; }


        protected float Rotation;

        public Sprite(Texture2D tex, Vector2 position)
        {
            Texture = tex;
            Position = position;
            Velocity = Vector2.Zero;
            if (Texture == null) return;
            Сenter = new Vector2(Position.X + Texture.Width/2, Position.Y + Texture.Height/2);
            Origin = new Vector2(Texture.Width/2, Texture.Height/2);
        }

        public virtual void Update(GameTime gameTime)
        {
            Сenter = new Vector2(Position.X + Texture.Width / 2, Position.Y + Texture.Height / 2);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Сenter, null, Color.White, Rotation, Origin, 1.0f, SpriteEffects.None, 0);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Texture, Сenter, null, color, Rotation, Origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
