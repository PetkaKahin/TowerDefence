using System.Collections.Generic;
using System;
using UnityEngine;

namespace Towers
{
    public class TowerFactory
    {
        private List<BaseTower> _towers = new List<BaseTower>();
        private BulletPool _bulletPool;

        public int TowersCount => _towers.Count;

        public TowerFactory(List<BaseTower> towers, BulletPool pool) 
        { 
            _towers = towers;
            _bulletPool = pool;
        }

        public BaseTower Get(Transform spawnPoint, int id = 0)
        {
            if (id < 0 && id >= _towers.Count)
                throw new ArgumentOutOfRangeException(nameof(id));   

            BaseTower tower = MonoBehaviour.Instantiate(_towers[id], spawnPoint.position, Quaternion.identity);
            
            if (tower.TryGetComponent(out Gun gun))
            {
                gun.Construct(_bulletPool);
            }

            return tower;
        }
    }
}
