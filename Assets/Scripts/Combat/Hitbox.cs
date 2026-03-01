using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagable = collision.GetComponent<IDamagable>();
        if (damagable != null)
        {
            if(GetComponentInParent<IDamageSource>() is IDamageSource damageSource)
            {
                if (damageSource == null) return;
                damagable.TakeDamage(damageSource.Damage);
            }
        }
    }
}   