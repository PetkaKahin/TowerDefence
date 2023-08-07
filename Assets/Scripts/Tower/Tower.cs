using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Tower : MonoBehaviour // временный скрипт, всё переделать по хорошему
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _damage;
        [SerializeField] private float _coolDownAttak;

        private CircleCollider2D _collider;

        private List<BaseEnemy> _enemyes = new List<BaseEnemy>();
        private BaseEnemy _target;

        private WaitForSeconds _sleep;

        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
            _collider.radius = _radius;
            _collider.isTrigger = true;
            _sleep = new WaitForSeconds(_coolDownAttak);
            StartCoroutine(Damage());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            BaseEnemy enemy;

            if (collision.TryGetComponent(out enemy))
                _enemyes.Add(enemy);

            if (_target == null)
                ChooseNewTarget();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            BaseEnemy enemy;

            if (collision.TryGetComponent(out enemy))
            {
                ExitTarget();
            }
        }

        private IEnumerator Damage()
        {
            while (true)
            {
                _target?.TakeDamage(_damage);
                yield return _sleep;
            }
        }

        private void ExitTarget()
        {
            if (_target != null) 
            {
                _target.Died -= ExitTarget;
                _enemyes.Remove(_target);
                _target = null;
            }
            ChooseNewTarget();
        }

        private void ChooseNewTarget()
        {
            if (_enemyes.Count <= 0)
                return;

            _target = _enemyes[0];
            _target.Died += ExitTarget;
        }
    }
}
