using System;
using UI.Health;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField] private ViewHealth _healthUI; // вот это надо исправить

        private IHealth _health;

        public Mover Mover { get; private set; }

        public event Action Died;
        public event Action ChangedHealth;
        
        public void Construct(Mover mover, IHealth health)
        {
            Mover = mover;
            _health = health;

            _health.Changed += ChangeHealth;
            _health.Died += Die;

            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.isTrigger = true;
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

        public void SetPosition(Vector3 position) => transform.position = position;

        public void TakeDamage(float damage) => _health.TakeDamage(damage);

        public void Heal(float value) => _health.Heal(value);

        public void ResetStats()
        {
            _health.Reset();
            Mover.Reset();
        }

        private void ChangeHealth()
        {
            _healthUI?.Set(_health.MaxHealth, _health.Value);
            ChangedHealth?.Invoke();
        }

        private void Die()
        {
            gameObject.SetActive(false);
            Died?.Invoke();
        }
    }
}
