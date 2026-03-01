using UnityEngine;
using System.Collections.Generic;

public class GameObjectPool<T> where T : Component
{
    private readonly Stack<T> _stack = new Stack<T>();
    private readonly T _prefab;

    public GameObjectPool(T prefab, int initialSize)
    {
        _prefab = prefab;
        for (int i = 0; i < initialSize; i++)
        {
            T obj = Object.Instantiate(prefab);
            obj.gameObject.SetActive(false);
            _stack.Push(obj);
        }
    }

    public T Get()
    {
        if (_stack.Count > 0)
        {
            T obj = _stack.Pop();
            obj.gameObject.SetActive(true);
            return obj;
        }
        // If the pool is empty, instantiate a new object
        return Object.Instantiate(_prefab);
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        _stack.Push(obj);
    }
}