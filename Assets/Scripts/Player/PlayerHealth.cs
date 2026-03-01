using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        IWorldTime worldTime = WorldTime.Instance;
        worldTime.SetTime(WorldTime.TimeOfDay.Morning); // Reset time to morning on death and add 1 day
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
