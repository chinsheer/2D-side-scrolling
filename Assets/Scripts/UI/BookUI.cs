using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookUI : MonoBehaviour
{
    public void ToggleVisibility()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
        {
            // This is hardcoded for simplicity. fixed later
            FindObjectOfType<InventoryUI>().RefreshUI();
        }
    }
}
