using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VirusAttack
{
    public class Enemy : Sprite
    {
        protected float StartHealth;
        public float CurrentHealth { get; set; }

        protected bool IsAlive = true;

        protected float Speed = 0.5f;
        public int BounthyGiven { get; protected set; }

        private Queue<Vector2> _wayPoints; 

        public bool IsDead
        {
            get { return !IsAlive; }
        }

        public float DistanceToDestination
        {
            get { return Vector2.Distance(Position,_wayPoints.Peek()); }
        }
        public Enemy(Texture2D tex, Vector2 position, float health, int bounthyGiven, float speed) : base(tex, position)
        {
            StartHealth = health;
            CurrentHealth = StartHealth;
            BounthyGiven = bounthyGiven;
            Speed = speed;
        }

        public void SetWayPoints(Queue<Vector2> wayPoints)
        {
            _wayPoints=new Queue<Vector2>();
            foreach (var wayPoint in wayPoints)
            {
                _wayPoints.Enqueue(wayPoint);
            }
            Position = _wayPoints.Dequeue();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (_wayPoints.Count > 0)
            {
                if (DistanceToDestination < Speed)
                {
                    Position = _wayPoints.Peek();
                    _wayPoints.Dequeue();
                    if (_wayPoints.Count > 0)
                    {
                        var k = Position - _wayPoints.Peek();
                        k.Normalize();
                        if (k.Y < 0) Rotation =(float)Math.PI/2;
                        if (k.Y > 0) Rotation = (float)(3*Math.PI / 2);
                        if (k.X < 0) Rotation = (float)(4 * Math.PI / 2);
                        if (k.X > 0) Rotation = (float)(2 * Math.PI / 2);
                    }
                }
                else
                {
                    var direction = _wayPoints.Peek() - Position;
                    direction.Normalize();
                    Velocity = Vector2.Multiply(direction, Speed);
                    Position += Velocity;
                }
            }
            else
            {
                IsAlive = false;
            }
            if (CurrentHealth<=0)
            {
                IsAlive = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsAlive) return;
            var healthPercentage = CurrentHealth/StartHealth;
            var color = new Color(new Vector3((1 - healthPercentage),  healthPercentage,  healthPercentage));
            base.Draw(spriteBatch,color);
        }
    }
}
