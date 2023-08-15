using UnityEngine;
using UnityEngine.UI;

namespace Towers
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class TowerCreator : MonoBehaviour
    {
        [SerializeField] private int _towerId;

        private TowerFactory _factory;
        private Transform _spawnPoint;

        public Button Button { get; private set; }
        public Image Image { get; private set; }

        private void Awake()
        {
            Button = GetComponent<Button>();
            Image = GetComponent<Image>();
        }

        public void Construct(TowerFactory factory, Transform spawnPoint)
        {
           _factory = factory;
           SetSpawnPoint(spawnPoint);
        }

        public void SetSpawnPoint(Transform spawnPoint)
        {
            _spawnPoint = spawnPoint;
        }

        public void Create()
        {
            _factory.Get(_spawnPoint, _towerId);
        }
    }
}
