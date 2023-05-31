// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.Audio;

// public class TestAudioSourcePool
// {
//     private GameObject objectUnderTest;
//     private AudioSourcePool _pool;

//     [SetUp]
//     public void Setup()
//     {
//         objectUnderTest = new GameObject();
//         _pool = objectUnderTest.AddComponent<AudioSourcePool>();

//         GameObject newAudioObject = new GameObject("AudioSource");
//         _pool._prefab = newAudioObject.AddComponent<AudioSource>();

//         _pool._initialSize = 5;
//         _pool._parentObject = new GameObject("AudioSourcePool");
//     }

//     [TearDown]
//     public void Teardown()
//     {
//         Object.DestroyImmediate(_pool);
//     }

//     [Test]
//     public void Get_ReturnsActiveAudioSource()
//     {
//         AudioSource audioSource = _pool.Get();

//         Assert.IsNotNull(audioSource);
//         Assert.IsTrue(audioSource.gameObject.activeSelf);
//     }

//     [Test]
//     public void Return_AddsAudioSourceToPool()
//     {
//         AudioSource audioSource = _pool.Get();

//         _pool.Return(audioSource);

//         Assert.IsFalse(audioSource.gameObject.activeSelf);
//         Assert.Contains(audioSource, _pool._pool);
//     }

//     [Test]
//     public void Get_CreatesNewAudioSourceIfPoolIsEmpty()
//     {
//         _pool._pool.Clear();

//         AudioSource audioSource = _pool.Get();

//         Assert.IsNotNull(audioSource);
//         Assert.IsTrue(audioSource.gameObject.activeSelf);
//     }
// }