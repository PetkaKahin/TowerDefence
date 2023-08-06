using Enemy;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyDamager : MonoBehaviour
    {
        [SerializeField] private float _damage;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out BaseEnemy enemy))
            {
                enemy.TakeDamage(_damage);
            }
        }
    }
}
