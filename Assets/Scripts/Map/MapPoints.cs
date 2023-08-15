using System.Collections.Generic;
using Towers;
using UnityEngine;

namespace Map
{
    public class MapPoints : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
        [SerializeField] private List<Transform> _movePoints = new List<Transform>();
        [SerializeField] private List<PointTowerHeandler> _towerPoints = new List<PointTowerHeandler>();

        public List<Transform> SpawnPoints => _spawnPoints;
        public List<Transform> MovePoints => _movePoints;
        public List<PointTowerHeandler> TowerPoints => _towerPoints;
    }
}
