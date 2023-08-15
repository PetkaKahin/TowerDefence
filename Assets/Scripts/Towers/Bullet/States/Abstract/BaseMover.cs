using System;
using UnityEngine;

namespace Towers
{
    public abstract class BaseMover : IMover
    {
        protected readonly Transform Transform;

        public float Speed {  get; private set; }

        public BaseMover(Transform transform, float speed) 
        {
            Transform = transform;
            Speed = speed;

            SetSpeed(Speed);
        }

        public abstract void Move();

        public virtual void SetSpeed(float speed)
        {
            if (speed < 0)
                throw new ArgumentOutOfRangeException(nameof(speed));

            Speed = speed;
        }
    }
}
