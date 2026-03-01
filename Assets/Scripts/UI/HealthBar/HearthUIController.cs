using System.Collections.Generic;
using UnityEngine;

public class HearthUIController : MonoBehaviour
{
    [SerializeField] private int _maxHearths = 7; // Maximum number of hearths to display
    [SerializeField] private float _hpPerHearth = 20f; // Amount of health each hearth represents
    [SerializeField] private GameObject _hearthUIPrefab; // Prefab for the hearth UI element

    private PlayerHealth _playerHealth;
    private List<HearthUI> _hearthUIs = new List<HearthUI>(); // List to hold references to HearthUI components

    public void Initialize(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
        _playerHealth.OnHealthChanged += UpdateHealthBar; // Subscribe to health change events
        _playerHealth.OnMaxHealthChanged += Initialize; // Re-initialize if max health changes
        _hearthUIs.Clear();

        int maxHearthsNeeded = Mathf.CeilToInt(_playerHealth.MaxHealth / _hpPerHearth);
        int i = 0;

        for (; i < maxHearthsNeeded; i++)
        {
            if (transform.GetChild(i) != null)
            {
                _hearthUIs.Add(transform.GetChild(i).GetComponent<HearthUI>()); 
                continue; 
            }

            GameObject hearthUIObj = Instantiate(_hearthUIPrefab, transform);
            hearthUIObj.name = "HearthUI_" + (i + 1);
            HearthUI hearthUI = hearthUIObj.GetComponent<HearthUI>();
            _hearthUIs.Add(hearthUI);
        }

        for (; i < _maxHearths; i++)
        {
            if (transform.GetChild(i) != null)
            {
                transform.GetChild(i).gameObject.SetActive(false); // Deactivate extra hearths if max health is reduced
            }
        }

        UpdateHealthBar(); // Initial update of the health 
    }

    private void Start()
    {
        
    }

    private void UpdateHealthBar()
    {
        float currentHealth = _playerHealth.CurrentHealth;
        for (int i = 0; i < _hearthUIs.Count; i++)
        {
            float hearthHealth = (i + 1) * _hpPerHearth;
            _hearthUIs[i].SetHearth(currentHealth >= hearthHealth); // Set each hearth based on the current health
        }
    }

    private void OnDestroy()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnHealthChanged -= UpdateHealthBar; // Unsubscribe from health change events to prevent memory leaks
            _playerHealth.OnMaxHealthChanged -= Initialize; // Unsubscribe from max health change events to prevent memory leaks
        }
    }
}