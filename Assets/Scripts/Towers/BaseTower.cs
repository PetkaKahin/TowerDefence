using Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Towers
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class BaseTower : MonoBehaviour
    {
        [SerializeField] private float _maxRange;
        [SerializeField] private float _range;

        private List<BaseEnemy> _enemies = new List<BaseEnemy>();
        private BaseEnemy _target;

        private CircleCollider2D _collider;

        public float Range => _range;
        public float MaxRange => _maxRange;

        public event Action<BaseEnemy> TargetSelectied;
        public event Action TargetDied;

        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
            SetRange(_range);
            _collider.isTrigger = true;

            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out BaseEnemy enemy))
                _enemies.Add(enemy);

            if (_target == null)
                ChooseNewTarget();

            print($"Enemies enter = {_enemies.Count}");
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out BaseEnemy enemy))
                ExitTarget(enemy);

            print($"Enemies exit = {_enemies.Count}");
        }

        private void Update()
        {
            if (_target != null)
                Debug.DrawLine(transform.position, _target.transform.position, Color.magenta);
        }

        public void SetRange(float range)
        {
            if (range < 0)
                throw new ArgumentOutOfRangeException(nameof(range));

            if (range > MaxRange)
            {
                _range = MaxRange;
                return;
            }

            _range = range;
            _collider.radius = _range;
        }

        private void ExitTarget(BaseEnemy enemy)
        {
            _enemies.Remove(enemy);
            _target = null;
            TargetDied?.Invoke();

            ChooseNewTarget();
        }

        private void ChooseNewTarget()
        {
            if (_enemies.Count <= 0)
                return;

            _target = _enemies[0];

            TargetSelectied?.Invoke(_target);
        }
    }
}
