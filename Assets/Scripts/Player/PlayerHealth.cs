using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int maxHealth = 100;
    private float currentHealth;

    public event Action OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        OnHealthChanged?.Invoke();
        Debug.Log($"Player healed by {amount}, current health: {currentHealth}");
    }

    public void TakeDamage(DamageAttribute damage)
    {
        currentHealth -= damage.DamageAmount;
        OnHealthChanged?.Invoke();
        Debug.Log($"Player took {damage.DamageAmount} damage, current health: {currentHealth}");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle player death (e.g., respawn, game over)
    }
}
