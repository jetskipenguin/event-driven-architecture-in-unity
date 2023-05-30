using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.Audio;

public class TestAudioManager : MonoBehaviour
{
    private AudioManager _audioManager;

    [SetUp]
    public void Setup()
    {
        _audioManager = new GameObject().AddComponent<AudioManager>();
        _audioManager._audioSourcePool = Substitute.For<IAudioSourcePool>();
        
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(_audioManager);
    }

    [Test]
    public void PlayAudioCue_ReturnsUniqueID()
    {
        IAudioCueSO audioCue = Substitute.For<IAudioCueSO>();
        IAudioConfigurationSO settings = Substitute.For<IAudioConfigurationSO>();
        Vector3 position = Vector3.zero;

        int uniqueID1 = _audioManager.PlayAudioCue(audioCue, settings, position);
        int uniqueID2 = _audioManager.PlayAudioCue(audioCue, settings, position);

        Assert.AreEqual(1, uniqueID1);
        Assert.AreEqual(2, uniqueID2);
    }
}
