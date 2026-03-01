using UnityEngine;

public class WorldEnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory[] _enemyFactory;

    private void Start()
    {
        for (int i = 0; i < _enemyFactory.Length; i++)
        {
            _enemyFactory[i].InitializePool(10); // Initialize each factory's pool with a size of 10
        }
        WorldTime.Instance.OnDayChanged += SpawnEnemy;
    }

    public void SpawnEnemy(int day)
    {
        _enemyFactory[Random.Range(0, _enemyFactory.Length)].SpawnEnemy(new SpawnContext{
            SpawnPosition = new Vector3(Random.Range(-10f, 10f), 0, 0), // Random spawn position for demonstration
            SpawnRotation = Quaternion.identity
        });
    }
    private void OnDisable()
    {
        if (WorldTime.Instance != null) WorldTime.Instance.OnDayChanged -= SpawnEnemy;
    }
}