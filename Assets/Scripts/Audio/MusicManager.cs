using UnityEngine;

public class MusicManager : MonoBehaviour
{
	[SerializeField] private AudioCueSO _musicSO = default;
	[SerializeField] private AudioConfigurationSO _audioConfig = default;

    [SerializeField] private AudioCueEventChannelSO _musicEventChannel = default;

	private int _musicKey;

	private void Start()
	{
		PlayMusic();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			_musicEventChannel.RaiseStopEvent(_musicKey);
		}
	}

	private void PlayMusic()
	{
		_musicKey = _musicEventChannel.RaisePlayEvent(_musicSO, _audioConfig, transform.position);
	}
}
