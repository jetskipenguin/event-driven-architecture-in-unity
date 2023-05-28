using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    [SerializeField] internal AudioSource _prefab;

    [SerializeField] internal int _initialSize = 10;

    internal readonly List<AudioSource> _pool = new List<AudioSource>();
    internal GameObject _parentObject;

    private void OnEnable()
    {
        _parentObject = new GameObject("Audio Sources");
        
        for (int i = 0; i < _initialSize; i++)
        {
            _pool.Add(Object.Instantiate(_prefab, _parentObject.transform));
            _pool[i].gameObject.SetActive(false);
        }
    }

    public AudioSource Get()
    {
        AudioSource obj;
        if (_pool.Count > 0)
        {
            obj = _pool[_pool.Count - 1];
            _pool.RemoveAt(_pool.Count - 1);
        }
        else
        {
            obj = Object.Instantiate(_prefab, _parentObject.transform);
        }
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Return(AudioSource obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Add(obj);
    }
}