using UnityEngine;

[CreateAssetMenu(fileName = "SlimeFactory", menuName = "Enemy/SlimeFactory")]
public class SlimeFactory : EnemyFactory
{
    [SerializeField] private int _initialPoolSize = 10;
    [SerializeField] private int _slimeHealth = 20;
    [SerializeField] private DamageAttribute _slimeDamage;

    public override EnemyController SpawnEnemy(SpawnContext context)
    {
        GameObject slimeObject = Instantiate(_enemyPrefab, context.SpawnPosition, context.SpawnRotation);
        EnemyController enemyController = slimeObject.GetComponent<EnemyController>();
        enemyController.OnSpawn();

        //config
        EnemyHealth enemyHealth = slimeObject.GetComponentInChildren<EnemyHealth>();
        IDamageConfigurable damageConfigurable = slimeObject.GetComponentInChildren<IDamageConfigurable>();
        SlimeAI enemyAI = slimeObject.GetComponent<SlimeAI>();
        EnemyMovement enemyMovement = slimeObject.GetComponent<EnemyMovement>();

        enemyAI.Initialize();
        damageConfigurable.ConfigureDamage(_slimeDamage);
        enemyHealth.OnDeath += enemyController.OnDespawn; // Subscribe to the OnDeath event
        enemyHealth.Initialize(_slimeHealth);
        enemyMovement.Initialize(enemyAI, 2f); // Initialize with AI and speed
        enemyController.Initialize(enemyHealth);
        enemyController.SetPool(_enemyPool);
        return enemyController;
    }
}