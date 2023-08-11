using Enemy;
using UnityEngine;
using System;

namespace Tower
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField, Range(1f, 100f)] private float _speed;
        [SerializeField, Range(0.1f, 15)] private float _damage;

        private Transform _target;

        private IHealth _targetEnemy;

        public void Construct(Transform target, IHealth targetEnemy)
        {
            _target = target;
            _targetEnemy = targetEnemy;
            _targetEnemy.Died += Destroy;
        }

        private void Update()
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            transform.position += direction * _speed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out BaseEnemy enemy))
            {
                enemy.Health.TakeDamage(_damage);
                Destroy();
            }
        }

        private void Destroy()
        {
            _targetEnemy.Died -= Destroy;
            gameObject.SetActive(false);
        }
    }
}

