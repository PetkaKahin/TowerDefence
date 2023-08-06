using UI.Health;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 15f)] private float _speed;
        [SerializeField] private ViewHealth _healthUI; // вот это надо исправить

        private IHealth _health;

        public Mover Mover { get; private set; }
        
        public void Construct(Mover mover, float health) // бахнуть конфиг со статами врага
        {
            Mover = mover;
            Mover.SetSpeed(_speed); 

            _health = new Health(health); // чтобы не было такого
            _health.Changed += ChangeHealth;
            _health.Died += Die;
        }

        private void OnDisable()
        {
            Mover.StopMove();
        }

        private void OnDestroy()
        {
            _health.Changed -= ChangeHealth;
            _health.Died -= Die;
        }

        public void BeginMove() => Mover.BeginMove();

        public void SetPosition(Vector3 position) => transform.position = position; // думаю это ещё  можно поправить

        public void TakeDamage(float damage) => _health.TakeDamage(damage);

        public void Heal(float value) => _health.Heal(value);

        public void ResetStats()
        {
            _health.Reset();
        }

        private void ChangeHealth()
        {
            _healthUI.Set(_health.MaxHealth, _health.Value);
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}
