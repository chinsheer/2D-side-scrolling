using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Item/Tool/Wand")]
public class WandItem : ItemData, IUsableItem, IDamageableItem, IAimableItem
{
    public DamageAttribute Damage;
    public float HealthCost; // Amount of health consumed when using the wand
    public float Speed;
    public GameObject ProjectilePrefab; // Reference to the projectile prefab for the wand

    private void OnEnable()
    {
        _type = ItemType.Tools; // Set the item type to Tools for weapons
    }

    public UseResult Use(UseContext context)
    {
        // Instantiate the projectile
        GameObject projectile = Instantiate(ProjectilePrefab, context.HandPosition, Quaternion.identity);
        // Set the projectile's direction and speed
        if (projectile.TryGetComponent<IProjectile>(out var projectileComponent))
        {
            Vector2 shootDirection = context.HandPosition - (Vector2)context.User.transform.position; 
            projectileComponent.Initialize(shootDirection.normalized * Speed);
        }
        if (projectile.TryGetComponent<IDamageConfigurable>(out var damageComponent))
        {
            damageComponent.ConfigureDamage(Damage);
        }
        if (context.playerHealth != null)
        {
            context.playerHealth.TakeDamage(new DamageAttribute { DamageAmount = HealthCost });
        }
        return new UseResult { Success = true, consumedQuantity = 0 };
    }

    public DamageAttribute GetDamage()
    {
        return Damage;
    }

    public void Aim(Vector2 direction)
    {
        // Implement aiming logic for the wand
    }
}