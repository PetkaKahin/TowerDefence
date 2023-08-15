using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Towers
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private Bullet _bullet;
        
        private List<Bullet> _bullets = new List<Bullet>();

        public Bullet Get(Transform spawnPoint)
        {
            Bullet bullet = _bullets.FirstOrDefault(findBullet => findBullet.gameObject.activeSelf == false);

            if (bullet == null)
            {
                bullet = Instantiate(_bullet, spawnPoint.position, spawnPoint.rotation, transform);
                _bullets.Add(bullet);
            }

            bullet.gameObject.transform.position = spawnPoint.position;
            bullet.gameObject.SetActive(true);
            return bullet;
        }
    }
}
