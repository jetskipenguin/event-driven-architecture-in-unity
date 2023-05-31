using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSourcePool", menuName = "ScriptableObjects/Audio/Audio Source Pool")]
public class AudioSourcePoolSO : ScriptableObject, IAudioSourcePoolSO
{
    [SerializeField] private AudioSource _prefab;
    [SerializeField] private int _initialSize = 10;

    private AudioSource[] _pool;
    private int _currentIndex;
    private GameObject _parentObject;

    public void Initialize()
    {
        _pool = new AudioSource[_initialSize];
        _currentIndex = _initialSize - 1;

        if (_parentObject == null)
            _parentObject = new GameObject("Audio Sources");

        for (int i = 0; i < _initialSize; i++)
        {
            _pool[i] = Object.Instantiate(_prefab, _parentObject.transform);
            _pool[i].gameObject.SetActive(false);
        }
    }

    public AudioSource Get()
    {
        AudioSource obj;
        if (_currentIndex >= 0)
        {
            obj = _pool[_currentIndex];
            _currentIndex--;
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
        _currentIndex++;
        _pool[_currentIndex] = obj;
    }
}
