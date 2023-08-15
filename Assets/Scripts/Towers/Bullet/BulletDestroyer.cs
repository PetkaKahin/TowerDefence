using UnityEngine;

namespace Towers
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]

    public class BulletDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Bullet bullet))
                bullet.gameObject.SetActive(false);
        }
    }
}
