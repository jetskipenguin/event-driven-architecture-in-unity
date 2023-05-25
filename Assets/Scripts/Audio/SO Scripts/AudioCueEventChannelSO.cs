using UnityEngine;

/// <summary>
/// Event on which <c>AudioCue</c> components send a message to play SFX and music. <c>AudioManager</c> listens on these events, and actually plays the sound.
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Events/AudioCue Event Channel")]
public class AudioCueEventChannelSO : ScriptableObject
{
	public AudioCuePlayAction OnAudioCuePlayRequested;
	public AudioCueStopAction OnAudioCueStopRequested;
	public AudioCueFinishAction OnAudioCueFinishRequested;

	public string RaisePlayEvent(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 positionInSpace = default)
	{
		string audioCueKey = string.Empty;

		if (OnAudioCuePlayRequested != null)
		{
			audioCueKey = OnAudioCuePlayRequested.Invoke(audioCue, audioConfiguration, positionInSpace);
		}
		else
		{
			Debug.LogWarning("An AudioCue play event was requested  for " + audioCue.name +", but nobody picked it up. " +
				"Check why there is no AudioManager already loaded, " +
				"and make sure it's listening on this AudioCue Event channel.");
		}

		return audioCueKey;
	}
}

public delegate string AudioCuePlayAction(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 positionInSpace);
public delegate bool AudioCueStopAction(string emitterKey);
public delegate bool AudioCueFinishAction(string emitterKey);
