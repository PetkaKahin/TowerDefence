using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy
{
    public class EnemyPool : MonoBehaviour
    {
        private EnemyFactory _factory;
        private List<BaseEnemy> _ememyes = new List<BaseEnemy>();

        public void Construct(EnemyFactory factory)
        {
            _factory = factory;
        }

        public BaseEnemy Get()
        {
            BaseEnemy enemy = _ememyes.FirstOrDefault(findEnemy => findEnemy.gameObject.activeSelf == false);

            if (enemy == null)
                enemy = _factory.Get(transform);

            _ememyes.Add(enemy);

            enemy.gameObject.SetActive(true);
            enemy = _factory.CreateNewRoute(enemy);
            enemy.BeginMove();
            enemy.ResetStats();

            return enemy;
        }
    }
}
 