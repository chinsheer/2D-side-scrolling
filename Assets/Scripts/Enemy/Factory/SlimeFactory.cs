using UnityEngine;

[CreateAssetMenu(fileName = "SlimeFactory", menuName = "Enemy/SlimeFactory")]
public class SlimeFactory : EnemyFactory
{
    [SerializeField] private int _initialPoolSize = 10;
    [SerializeField] private int _slimeHealth = 20;
    [SerializeField] private DamageAttribute _slimeDamage;

    public event System.Action<Vector2> OnSlimeDeath;

    public override EnemyController SpawnEnemy(SpawnContext context)
    {
        EnemyController enemyController;
        if(_enemyPool == null)
        {
            InitializePool(_initialPoolSize);
            enemyController = _enemyPool.Get();
        }
        else
        {
            enemyController = _enemyPool.Get();
        }
        GameObject slimeObject = enemyController.gameObject;
        slimeObject.transform.position = context.SpawnPosition;
        slimeObject.transform.rotation = context.SpawnRotation;
        enemyController.OnSpawn();

        //config
        EnemyHealth enemyHealth = slimeObject.GetComponentInChildren<EnemyHealth>();
        IDamageConfigurable damageConfigurable = slimeObject.GetComponentInChildren<IDamageConfigurable>();
        SlimeAI enemyAI = slimeObject.GetComponent<SlimeAI>();
        EnemyMovement enemyMovement = slimeObject.GetComponent<EnemyMovement>();

        enemyAI.Initialize();
        damageConfigurable.ConfigureDamage(_slimeDamage);
        enemyHealth.Initialize(_slimeHealth);
        enemyMovement.Initialize(enemyAI, 2f); // Initialize with AI and speed
        enemyController.Initialize(enemyHealth);
        enemyController.SetPool(_enemyPool);
        slimeObject.SetActive(true);

        enemyHealth.OnDeath += () => OnSlimeDeath?.Invoke(slimeObject.transform.position);

        return enemyController;
    }
}