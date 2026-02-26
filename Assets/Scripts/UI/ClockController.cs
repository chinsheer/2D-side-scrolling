using System;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    [SerializeField] private float UpdateInterval = 1f; // Update every in-game hour
    private IWorldTime _worldTime;
    private float _timeSinceLastUpdate = 0f;

    public event Action<IWorldTime> OnClockUpdate; // passes the current time
    void Start()
    {
        _worldTime = WorldTime.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastUpdate += Time.deltaTime;
        if (_worldTime != null)
        {
            if (_timeSinceLastUpdate >= UpdateInterval)
            {
                _timeSinceLastUpdate = 0f;
                OnClockUpdate?.Invoke(_worldTime);
            }
        }
    }
}
