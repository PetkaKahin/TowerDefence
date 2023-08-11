using Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float _range;

        private List<BaseEnemy> _enemyes = new List<BaseEnemy>();
        private BaseEnemy _target;

        public event Action<BaseEnemy> TargetSelectied;
        public event Action TargetDied;

        private void Awake()
        {
            CircleCollider2D collider = GetComponent<CircleCollider2D>();
            collider.radius = _range;
            collider.isTrigger = true;

            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out BaseEnemy enemy))
                _enemyes.Add(enemy);

            if (_target == null)
                ChooseNewTarget();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out BaseEnemy enemy))
                ExitTarget(enemy);
        }

        private void Update()
        {
            if (_target != null)
                Debug.DrawLine(transform.position, _target.transform.position, Color.magenta);
        }

        private void ExitTarget(BaseEnemy enemy)
        {
            _enemyes.Remove(enemy);
            _target = null;
            TargetDied?.Invoke();

            ChooseNewTarget();
        }

        private void ChooseNewTarget()
        {
            if (_enemyes.Count <= 0)
                return;

            _target = _enemyes[0];

            TargetSelectied?.Invoke(_target);
        }
    }
}
