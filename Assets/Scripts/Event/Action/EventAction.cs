using UnityEngine;

public abstract class EventAction : ScriptableObject
{
    public abstract void Execute(EventContext context);
}

public struct EventContext
{
    public GameObject investigator;   // who triggered it (player)
    public Inventory[] PlayerInventory; // player's inventory, for convenience
}