using UnityEngine;

[CreateAssetMenu(fileName = "ShootAction", menuName = "Inventory/UseActions/Shoot")]
public class Shoot : UseAction
{
    public GameObject ProjectilePrefab;
    public override void Use(UseContext context)
    {
        // Instantiate the projectile
        GameObject projectile = Instantiate(ProjectilePrefab, context.HandPosition, Quaternion.identity);
        // Set the projectile's direction and speed
        if (projectile.TryGetComponent<IProjectile>(out var projectileComponent))
        {
            Vector2 shootDirection = context.HandPosition - (Vector2)context.User.transform.position; // Calculate direction from user to hand position
            projectileComponent.Initialize(10, shootDirection * 10); // Example damage and speed
        }
    }
}