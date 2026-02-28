using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftPreviewSlotUI : MonoBehaviour
{
    public struct CraftPreviewSlotData
    {
        public int OwnedAmount;
        public int RequiredAmount;
        public Sprite ItemIcon;
    }

    public CraftPreviewSlotData Data { get; private set; }
    public int Index { get; private set; }

    public void SetItem(CraftPreviewSlotData data, int index)
    {
        Data = data;
        Index = index;
        var itemIconImage = transform.Find("Icon").GetComponent<UnityEngine.UI.Image>();
        var quantityText = transform.Find("Required").GetComponent<TMPro.TextMeshProUGUI>();

        itemIconImage.sprite = data.ItemIcon;
        itemIconImage.enabled = true;

        quantityText.text = $"{data.OwnedAmount}/{data.RequiredAmount}";
        quantityText.enabled = true;
    }
}
