using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;

public class TestAudioSourcePool
{
    private GameObject objectUnderTest;
    private AudioSourcePool _pool;

    [SetUp]
    public void Setup()
    {
        objectUnderTest = new GameObject();
        _pool = objectUnderTest.AddComponent<AudioSourcePool>();

        GameObject newAudioObject = new GameObject("AudioSource");
        _pool._prefab = newAudioObject.AddComponent<AudioSource>();

        _pool._initialSize = 5;
        _pool.Prewarm();
        _pool._parentObject = new GameObject("AudioSourcePool");
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(_pool);
    }

    [Test]
    public void Get_ReturnsActiveAudioSource()
    {
        (AudioSource audioSource, int id) = _pool.Get();

        Assert.IsNotNull(audioSource);
        Assert.IsTrue(audioSource.gameObject.activeSelf);
    }

    [Test]
    public void Return_AddsAudioSourceToPool()
    {
        (AudioSource audioSource, int id) = _pool.Get();

        _pool.Return(id);

        Assert.IsFalse(audioSource.gameObject.activeSelf);
        Assert.Contains(audioSource, _pool._pool);
    }

    [Test]
    public void Get_CreatesNewAudioSourceIfPoolIsEmpty()
    {
        _pool._pool.Clear();

        (AudioSource audioSource, int id) = _pool.Get();

        Assert.IsNotNull(audioSource);
        Assert.IsTrue(audioSource.gameObject.activeSelf);
    }

    [Test]
    public void Return_DoesNotAddAudioSourceToPoolIfPoolIsFull()
    {
        _pool._pool.Clear();
        _pool._initialSize = 1;
        _pool.Prewarm();

        (AudioSource audioSource1, int id1) = _pool.Get();
        (AudioSource audioSource2, int id2) = _pool.Get();

        _pool.Return(id1);
        _pool.Return(id2);

        Assert.IsFalse(audioSource2.gameObject.activeSelf);
        Assert.IsTrue(audioSource1 == null);
        Assert.AreEqual(1, _pool._pool.Count);
    }
}