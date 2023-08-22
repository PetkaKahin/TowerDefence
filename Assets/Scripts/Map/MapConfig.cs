using Enemy;
using System.Collections.Generic;
using Towers;
using UI;
using UnityEngine;

namespace Map
{
    [CreateAssetMenu(fileName = "MapConfig", menuName = "Configs/Level/MapConfig")]
    public class MapConfig : ScriptableObject
    {
        [SerializeField] private List<BaseTower> _towers = new List<BaseTower>();

        [SerializeField] private EnemySpawner _enemySpawner;

        [SerializeField] private EnemyConfig _enemyConfig;

        [SerializeField] private BaseEnemy _enemy;

        [SerializeField] private TowerHeandlerUI _towerHeandlerUI;

        public List<BaseTower> Towers => _towers;
        public BaseEnemy Enemy => _enemy;
        public EnemySpawner EnemySpawner => _enemySpawner;
        public TowerHeandlerUI TowerHeandlerUI => _towerHeandlerUI;
        public EnemyConfig EnemyConfig => _enemyConfig;
    }
}
