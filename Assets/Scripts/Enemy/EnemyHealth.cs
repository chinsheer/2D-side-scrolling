using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    public float maxHealth = 20;
    private float currentHealth;

    public event Action OnDeath; // Event to notify when the enemy dies

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(DamageAttribute damageAttribute)
    {
        currentHealth -= damageAttribute.DamageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke(); // Trigger the OnDeath event
    }
}
