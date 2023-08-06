using Enemy;
using System;

public class Health : IHealth
{
    private readonly float _maxHealth;

    private float _health;

    public Health(float health)
    {
        _maxHealth = health;
        _health = _maxHealth;
    }

    public float MaxHealth => _maxHealth;
    public float Value => _health;

    public event Action Died;
    public event Action Changed;

    public void Heal(float value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        _health += value;

        if (_health > MaxHealth)
            _health = MaxHealth;

        Changed?.Invoke();
    }

    public void Reset()
    {
        _health = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        _health -= damage;
        Changed?.Invoke();

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        Died?.Invoke();
    }
}
