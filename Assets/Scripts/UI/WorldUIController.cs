using UnityEngine;

public class WorldUIController : MonoBehaviour
{
    [SerializeField] private Inventory _hotbarInventory;
    [SerializeField] private InventoryUI _hotbarInventoryUI;
    [SerializeField] private HearthUIController _hearthUIController;

    [SerializeField] private PlayerHealth _playerHealth;

    public void Start()
    {
        _hotbarInventoryUI.Initialize(_hotbarInventory);
        _hearthUIController.Initialize(_playerHealth);
    }
}