using DG.Tweening;
using Map;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private MapForEnemy _map;

        [SerializeField] private BaseEnemy _enemy;

        [SerializeField] private EnemyConfig _enemyConfig;

        [SerializeField] private EnemyPool _pool;

        [SerializeField] private float _coolDown;

        private EnemyFactory _factory;

        private WaitForSeconds _sleep;

        private void Awake()
        {
            DOTween.SetTweensCapacity(10000, 1000); // только, чтоб убрать сообщения из консоли, задолбали

            _factory = new EnemyFactory(_enemy, _map, _enemyConfig);

            _pool.Construct(_factory);

            _sleep = new WaitForSeconds(_coolDown);

            StartCoroutine(Create());
        }

        private void OnValidate()
        {
            _sleep = new WaitForSeconds(_coolDown);
        }

        private IEnumerator Create()
        {
            while (true)
            {
                _pool.Get();

                yield return _sleep;
            }
        }
    }
}
