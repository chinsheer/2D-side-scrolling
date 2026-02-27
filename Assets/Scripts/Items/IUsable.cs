using UnityEngine;

public interface IUsable
{
    void Use(UseContext context);
}

public struct UseContext
{
    public GameObject User;
    public Vector2 HandPosition;
    public Inventory inventory;
}