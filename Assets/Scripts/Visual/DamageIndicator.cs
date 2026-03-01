using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private float _floatUpSpeed = 1f;
    [SerializeField] private float _fadeOutSpeed = 0.5f;

    private float _elapsedTime;

    public void Initialize(float damageAmount)
    {
        _damageText.text = damageAmount.ToString();
        _elapsedTime = 0f;
    }

    public void Update()
    {
        _elapsedTime += Time.deltaTime;
        transform.position += Vector3.up * _floatUpSpeed * Time.deltaTime;

        Color currentColor = _damageText.color;
        currentColor.a = Mathf.Lerp(1f, 0f, _elapsedTime * _fadeOutSpeed);
        _damageText.color = currentColor;

        if (currentColor.a <= 0f)
        {
            Destroy(gameObject);
        }

    }
}
