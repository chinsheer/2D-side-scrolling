using UnityEngine;

public interface IUsableItem
{
    UseResult Use(UseContext context);
}

public struct UseContext
{
    public GameObject User;
    public Vector2 HandPosition;
    public Inventory inventory;
    public PlayerHealth playerHealth;

    public ItemData ItemData;
}

public struct UseResult
{
    public bool Success;
    public int consumedQuantity;
}