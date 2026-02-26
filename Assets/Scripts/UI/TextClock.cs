using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextClock : MonoBehaviour
{
    private ClockController _clockController;
    [SerializeField] private TextMeshProUGUI _weekDayTextMesh;
    [SerializeField] private TextMeshProUGUI _dayTextMesh;


    void Awake()
    {
        _clockController = GetComponentInParent<ClockController>();
        if (_clockController != null)
        {
            _clockController.OnClockUpdate += UpdateClockText;
        }
    }

    void UpdateClockText(IWorldTime worldTime)
    {
        _weekDayTextMesh.text = worldTime.CurrentWeekDay.ToString();
        _dayTextMesh.text = "DAY " + (worldTime.CurrentDay + 1).ToString();
    }
}
