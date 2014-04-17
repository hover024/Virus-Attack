using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VirusAttack
{
    public class Tower : Sprite
    {
        public int Cost { get; protected set; }
        public int Damage { get; protected set; }

        public float Radius { get; protected set; }

        public Enemy Target { get; protected set; }

        protected float BulletTimer;

        protected List<Bullet> BulletList;
 
        protected Texture2D BulletTexture;

        public Tower(Texture2D tex, Texture2D bulletTexture, Vector2 position)
            : base(tex, position)
        {
            BulletTexture = bulletTexture;
            BulletList=new List<Bullet>();
        }

        public bool IsInRange(Vector2 position)
        {
            return Vector2.Distance(Сenter, position) <= Radius;
        }

        public void SetTarget(List<Enemy> enemies)
        {
            Target = null;
            var smallestRange = Radius;
            foreach (var enemy in enemies)
            {
                if (!(Vector2.Distance(Сenter, enemy.Сenter) < smallestRange)) continue;
                smallestRange = Vector2.Distance(Сenter, enemy.Сenter);
                Target = enemy;
            }
        }

        protected void FaceTarget()
        {
            var direction = Сenter - Target.Сenter;
            direction.Normalize();
            Rotation = (float) Math.Atan2(-direction.X, direction.Y);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            BulletTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Target != null)
            {
                
                if (!IsInRange(Target.Сenter) || Target.IsDead)
                {
                    Target = null;
                    BulletTimer = 0;
                }
                else
                {
                    FaceTarget();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var bullet in BulletList)
            {
                bullet.Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
        }
    }
}
