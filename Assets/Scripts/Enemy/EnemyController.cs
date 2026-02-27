using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private IMoveInputSource _enemyInput;

    private bool _isGrounded = false;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _jumpForce = 4f;
    [SerializeField] private GameObject _deathEffectPrefab;

    private EnemyHealth _enemyHealth;

    public float MoveSpeed { get => _moveSpeed; }
    public float JumpForce { get => _jumpForce; }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _enemyInput = GetComponent<IMoveInputSource>();
        _enemyHealth = GetComponentInChildren<EnemyHealth>();
        if (_enemyHealth != null)
        {
            _enemyHealth.OnDeath += Die; // Subscribe to the OnDeath event
        }
    }

    void FixedUpdate()
    {
        Vector2 moveDirection = _enemyInput.MoveDirection;
        _rigidbody2D.velocity = new Vector2(moveDirection.x * MoveSpeed, _rigidbody2D.velocity.y);

        if (_enemyInput.Jump && _isGrounded)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, JumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isGrounded = false;
        }
    }

    public void Die()
    {
        //Hardcoding the slime's death
        if (_deathEffectPrefab != null)
        {
            Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
