using UnityEngine;

public class WorldEnemySpawner : MonoBehaviour
{
    [SerializeField] private SlimeFactory _enemyFactory;

    //Special mini slime spawner
    [SerializeField] private SlimeFactory _miniSlimeFactory;

    private void Start()
    {
        _enemyFactory.InitializePool(10);
        _miniSlimeFactory.InitializePool(20);
        _enemyFactory.OnSlimeDeath += SpawnMiniSlime; // Subscribe to the slime death event to spawn mini slimes
        
        WorldTime.Instance.OnDayChanged += SpawnEnemy;
    }

    public void SpawnEnemy(int day)
    {
        _enemyFactory.SpawnEnemy(new SpawnContext{
            SpawnPosition = new Vector3(Random.Range(-10f, 10f), 0, 0), // Random spawn position for demonstration
            SpawnRotation = Quaternion.identity
        });
    }
    private void OnDisable()
    {
        if (WorldTime.Instance != null) WorldTime.Instance.OnDayChanged -= SpawnEnemy;
    }

    public void SpawnMiniSlime(Vector2 position)
    {
        if (_miniSlimeFactory != null)
        {
            for (int i = 0; i < 3; i++)
            _miniSlimeFactory.SpawnEnemy(new SpawnContext
            {
                SpawnPosition = position,
                SpawnRotation = Quaternion.identity
            });
        }
    }
}