using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IProjectile, IDamageSource, IDamageConfigurable
{
    private Rigidbody2D _rb;
    private DamageAttribute _damage;
    public DamageAttribute Damage => _damage;


    // Awake is called when the script instance is being loaded
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 initialVelocity)
    {
        _rb.velocity = initialVelocity; // Adjust speed as needed
    }

    public void ConfigureDamage(DamageAttribute damageAttribute)
    {
        _damage = damageAttribute;
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
