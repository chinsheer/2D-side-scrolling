using UnityEngine;

public class CraftItemUI : MonoBehaviour
{
    public Sprite NormalBackground;
    public Sprite SelectedBackground;

    private RecipeData _recipeData;

    public struct CraftItemData
    {
        public RecipeData RecipeData;
        public int CraftableQuantity;
        public bool IsSelected;
    }

    public CraftItemData Data { get; private set; }
    public int Index { get; private set; }
    public event System.Action<int> OnSelected;

    public void SetItem(CraftItemData data, int index)
    {
        Data = data;
        Index = index;
        _recipeData = data.RecipeData;
        var backgroundImage = GetComponent<UnityEngine.UI.Image>();
        var itemIconImage = transform.Find("Icon").GetComponent<UnityEngine.UI.Image>();
        var nameText = transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>();
        var quantityText = transform.Find("Quantity").GetComponent<TMPro.TextMeshProUGUI>();
        var button = GetComponent<UnityEngine.UI.Button>();

        itemIconImage.sprite = _recipeData.ResultItem.item.ItemIcon;
        nameText.text = _recipeData.ResultItem.item.ItemName;
        quantityText.text = data.CraftableQuantity.ToString();
        backgroundImage.sprite = data.IsSelected ? SelectedBackground : NormalBackground;
        button.onClick.AddListener(() => OnSelected?.Invoke(Index));

    }

    public void toggleSelection(bool isSelected)
    {
        var backgroundImage = GetComponent<UnityEngine.UI.Image>();
        backgroundImage.sprite = isSelected ? SelectedBackground : NormalBackground;
    }
}
