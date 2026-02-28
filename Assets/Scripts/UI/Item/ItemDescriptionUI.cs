using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescriptionUI : MonoBehaviour
{
    public ItemData ItemData { get; set; }
    
    private UnityEngine.UI.Image _itemIconImage;
    private TMPro.TextMeshProUGUI _itemNameText;
    private TMPro.TextMeshProUGUI _itemDescriptionText;

    public void Awake()
    {
        _itemIconImage = transform.Find("ItemData/Frame/Icon").GetComponent<UnityEngine.UI.Image>();
        _itemNameText = transform.Find("ItemData/Data/ItemName").GetComponent<TMPro.TextMeshProUGUI>();
        _itemDescriptionText = transform.Find("ItemDescription/Description").GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void RefreshUI()
    {
        if (ItemData == null) return;

        _itemIconImage.sprite = ItemData.ItemIcon;
        _itemNameText.text = ItemData.ItemName;
        _itemDescriptionText.text = ItemData.ItemDescription;
    }

    public void OnEnable()
    {
        _itemIconImage = transform.Find("ItemData/Frame/Icon").GetComponent<UnityEngine.UI.Image>();
        _itemNameText = transform.Find("ItemData/Data/ItemName").GetComponent<TMPro.TextMeshProUGUI>();
        _itemDescriptionText = transform.Find("ItemDescription/Description").GetComponent<TMPro.TextMeshProUGUI>();      
    }
}
