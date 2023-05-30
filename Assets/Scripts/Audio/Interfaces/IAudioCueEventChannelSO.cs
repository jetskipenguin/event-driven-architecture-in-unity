using UnityEngine;

public interface IAudioCueEventChannelSO
{
    int RaisePlayEvent(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 positionInSpace = default);
    bool RaiseStopEvent(int audioCueKey);
}
