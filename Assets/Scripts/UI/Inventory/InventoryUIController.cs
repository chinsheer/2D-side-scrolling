using UnityEngine;
public class InventoryUIController : MonoBehaviour, IUIPageController
{
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void RefreshUI()
    {
        // No dynamic content to refresh in inventory page for now
    }
}
