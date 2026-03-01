using UnityEngine;

public class Chest : MonoBehaviour
{
    private Inventory _chestInventory;

    public Inventory ChestInventory => _chestInventory;

    private void Awake()
    {
        _chestInventory = GetComponent<Inventory>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponentInChildren<PageController>(true)?.BindChestInventory(_chestInventory);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponentInChildren<PageController>(true)?.UnbindChestInventory();
    }
}