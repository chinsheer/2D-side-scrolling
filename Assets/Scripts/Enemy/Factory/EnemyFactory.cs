using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyFactory : ScriptableObject
{
    [SerializeField] protected GameObject _enemyPrefab;

    protected GameObjectPool<EnemyController> _enemyPool;

    public abstract EnemyController SpawnEnemy(SpawnContext context);

    public void InitializePool(int initialPoolSize = 10)
    {
        _enemyPool = new GameObjectPool<EnemyController>(_enemyPrefab.GetComponent<EnemyController>(), initialPoolSize);
    }
}

public struct SpawnContext
{
    public Vector3 SpawnPosition;
    public Quaternion SpawnRotation;
}