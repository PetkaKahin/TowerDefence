using Enemy;
using UnityEngine;
using System;

namespace Towers
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField, Range(1f, 100f)] private float _speed;
        private float _damage;

        private Transform _target;

        private IHealth _targetEnemy;

        private IMover _mover;
        public void Construct(Transform target, IHealth targetEnemy, float damage)
        {
            _damage = damage;
            _target = target;
            _targetEnemy = targetEnemy;

            _targetEnemy.Died += LossTarget;

            _mover = new MoverToTarget(transform, _target, _speed);
        }

        private void Update()
        {
            _mover.Move();   
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out BaseEnemy enemy))
            {
                enemy.Health.TakeDamage(_damage);
                gameObject.SetActive(false);
            }
        }

        private void LossTarget()
        {
            _targetEnemy.Died -= LossTarget;

            Vector3 direction = (_target.position - transform.position).normalized;
            _mover = new MoverStraightLine(transform, direction, _speed);
        }

        private void OnDisable()
        {
            gameObject.SetActive(false);
            _targetEnemy.Died -= LossTarget;
            _target = null;
            _targetEnemy = null;
        }
    }
}

