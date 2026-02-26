using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private IMoveInputSource _playerInput;

    private bool _isGrounded = false;

    [SerializeField] public float _moveSpeed = 5f;
    [SerializeField] public float _jumpForce = 5f;

    public float MoveSpeed { get => _moveSpeed; }
    public float JumpForce { get => _jumpForce; }
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<IMoveInputSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector2 moveDirection = _playerInput.MoveDirection;
        _rigidbody2D.velocity = new Vector2(moveDirection.x * MoveSpeed, _rigidbody2D.velocity.y);

        if (_playerInput.Jump && _isGrounded)
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
