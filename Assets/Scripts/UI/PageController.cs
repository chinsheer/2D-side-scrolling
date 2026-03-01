using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageController : MonoBehaviour
{
    private GameObject _itemPageMarker;
    private GameObject _craftingPageMarker;
    private GameObject _chestPageMarker;
    private IUIPageController _itemPageController;
    private IUIPageController _craftingPageController;
    private IUIPageController _chestPageController;

    private CraftingUIController _craftingUIController;
    private ChestUIController _chestUIController;

    private List<IUIPageController> _pageControllers = new List<IUIPageController>();
    private List<GameObject> _pageMarkers = new List<GameObject>();

    public enum PageType
    {
        Item,
        Crafting,
        Chest
    }

    private void Start()
    {
        _itemPageMarker = transform.Find("ItemPageMarker").gameObject;
        _craftingPageMarker = transform.Find("CraftingPageMarker").gameObject;
        _chestPageMarker = transform.Find("ChestPageMarker").gameObject;
        _itemPageMarker.GetComponent<Button>().onClick.AddListener(() => OpenPage(PageType.Item));
        _craftingPageMarker.GetComponent<Button>().onClick.AddListener(() => OpenPage(PageType.Crafting));
        _chestPageMarker.GetComponent<Button>().onClick.AddListener(() => OpenPage(PageType.Chest));
        _pageMarkers.Add(_itemPageMarker);
        _pageMarkers.Add(_craftingPageMarker);
        _pageMarkers.Add(_chestPageMarker);

        var itemPage = transform.Find("InventoryPage").gameObject;
        var craftingPage = transform.Find("CraftingPage").gameObject;
        var chestPage = transform.Find("ChestPage").gameObject;

        _itemPageController = itemPage.GetComponent<IUIPageController>();
        _craftingPageController = craftingPage.GetComponent<IUIPageController>();
        _chestPageController = chestPage.GetComponent<IUIPageController>();
        _itemPageController.Initialize();
        _craftingPageController.Initialize();
        _chestPageController.Initialize();
        _pageControllers.Add(_itemPageController);
        _pageControllers.Add(_craftingPageController);
        _pageControllers.Add(_chestPageController);


        _craftingUIController = craftingPage.GetComponent<CraftingUIController>();
        _chestUIController = chestPage.GetComponent<ChestUIController>();

        // Open item page by default
        OpenPage(PageType.Item);
    }

    public void OpenPage(PageType pageType)
    {
        for(int i = 0; i < _pageControllers.Count; i++)
        {
            if (i == (int)pageType)
            {
                _pageControllers[i].Open();
                _pageControllers[i].RefreshUI();
                _pageMarkers[i].transform.SetAsLastSibling(); // Move the marker to the end to ensure it's on top
                continue;
            }
            _pageControllers[i].Close();
            _pageMarkers[i].transform.SetAsFirstSibling(); // Move the marker to the beginning to ensure it's behind other markers
        }
    }

    public void BindCraftingProvider(ICraftingProvider craftingProvider)
    {
        _craftingUIController.Bind(craftingProvider);
    }

    public void UnbindCraftingProvider(ICraftingProvider craftingProvider)
    {
        _craftingUIController.Unbind(craftingProvider);
    }

    public void BindChestInventory(Inventory chestInventory)
    {
        _chestUIController.Bind(chestInventory);
    }

    public void UnbindChestInventory()
    {
        _chestUIController.Bind(null);
    }
}