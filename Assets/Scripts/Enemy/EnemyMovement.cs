using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float _speed;
    private Rigidbody2D _rigidbody2D;
    private IMoveInputSource _moveInputSource;
    private bool _isGrounded;

    public void Initialize(IMoveInputSource moveInputSource, float speed)
    {
        _speed = speed;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _moveInputSource = moveInputSource;
    }

    public void Update()
    {
        if (_rigidbody2D != null && _moveInputSource != null)
        {
            _rigidbody2D.velocity = new Vector2(_moveInputSource.MoveDirection.x * _speed, _rigidbody2D.velocity.y);
        }
        if (_moveInputSource.Jump && _isGrounded)
        {
            _rigidbody2D.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse); 
            _isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)  
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0); // Reset vertical velocity when landing
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