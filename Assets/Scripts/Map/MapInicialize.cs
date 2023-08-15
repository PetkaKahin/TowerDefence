using Enemy;
using System.Collections.Generic;
using Towers;
using UI;
using UnityEngine;

namespace Map
{
    public class MapInicialize : MonoBehaviour
    {
        [SerializeField] private MapPoints _mapPoints;
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private EnemyPool _enemyPool;

        [SerializeField] private Transform _UI;

        [Space, Header("Prefabs")]
        [SerializeField] private BaseEnemy _enemy;
        [SerializeField] private EnemyConfig _enemyConfig;

        [SerializeField] private List<BaseTower> _towers = new List<BaseTower>();

        [SerializeField] private EnemySpawner _enemySpawnerPrefab;

        [SerializeField] private TowerHeandlerUI _towerHeandlerUIPrefab;

        private List<EnemySpawner> _enemySpawners = new List<EnemySpawner>(); // обернуть в дату или типа того
        private List<TowerHeandlerUI> _towerHeandlersUI = new List<TowerHeandlerUI>();

        private TowerFactory _towerFactory;
        private EnemyFactory _enemyFactory;

        private void Awake()
        {
            InicializeEnemyPool();

            InicializeEnemySpawner();

            InicializeTowerHeandlers();
        }

        private void InicializeEnemyPool()
        {
            _enemyFactory = new EnemyFactory(_enemy, _enemyConfig, _mapPoints);
            _enemyPool.Construct(_enemyFactory);
        }

        private void InicializeEnemySpawner()
        {
            for (int i = 0; i < _mapPoints.SpawnPoints.Count; i++)
            {
                _enemySpawners.Add(Instantiate(_enemySpawnerPrefab, _mapPoints.SpawnPoints[i].position, Quaternion.identity, transform));
                _enemySpawners[i].Construct(_enemyPool);
            }
        }

        private void InicializeTowerHeandlers()
        {
            _towerFactory = new TowerFactory(_towers, _bulletPool);

            for (int i = 0; i < _mapPoints.TowerPoints.Count; i++)
            {
                _towerHeandlersUI.Add(Instantiate(_towerHeandlerUIPrefab, _mapPoints.TowerPoints[i].transform.position, Quaternion.identity, _UI));
                _mapPoints.TowerPoints[i].Construct(_towerFactory, _towerHeandlersUI[i]);
            }
        }
    }
}
