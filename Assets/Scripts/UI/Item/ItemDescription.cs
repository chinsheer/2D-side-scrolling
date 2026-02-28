using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    public ItemData ItemData { get; set; }
    
    private UnityEngine.UI.Image _itemIconImage;
    private TMPro.TextMeshProUGUI _itemNameText;
    private TMPro.TextMeshProUGUI _itemDescriptionText;

    private void Awake()
    {
        _itemIconImage = transform.Find("ItemData/Frame/Icon").GetComponent<UnityEngine.UI.Image>();
        _itemNameText = transform.Find("ItemData/Data/ItemName").GetComponent<TMPro.TextMeshProUGUI>();
        _itemDescriptionText = transform.Find("ItemDescription/Description").GetComponent<TMPro.TextMeshProUGUI>();

        _itemIconImage.enabled = false;
        _itemNameText.enabled = false;
        _itemDescriptionText.enabled = false;
    }

    public void RefreshUI()
    {
        if (ItemData == null)
        {
            _itemIconImage.enabled = false;
            _itemNameText.enabled = false;
            _itemDescriptionText.enabled = false;
            return;
        }

        _itemIconImage.sprite = ItemData.ItemIcon;
        _itemNameText.text = ItemData.ItemName;
        _itemDescriptionText.text = ItemData.ItemDescription;

        _itemIconImage.enabled = true;
        _itemNameText.enabled = true;
        _itemDescriptionText.enabled = true;
    }
}
