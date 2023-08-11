using Enemy;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyDamager : MonoBehaviour
    {
        [SerializeField] private float _damage;

        private void Awake()
        {
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out BaseEnemy enemy))
            {
                enemy.Health.TakeDamage(_damage);
            }
        }
    }
}
