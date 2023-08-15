using Enemy;
using UI.Health;
using UnityEngine;

namespace Custle
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Castle : MonoBehaviour
    {
        private const int EnemyDamage = 1;

        [SerializeField] private int _maxHealth;

        [SerializeField] private ViewHealth _viewHealth; // xyeta

        private int _health;

        private void Awake()
        {
            _health = _maxHealth;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out BaseEnemy enemy))
            {
                TakeDamage(EnemyDamage); 
            }
        }

        private void TakeDamage(int damage)
        {
            _health -= damage;

            _viewHealth.Set(_maxHealth, _health);

            if (_health <= 0)
                Die();
        }

        private void Die()
        {
            Debug.Log("Game over epta");
        }
    }
}
