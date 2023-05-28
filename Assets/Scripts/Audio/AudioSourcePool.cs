using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    [SerializeField] internal AudioSource _prefab;

    [SerializeField] internal int _initialSize = 10;

    internal List<AudioSource> _pool = new List<AudioSource>();
    internal Dictionary<int, AudioSource> _activeSources = new Dictionary<int, AudioSource>();
    internal int _nextUniqueID = 0;
    internal GameObject _parentObject;

    private void OnEnable()
    {
        Prewarm();
    }

    private void OnDisable()
    {
        _pool.Clear();
        _activeSources.Clear();
    }

    internal void Prewarm()
    {
        _parentObject = new GameObject("Audio Sources");

        for (int i = 0; i < _initialSize; i++)
        {
            AudioSource obj = Object.Instantiate(_prefab, _parentObject.transform);
            obj.gameObject.SetActive(false);
            _pool.Add(obj);
        }
    }

    public (AudioSource, int) Get()
    {
        AudioSource obj;

        if(_pool.Count > 0)
        {
            obj = _pool[_pool.Count - 1];
            _activeSources[++_nextUniqueID] = obj;
            _pool.RemoveAt(_pool.Count - 1);
        }
        else
        {
            obj = Object.Instantiate(_prefab, _parentObject.transform);
            _activeSources[++_nextUniqueID] = obj;
        }

        obj.gameObject.SetActive(true);
        return (obj, _nextUniqueID);
    }

    public void Return(int id)
    {
        if (_activeSources.Count > _initialSize)
        {
            DestroyImmediate(_activeSources[id].gameObject);
            _activeSources.Remove(id);
            return;
        }
        
        _activeSources[id].gameObject.SetActive(false);
        _pool.Add(_activeSources[id]);
        _activeSources.Remove(id);
    }
}