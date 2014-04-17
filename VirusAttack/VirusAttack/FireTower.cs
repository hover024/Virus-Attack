using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VirusAttack
{
    public class FireTower : Tower
    {
        public FireTower(Texture2D tex, Texture2D bulletTexture, Vector2 position) : base(tex, bulletTexture, position)
        {
            Damage = 15;
            Cost = 15;
            Radius = 80;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (BulletTimer >= 0.75f && Target != null)
            {
                var bullet = new Bullet(BulletTexture, Vector2.Subtract(Сenter, new Vector2(BulletTexture.Width/2)),
                    Rotation, 6, Damage);
                BulletList.Add(bullet);
                BulletTimer = 0;
            }

            for (var i = 0; i < BulletList.Count; i++)
            {
                var bullet = BulletList[i];
                bullet.SetRotation(Rotation);
                bullet.Update(gameTime);
                if(!IsInRange(bullet.Сenter))
                    bullet.Kill();
                if (Target != null && Vector2.Distance(bullet.Сenter, Target.Сenter) < 12)
                {
                    Target.CurrentHealth -= bullet.Damage;
                    bullet.Kill();
                }
                if (bullet.IsDead)
                {
                    BulletList.Remove(bullet);
                    i--;
                }
            }
        }
    }
}
