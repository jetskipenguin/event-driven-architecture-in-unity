// using NUnit.Framework;
// using NSubstitute;
// using UnityEngine;
// using UnityEngine.Audio;

// public class TestAudioManager : MonoBehaviour
// {
//     private AudioManager _audioManager;

//     [SetUp]
//     public void Setup()
//     {
//         _audioManager = new GameObject().AddComponent<AudioManager>();
        
//     }

//     [TearDown]
//     public void Teardown()
//     {
//         Object.DestroyImmediate(_audioManager);
//     }

//     [Test]
//     public void PlayAudioCue_ReturnsUniqueID()
//     {
//         AudioCueSO audioCue = Substitute.For<IAudioCueSO>();
//         AudioConfigurationSO settings = ScriptableObject.CreateInstance<AudioConfigurationSO>();
//         Vector3 position = Vector3.zero;

//         int uniqueID = _audioManager.PlayAudioCue(audioCue, settings, position);

//         Assert.AreEqual(1, uniqueID);
//     }

// }
