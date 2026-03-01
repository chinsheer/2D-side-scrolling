using UnityEngine;

public interface IPoolMember<T> where T : Component
{
    void SetPool(GameObjectPool<T> pool);
}