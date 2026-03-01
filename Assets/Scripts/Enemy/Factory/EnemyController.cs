using UnityEngine;

public class EnemyController : MonoBehaviour, IPoolable
{
    private GameObjectPool<EnemyController> _enemyPool;
    private EnemyHealth _enemyHealth;

    [SerializeField] private GameObject _deathEffectPrefab;

    public void Initialize(EnemyHealth enemyHealth)
    {
        _enemyHealth = enemyHealth;
        _enemyHealth.OnDeath += OnDespawn; // Subscribe to the OnDeath event
    }

    public void SetPool(GameObjectPool<EnemyController> pool)
    {
        _enemyPool = pool;
    }

    public void OnSpawn()
    {
        // Initialize enemy state when spawned
        gameObject.SetActive(true);
    }

    public void OnDespawn()
    {
        // Clean up enemy state when despawned
        gameObject.SetActive(false);
        StopAllCoroutines(); // Stop any ongoing coroutines (Player detection)
        _enemyPool.Return(this);
    }
}