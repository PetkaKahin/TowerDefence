using Assets.Scripts;
using Enemy;
using System.Collections.Generic;
using Towers;
using UI;
using UnityEngine;

namespace Map
{
    public class MapData : MonoBehaviour
    {
        [SerializeField] private MapPoints _mapPoints;

        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private EnemyPool _enemyPool;

        [SerializeField] private Transform _UI;

        private List<EnemySpawner> _enemySpawners = new List<EnemySpawner>();
        private List<TowerHeandlerUI> _towerHeandlersUI = new List<TowerHeandlerUI>();

        private Wallet _wallet = new Wallet();

        private TowerFactory _towerFactory;
        private EnemyFactory _enemyFactory;

        public TowerFactory TowerFactory => _towerFactory;
        public EnemyFactory EnemyFactory => _enemyFactory;
        public MapPoints MapPoints => _mapPoints;
        public BulletPool BulletPool => _bulletPool;
        public EnemyPool EnemyPool => _enemyPool;
        public Transform UI => _UI;
        public Wallet Wallet => _wallet;

        [HideInInspector] public List<EnemySpawner> EnemySpawners = new List<EnemySpawner>();
        [HideInInspector] public List<TowerHeandlerUI> TowerHeandlersUI = new List<TowerHeandlerUI>();

        public void EnemyFactoryConstruct(BaseEnemy enemy, EnemyConfig config, MapPoints points)
        {
            _enemyFactory = new EnemyFactory(enemy,config, points);
        }

        public void TowerFactoryConstruct(List<BaseTower> towers, BulletPool pool)
        {
            _towerFactory = new TowerFactory(towers, pool);
        }
    }
}
