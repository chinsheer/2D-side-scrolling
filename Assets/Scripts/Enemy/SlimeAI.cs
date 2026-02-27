using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour, IMoveInputSource
{
    private Transform _playerTransform;
    private Vector2 _moveDirection;

    public Vector2 MoveDirection { get => _moveDirection; }
    public bool Jump { get => false; }

    public float DetectionRange = 5f;
    public Vector2 TimeBetweenDirectionChanges = new Vector2(1f, 3f);

    private float _durationUntilNextDirectionChange;
    private Vector2 _lastRandomDirection;

    void Awake()
    {
        _playerTransform = GameObject.Find("Player").transform;
        _durationUntilNextDirectionChange = NextDirectionChangeTime();
        _lastRandomDirection = Vector2.right; // Initial random direction
    }

    void Update()
    {
        _durationUntilNextDirectionChange -= Time.deltaTime;
        if (Vector2.Distance(transform.position, _playerTransform.position) < DetectionRange)
        {
            _moveDirection = new Vector2((_playerTransform.position - transform.position).normalized.x, 0f);
        }
        else
        {
            if (_durationUntilNextDirectionChange <= 0f)
            {
                _moveDirection = -_lastRandomDirection; // Change direction
                _lastRandomDirection = _moveDirection;
                _durationUntilNextDirectionChange = NextDirectionChangeTime();
            }
        }
    }

    float NextDirectionChangeTime()
    {
        return Random.Range(TimeBetweenDirectionChanges.x, TimeBetweenDirectionChanges.y);
    }
}
