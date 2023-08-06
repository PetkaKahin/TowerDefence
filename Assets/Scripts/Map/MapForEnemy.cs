using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapForEnemy : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
        [SerializeField] private List<Transform> _movePoints = new List<Transform>();

        public List<Transform> SpawnPoints => _spawnPoints;
        public List<Transform> MovePoints => _movePoints;
    }
}
