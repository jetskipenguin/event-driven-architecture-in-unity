using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Audio control")]
	[SerializeField] private AudioMixer audioMixer = default;
    [Range(0f, 1f)]
    [SerializeField] private float _masterVolume = 1f;

    [Header("Listening on channels")]
    [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to change SFXs volume")]
	[SerializeField] private AudioCueEventChannelSO _SFXEventChannel = default;
	[Tooltip("The SoundManager listens to this event, fired by objects in any scene, to play Music")]
	[SerializeField] private AudioCueEventChannelSO _musicEventChannel = default;

    private Dictionary<int, List<AudioSource>> _activeAudioCues = new Dictionary<int, List<AudioSource>>();
    private int _nextUniqueID = 0;
    
    private List<AudioCueEventChannelSO> _audioChannels = new List<AudioCueEventChannelSO>();
    private AudioSourcePool _audioSourcePool = default;

    void Awake()
    {
        _audioSourcePool = GetComponent<AudioSourcePool>();
        SetGroupVolume("MasterVolume", _masterVolume);

        _audioChannels.AddRange(new[]  { _SFXEventChannel, _musicEventChannel});
    }

    private void OnEnable()
    {
        _audioChannels.ForEach(d => d.OnAudioCuePlayRequested += PlayAudioCue);
        _audioChannels.ForEach(d => d.OnAudioCueStopRequested += StopAudioCue);
    }

    private void OnDestroy()
    {
        _audioChannels.ForEach(d => d.OnAudioCuePlayRequested -= PlayAudioCue);
        _audioChannels.ForEach(d => d.OnAudioCueStopRequested -= StopAudioCue);
    }

    public int PlayAudioCue(AudioCueSO audioCue, AudioConfigurationSO settings, Vector3 position = default)
	{
		List<AudioClip> clipsToPlay = audioCue.GetClips().ToList();
        List<AudioSource> sources = clipsToPlay.Select(clip => SetupAudioSource(position, clip, audioCue.looping, settings)).ToList();

        _activeAudioCues.Add(++_nextUniqueID, sources);

		return _nextUniqueID;
	}

    private AudioSource SetupAudioSource(Vector3 position, AudioClip clip, bool isLooping, AudioConfigurationSO settings)
    {
        AudioSource source = _audioSourcePool.Get();
        if (!source)
        {
            Debug.LogError("No audio source available, issue in AudioSourcePool");
            return null;
        }

        source.transform.position = position;
        settings.ApplyTo(source);
        source.clip = clip;
        source.loop = isLooping;
        source.Play();

        return source;
    }

    public bool StopAudioCue(int audioCueKey)
    {
        if (_activeAudioCues.ContainsKey(audioCueKey))
        {
            _activeAudioCues[audioCueKey].ToList().ForEach(s => _audioSourcePool.Return(s));
            return true;
        }
        return false;
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