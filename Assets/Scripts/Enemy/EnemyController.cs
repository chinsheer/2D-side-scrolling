using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private IMoveInputSource _enemyInput;

    private bool _isGrounded = false;
    [SerializeField] public float _moveSpeed = 3f;
    [SerializeField] public float _jumpForce = 4f;

    public float MoveSpeed { get => _moveSpeed; }
    public float JumpForce { get => _jumpForce; }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _enemyInput = GetComponent<IMoveInputSource>();
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
}
