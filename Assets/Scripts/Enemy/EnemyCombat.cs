using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour, IDamageConfigurable
{
    private DamageAttribute damage = new DamageAttribute { DamageAmount = 10 }; // adjust as needed

    public void ConfigureDamage(DamageAttribute damageAttribute)
    {
        damage = damageAttribute;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagable = collision.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(damage);
        }
    }
}
