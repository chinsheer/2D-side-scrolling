using UnityEngine;
public abstract class UseAction : ScriptableObject, IUsable
{
    public abstract void Use(UseContext context);
}