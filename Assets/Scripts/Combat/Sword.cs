using UnityEngine;

public class Sword : MonoBehaviour, ISwing, IDamageSource, IDamageConfigurable
{
    private Transform _center;
    private float _radius;
    private float _startAngle;
    private float _endAngle;
    private float _duration;
    private float _elapsedTime;

    private DamageAttribute _damage;
    private Quaternion _initialRotation;

    public DamageAttribute Damage => _damage;

    public void Initialize(Transform center, float radius, float startAngle, float endAngle, float duration)
    {
        _center = center;
        _radius = radius;
        _startAngle = startAngle; // Prefabs 0 degrees is facing up right
        _endAngle = endAngle; // Prefabs 0 degrees is facing up right
        _duration = duration;
        _elapsedTime = 0f;

    }

    public void ConfigureDamage(DamageAttribute damageAttribute)
    {
        _damage = damageAttribute;
    }

    void Update()
    {
        if (_elapsedTime < _duration)
        {
            _elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(_elapsedTime / _duration);
            float currentAngle = Mathf.Lerp(_startAngle, _endAngle, t);
            Vector2 offset = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * _radius;
            transform.position = _center.position + (Vector3)offset;

            gameObject.transform.rotation = Quaternion.Euler(0, 0, currentAngle - 45f); // 0 degrees is facing up right, adjust as needed based on your sprite orientation
        }
        else
        {
            Destroy(gameObject); // Destroy the sword after the swing is complete
        }
    }
}