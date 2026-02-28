using UnityEngine;

public class PageController : MonoBehaviour
{
    private GameObject _itemPageMarker;
    private GameObject _craftingPageMarker;
    private IUIPageController _itemPageController;
    private IUIPageController _craftingPageController;

    private void Awake()
    {
        _itemPageMarker = transform.Find("ItemPageMarker").gameObject;
        _craftingPageMarker = transform.Find("CraftingPageMarker").gameObject;

        var itemPage = transform.Find("InventoryPage").gameObject;
        var craftingPage = transform.Find("CraftingPage").gameObject;

        _itemPageController = itemPage.GetComponent<IUIPageController>();
        _craftingPageController = craftingPage.GetComponent<IUIPageController>();

        // Open item page by default
        OpenItemPage();
    }

    public void OpenItemPage()
    {
        _itemPageController.Open();
        _itemPageMarker.GetComponent<RectTransform>().SetAsLastSibling();
        _craftingPageController.Close();
        _craftingPageMarker.GetComponent<RectTransform>().SetAsFirstSibling();
    }

    public void OpenCraftingPage()
    {
        _craftingPageController.Open();
        _craftingPageMarker.GetComponent<RectTransform>().SetAsLastSibling();
        _itemPageController.Close();
        _itemPageMarker.GetComponent<RectTransform>().SetAsFirstSibling();
    }
}