using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Item/Tool/Sword")]

public class SwordItem : ItemData, IUsableItem, IDamageableItem
{
    public DamageAttribute Damage;
    public GameObject SwingEffectPrefab; // Reference to the swing effect prefab for the sword

    private void OnEnable()
    {
        _type = ItemType.Tools; // Set the item type to Tools for weapons
    }

    public UseResult Use(UseContext context)
    {
        Vector3 SpawnPosition = context.User.transform.position + new Vector3(0.2f, 0.2f, 0);
        GameObject swingEffect = Instantiate(SwingEffectPrefab, SpawnPosition, Quaternion.identity);

        if (swingEffect.TryGetComponent<ISwing>(out var swingComponent))
        {
            swingComponent.Initialize(context.User.transform, 1f, 90f, -90f, 0.5f); // Example parameters for the swing
        }
        if (swingEffect.TryGetComponent<IDamageConfigurable>(out var damageComponent))
        {
            damageComponent.ConfigureDamage(Damage);
        }

        return new UseResult { Success = true, consumedQuantity = 0 };
    }

    public DamageAttribute GetDamage()
    {
        return Damage;
    }
}