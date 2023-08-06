using Map;
using UnityEngine;

namespace Enemy
{
    public class EnemyFactory
    {
        private readonly MapForEnemy _map;
        private readonly Transform[] _spawnPoints;
        private readonly BaseEnemy _enemy; // это временно

        private  Vector3[] _movePoints;

        public EnemyFactory(BaseEnemy enemy, MapForEnemy map)
        {
            _enemy = enemy;
            _spawnPoints = map.SpawnPoints.ToArray();
            _map = map;

            InicializeMovePoints();
        }

        public BaseEnemy Get(Transform parent)
        {
            int randomIndex = Random.Range(0, _spawnPoints.Length);

            BaseEnemy enemy = MonoBehaviour.Instantiate(_enemy, _spawnPoints[randomIndex].position, _spawnPoints[randomIndex].rotation, parent);

            return InicializeEnemy(enemy);
        }

        public BaseEnemy CreateNewRoute(BaseEnemy enemy)
        {
            enemy.SetPosition(_spawnPoints[Random.Range(0, _spawnPoints.Length)].position);
            enemy.Mover.CreateNewRute(_movePoints);

            return enemy;
        }

        private BaseEnemy InicializeEnemy(BaseEnemy enemy)
        {
            enemy.Construct(new Mover(_movePoints, enemy.transform), 10f);

            return enemy;
        }

        private void InicializeMovePoints()
        {
            _movePoints = new Vector3[_map.MovePoints.Count];

            for (int i = 0; i < _movePoints.Length; i++)
            {
                _movePoints[i] = _map.MovePoints[i].position;
            }
        }
    }
}
