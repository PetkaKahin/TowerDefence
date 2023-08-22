using UnityEngine;

namespace Map
{
    [RequireComponent(typeof(MapData))]
    public class MapInicialize : MonoBehaviour
    {
        [SerializeField] private MapConfig _mapConfig;

        private MapData _mapData;

        private void Awake()
        {
            _mapData = GetComponent<MapData>();

            InicializeEnemyPool();

            InicializeEnemySpawner();

            InicializeTowerHeandlers();
        }

        private void InicializeEnemyPool()
        {
            _mapData.EnemyFactoryConstruct(_mapConfig.Enemy, _mapConfig.EnemyConfig, _mapData.MapPoints);
            _mapData.EnemyPool.Construct(_mapData.EnemyFactory);
        }

        private void InicializeEnemySpawner()
        {
            for (int i = 0; i < _mapData.MapPoints.SpawnPoints.Count; i++)
            {
                _mapData.EnemySpawners.Add(Instantiate(_mapConfig.EnemySpawner, _mapData.MapPoints.SpawnPoints[i].position, Quaternion.identity, _mapData.MapPoints.SpawnPoints[i]));
                _mapData.EnemySpawners[i].Construct(_mapData.EnemyPool);
            }
        }

        private void InicializeTowerHeandlers()
        {
            _mapData.TowerFactoryConstruct(_mapConfig.Towers, _mapData.BulletPool);

            for (int i = 0; i < _mapData.MapPoints.TowerPoints.Count; i++)
            {
                _mapData.TowerHeandlersUI.Add(Instantiate(_mapConfig.TowerHeandlerUI, _mapData.MapPoints.TowerPoints[i].transform.position, Quaternion.identity, _mapData.UI));
                _mapData.MapPoints.TowerPoints[i].Construct(_mapData.TowerFactory, _mapData.TowerHeandlersUI[i]);
            }
        }
    }
}
