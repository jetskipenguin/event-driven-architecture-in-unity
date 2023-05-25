using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    [SerializeField] private AudioSource _prefab;

    [SerializeField] private int _initialSize = 10;

    private readonly List<AudioSource> _pool = new List<AudioSource>();
    private GameObject _parentObject;

    private void OnEnable()
    {
        _parentObject = Object.Instantiate(new GameObject("AudioSourcePool"), gameObject.transform);
        
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