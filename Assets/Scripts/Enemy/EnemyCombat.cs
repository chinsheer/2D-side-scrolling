using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int damage = 10; // Example damage value, can be adjusted or made dynamic
    public void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagable = collision.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(damage); // Example damage value, can be adjusted or made dynamic
        }
    }
}
