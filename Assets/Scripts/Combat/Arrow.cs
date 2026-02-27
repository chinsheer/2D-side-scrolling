using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IProjectile
{
    private int _damage;
    private Rigidbody2D _rb;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(int damage, Vector2 initialVelocity)
    {
        _damage = damage;
        _rb.velocity = initialVelocity; // Adjust speed as needed
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _rb.velocity); // Rotate the arrow to face the direction it's moving
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.TakeDamage(_damage); // Adjust damage as needed
        }
        Destroy(gameObject); // Destroy the arrow on impact
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
