using Enemy;
using System;
using System.Collections;
using UnityEngine;

namespace Towers
{
    [RequireComponent(typeof(BaseTower))]
    public class Gun : MonoBehaviour
    {
        [SerializeField, Range(0.05f, 5f)] private float _coolDown;
        [SerializeField, Range(0.05f, 5f)] private float _minCoolDown;

        [SerializeField, Range(0.1f, 50f)] private float _damage;
        [SerializeField, Range(0.1f, 50f)] private float _maxDamage;

        private BulletPool _pool;

        private BaseTower _tower;

        private BaseEnemy _target;

        private WaitForSeconds _sleep;

        private IEnumerator _shooting;

        private float _timer;

        private bool _isTarget => _target != null;

        public float CoolDown => _coolDown;
        public float MinCoolDown => _minCoolDown;
        public float Damage => _damage;
        public float MaxDamage => _maxDamage;

        public void Construct(BulletPool pool)
        {
            _pool = pool;
        }

        private void Start()
        {
            if (_coolDown < MinCoolDown)
                _coolDown = MinCoolDown;

            if (_damage > MaxDamage)
                _damage = MaxDamage;

            _tower = GetComponent<BaseTower>();
            SetCoolDonw(_coolDown);
            _tower.TargetDied += TargetDie;
            _tower.TargetSelectied += SetTarget;

            _shooting = Shooting();
        }

        private void OnDisable()
        {
            _tower.TargetDied -= TargetDie;
            _tower.TargetSelectied -= SetTarget;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }

        public void SetDamage(float damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            if (damage > MaxDamage)
            {
                _damage = MaxDamage;
                return;
            }

            _damage = damage;
        }

        public void SetCoolDonw(float coolDonw)
        {
            if (coolDonw < 0)
                throw new ArgumentOutOfRangeException(nameof(coolDonw));

            if (coolDonw < MinCoolDown)
            {
                _coolDown = MinCoolDown;
                _sleep = new WaitForSeconds(_coolDown);
                Debug.Log(_coolDown);
                return;
            }

            _coolDown = coolDonw;
            _sleep = new WaitForSeconds(_coolDown);
        }

        

        public void SetTarget(BaseEnemy target)
        {
            _target = target;

            StopCoroutine(_shooting);
            StartCoroutine(_shooting);
        }

        private IEnumerator Shooting()
        {
            while (_isTarget)
            {
                if (_timer >= _coolDown)
                {
                    _timer = 0;
                    Shoot();
                }

                yield return _sleep;
            }
        }

        private void Shoot() 
        {
            Bullet bullet = _pool.Get(transform);
            
            bullet.Construct(_target.transform, _target.Health, _damage);
        }

        private void TargetDie()
        {
            StopCoroutine(_shooting);
            _target = null;
        }
    }
}
