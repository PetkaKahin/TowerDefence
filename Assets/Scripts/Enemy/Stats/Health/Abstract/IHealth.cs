using System;

namespace Enemy
{
    public interface IHealth
    {
        float MaxHealth { get; }
        float Value { get; }

        event Action Died;
        event Action Changed;

        void TakeDamage(float damage);
        void Heal(float value);
        void Reset();
    }
}
