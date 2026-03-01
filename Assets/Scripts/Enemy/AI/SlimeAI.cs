using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class SlimeAI : EnemyAI
{
    private bool _isAttacking;
    private Vector2 _lastMove;
    private float _timeUntilNextMove;
    private bool _jump;
    public override bool Jump { get => _jump;}

    public override void Initialize()
    {
        base.Initialize();
        _isAttacking = false;
        _lastMove = new Vector2[] { Vector2.right, Vector2.left }[Random.Range(0, 2)]; // Start moving in a random direction
        _timeUntilNextMove = Random.Range(1f, 3f); // Random time until next move change
        _moveDirection = _lastMove;
    }

    public override void Update()
    {
        if(_isAttacking) return;

        if (_target != null)
        {
            StartCoroutine(AttackRoutine(_target.position - transform.position));
        }
        else
        {
            _timeUntilNextMove -= Time.deltaTime;
            if (_timeUntilNextMove <= 0)
            {
                _lastMove = new Vector2(_lastMove.x * -1, 0);
                _moveDirection = _lastMove;
                _timeUntilNextMove = Random.Range(1f, 3f);
            }
        }
    }

    private IEnumerator AttackRoutine(Vector2 direction)
    {
        _isAttacking = true;
        _moveDirection = Vector2.zero; // Stop moving while attacking
        yield return new WaitForSeconds(0.5f);

        Vector2 attackDirection = direction.normalized;
        _moveDirection = new Vector2(attackDirection.x * 3f, 0);
        _jump = true; // Make the slime jump during the attack
        yield return new WaitForSeconds(0.1f);
        _jump = false; // Reset jump after the attack
        yield return new WaitForSeconds(1f); // Cooldown after attack
        _isAttacking = false;
    }
}
