using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly List<T> _pool = new List<T>();

    public ObjectPool(T prefab, Transform parent)
    {
        _prefab = prefab;
        _parent = parent;
    }

    public T Get()
    {
        T obj;
        if (_pool.Count > 0)
        {
            obj = _pool[_pool.Count - 1];
            _pool.RemoveAt(_pool.Count - 1);
        }
        else
        {
            obj = Object.Instantiate(_prefab, _parent);
        }
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Add(obj);
    }
}