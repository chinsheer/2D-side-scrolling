using UnityEngine;
using UnityEngine.UI;

public class RingClockUI : MonoBehaviour
{
    private Image ClockHand; // For ring clock
    private float fillAmount;
    private bool isUpdateNeeded = false;
    private ClockController _clockController;

    void Awake()
    {
        _clockController = GetComponentInParent<ClockController>();
        if (_clockController != null)
        {
            _clockController.OnClockUpdate += UpdateFillAmount;
        }
        ClockHand = GetComponent<Image>();
    }

    void UpdateFillAmount(IWorldTime worldTime)
    {
        fillAmount = worldTime.CurrentHour / 24f;
        ClockHand.fillAmount = fillAmount;
        isUpdateNeeded = true;
    }

    void LateUpdate()
    {
        if (isUpdateNeeded)
        {
            ClockHand.fillAmount = fillAmount;
            isUpdateNeeded = false;
        }
    }
}