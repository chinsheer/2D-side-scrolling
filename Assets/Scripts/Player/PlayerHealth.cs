using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int _maxHealth = 100;
    private float _currentHealth;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public event Action OnHealthChanged;
    public event Action<PlayerHealth> OnMaxHealthChanged;

    void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void Heal(float amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
        OnHealthChanged?.Invoke();
    }

    public void TakeDamage(DamageAttribute damage)
    {
        _currentHealth -= damage.DamageAmount;
        OnHealthChanged?.Invoke();
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle player death (e.g., respawn, game over)
    }
}
