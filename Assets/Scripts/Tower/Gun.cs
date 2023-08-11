using Assets.Scripts.Tower;
using Enemy;
using System.Collections;
using UnityEngine;

namespace Tower
{
    public class Gun : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 5f)] private float _coolDown;

        [SerializeField] private Transform _spawnPoint;

        [SerializeField] private BulletPool _pool;

        [SerializeField] private Tower _tower;

        private BaseEnemy _target;

        private WaitForSeconds _sleep;

        private IEnumerator _shooting;

        private bool _isTarget => _target != null;

        private void Awake()
        {
            _sleep = new WaitForSeconds(_coolDown);
            _tower.TargetDied += TargetDie;
            _tower.TargetSelectied += SetTarget;
            _shooting = Shooting();
        }

        private void OnDisable()
        {
            _tower.TargetDied -= TargetDie;
            _tower.TargetSelectied -= SetTarget;
        }

        public void SetTarget(BaseEnemy target)
        {
            _target = target;
            StartCoroutine(_shooting);
        }

        private IEnumerator Shooting()
        {
            while (_isTarget)
            {
                Shoot();

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
