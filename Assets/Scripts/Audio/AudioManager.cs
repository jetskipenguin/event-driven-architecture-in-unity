using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Audio control")]
	[SerializeField] private AudioMixer audioMixer = default;
	[Range(0f, 1f)]
	[SerializeField] private float _masterVolume = 1f;
	[Range(0f, 1f)]

    private AudioSourcePool _audioSourcePool = default;

    private void Start()
    {
        _audioSourcePool = GetComponent<AudioSourcePool>();
    }

    private void ChangeMasterVolume(float newVolume)
    {
        _masterVolume = newVolume;
        SetGroupVolume("MasterVolume", _masterVolume);
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        var audioSource = _audioSourcePool.Get();
        audioSource.clip = clip;
        audioSource.volume = volume * _masterVolume;
        audioSource.Play();
        StartCoroutine(ReturnAudioSource(audioSource));
    }

    private IEnumerator ReturnAudioSource(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        _audioSourcePool.Return(audioSource);
    }

    public void SetGroupVolume(string parameterName, float normalizedVolume)
	{
		bool volumeSet = audioMixer.SetFloat(parameterName, NormalizedToMixerValue(normalizedVolume));
		if (!volumeSet)
			Debug.LogError("The AudioMixer parameter was not found");
	}

    private float NormalizedToMixerValue(float normalizedValue)
	{
		// We're assuming the range [0 to 1] becomes [-80dB to 0dB]
		// This doesn't allow values over 0dB
		return (normalizedValue - 1f) * 80f;
	}
}