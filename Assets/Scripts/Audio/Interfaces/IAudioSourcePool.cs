using UnityEngine;

public interface IAudioSourcePool
{
    AudioSource Get();
    void Return(AudioSource obj);
}