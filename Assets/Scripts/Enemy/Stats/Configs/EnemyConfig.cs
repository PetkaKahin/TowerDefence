using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Enemy/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField, Range(0.1f, 25)] private float _speed;
        [SerializeField, Range(1, 100)] private float _health;

        public float Speed => _speed;
        public float Health => _health;
    }
}
