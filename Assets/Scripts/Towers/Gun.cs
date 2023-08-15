using Enemy;
using System.Collections;
using UnityEngine;

namespace Towers
{
    [RequireComponent(typeof(BaseTower))]
    public class Gun : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 5f)] private float _coolDown;

        [SerializeField] private Transform _spawnPoint;

        private BulletPool _pool;

        private BaseTower _tower;

        private BaseEnemy _target;

        private WaitForSeconds _sleep;

        private IEnumerator _shooting;

        private float _timer;

        private bool _isTarget => _target != null;

        private void Start()
        {
            _tower = GetComponent<BaseTower>();
            _sleep = new WaitForSeconds(_coolDown);
            _tower.TargetDied += TargetDie;
            _tower.TargetSelectied += SetTarget;
            _shooting = Shooting();
        }

        public void Construct(BulletPool pool)
        {
            _pool = pool;
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
            Bullet bullet = _pool.Get(_spawnPoint);
            
            bullet.Construct(_target.transform, _target.Health);
        }

        private void TargetDie()
        {
            StopCoroutine(_shooting);
            _target = null;
        }
    }
}
