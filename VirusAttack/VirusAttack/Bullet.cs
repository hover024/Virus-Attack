using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VirusAttack
{
    public class Bullet : Sprite
    {
        public int Damage { get; private set; }
        private int _age;
        private int _speed;

        public bool IsDead
        {
            get { return _age > 100; }
        }

        public Bullet(Texture2D tex, Vector2 position, float rotation, int speed, int damage) : base(tex, position)
        {
            _speed = speed;
            Damage = damage;
            Rotation = rotation;
        }

        public void Kill()
        {
            _age = 200;
        }

        public override void Update(GameTime gameTime)
        {
            _age++;
            Position += Velocity;
            base.Update(gameTime);
        }

        public void SetRotation(float value)
        {
            Rotation = value;

            Velocity = Vector2.Transform(new Vector2(0, -_speed),
                Matrix.CreateRotationZ(Rotation));
        }
    }
}
