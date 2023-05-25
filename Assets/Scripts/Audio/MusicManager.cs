using UnityEngine;

public class MusicManager : MonoBehaviour
{
	[SerializeField] private AudioCueSO _musicSO = default;
	[SerializeField] private AudioConfigurationSO _audioConfig = default;

    [SerializeField] private AudioCueEventChannelSO _musicEventChannel = default;

	private void Start()
	{
		PlayMusic();
	}

	private void PlayMusic()
	{
		_musicEventChannel.RaisePlayEvent(_musicSO, _audioConfig, transform.position);
	}
}
