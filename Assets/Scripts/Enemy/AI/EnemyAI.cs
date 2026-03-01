using System.Collections;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour, IMoveInputSource
{
    protected Transform _target;

    protected Vector2 _moveDirection;

    public virtual void Initialize()
    {
        StartCoroutine(DetectPlayerRoutine(0.5f)); // Check for player every 0.5 seconds
    }

    // Allow changing target at runtime if needed
    public virtual void SetTarget(Transform target)
    {
        _target = target;
    }

    //Base AI zombie like
    public virtual void Update()
    {
        if (_target != null)
        {
            _moveDirection = (_target.position - transform.position).normalized;
        }
        else
        {
            _moveDirection = Vector2.zero;
        }
    }

    protected virtual IEnumerator DetectPlayerRoutine(float detectionInterval)
    {
        while (true)
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 5f, LayerMask.GetMask("PlayerDetector"));
            if (playerCollider != null)
            {
                _target = playerCollider.transform;
            }
            else
            {
                _target = null;
            }
            yield return new WaitForSeconds(detectionInterval);
        }
    }

    public virtual Vector2 MoveDirection => _moveDirection;
    public virtual bool Jump => false; // Enemies don't jump by default, can be overridden by specific enemy types
}