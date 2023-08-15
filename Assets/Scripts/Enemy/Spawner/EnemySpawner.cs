using DG.Tweening;
using Map;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField, Range(0.05f, 5f)] private float _coolDown;

        [Space, Header("Prefabs")]
        [SerializeField] private BaseEnemy _enemy;

        [SerializeField] private EnemyConfig _enemyConfig;

        private EnemyPool _pool;

        private WaitForSeconds _sleep;

        public void Construct(EnemyPool pool)
        {
            _pool = pool;

            DOTween.SetTweensCapacity(10000, 1000); // только, чтоб убрать сообщения из консоли, задолбали

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
