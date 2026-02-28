using UnityEngine;

[System.Serializable]
public struct DamageAttribute
{
    public float DamageAmount;

    public Vector2 KnockbackDirection;
    public float KnockbackStrength;

    public DamageAttribute(float damageAmount, Vector2 knockbackDirection, float knockbackStrength)
    {
        DamageAmount = damageAmount;
        KnockbackDirection = knockbackDirection;
        KnockbackStrength = knockbackStrength;
    }
}