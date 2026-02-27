using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventContextProvider : MonoBehaviour
{
    public EventContext GetContext()
    {
        var context = new EventContext
        {
            investigator = gameObject,
            PlayerInventory = gameObject.GetComponentsInChildren<Inventory>()
        };
        return context;
    }
}
