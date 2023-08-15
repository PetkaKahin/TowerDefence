using UnityEngine;

namespace Towers
{
    public class MoverStraightLine : BaseMover
    {
        private Vector3 _direction;

        public MoverStraightLine(Transform transform, Vector3 direction, float speed) : base(transform, speed)
        {
            _direction = direction;
        }

        public override void Move()
        {
            Transform.position += _direction * Speed * Time.deltaTime;
        }
    }
}