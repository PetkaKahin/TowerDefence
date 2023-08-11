using UI.Health;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField] private ViewHealth _healthUI; // вот это надо исправить

        public IHealth Health;

        public Mover Mover { get; private set; }
        
        public void Construct(Mover mover, IHealth health)
        {
            Mover = mover;
            Health = health;

            Health.Changed += ChangeHealth;
            Health.Died += Die;

            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.isTrigger = true;

            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
        }

        private void OnDisable()
        {
            Mover.StopMove();
        }

        private void OnDestroy()
        {
            Health.Changed -= ChangeHealth;
            Health.Died -= Die;
        }

        public void BeginMove() => Mover.BeginMove();

        public void SetPosition(Vector3 position) => transform.position = position;

        public void ResetStats()
        {
            Health.Reset();
            Mover.Reset();
        }

        private void ChangeHealth()
        {
            _healthUI?.Set(Health.MaxHealth, Health.Value);
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}
