using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy
{
    public class EnemyPool : MonoBehaviour
    {
        private EnemyFactory _factory;
        private List<BaseEnemy> _enemyes = new List<BaseEnemy>();

        public void Construct(EnemyFactory factory)
        {
            _factory = factory;
        }

        public BaseEnemy Get()
        {
            BaseEnemy enemy = _enemyes.FirstOrDefault(findEnemy => findEnemy.gameObject.activeSelf == false);

            if (enemy == null)
            {
                enemy = _factory.Get(transform);
                enemy = _factory.CreateNewRoute(enemy);
                _enemyes.Add(enemy);
                return enemy;
            }

            enemy = _factory.CreateSpawnPosition(enemy);
            enemy.gameObject.SetActive(true);
            enemy.BeginMove();
            enemy.ResetStats();

            return enemy;
        }
    }
}
 