using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    private float _currentHealth;
    private float _maxHealth;

    public event Action OnDeath; // Event to notify when the enemy dies

    public void Initialize(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
    }

    public void TakeDamage(DamageAttribute damageAttribute)
    {
        _currentHealth -= damageAttribute.DamageAmount;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnDeath?.Invoke(); // Trigger the OnDeath event
        }
    }
}
