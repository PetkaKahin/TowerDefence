using UnityEngine;

namespace Towers
{
    public class MoverToTarget : BaseMover
    {
        private readonly Transform _target;

        public MoverToTarget(Transform transform, Transform target, float speed) : base(transform, speed)
        {
            _target = target;
        }

        public override void Move()
        {
            Vector3 direction = (_target.position - Transform.position).normalized;
            Transform.position += direction * Speed * Time.deltaTime;
        }
    }
}
