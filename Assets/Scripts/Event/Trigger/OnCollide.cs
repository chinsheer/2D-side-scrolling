using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollide : MonoBehaviour
{
    public EventAction[] actions;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var act in actions)
        {
            if (collision.gameObject.TryGetComponent<EventContextProvider>(out var contextProvider))
            {
                act.Execute(contextProvider.GetContext());
            }
        }
    }
}
