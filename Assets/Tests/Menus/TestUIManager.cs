using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestUIManager
{
    private UIManager uiManager;
    private InputReaderSO inputReader;

    [SetUp]
    public void SetUp()
    {
        GameObject testObject = new GameObject();
        testObject.AddComponent<UIManager>();

        uiManager = testObject.GetComponent<UIManager>();
        inputReader = ScriptableObject.CreateInstance<InputReaderSO>();
        uiManager._pauseMenu = new GameObject();

        uiManager._inputReader = inputReader;
    }

    [Test]
    public void PauseGame_PausesGameAndEnablesMenuInput()
    {
        uiManager.PauseGame();

        Assert.AreEqual(0f, Time.timeScale);
        Assert.IsTrue(uiManager._inputReader.isMenuInputEnabled);
    }

    [Test]
    public void UnpauseGame_UnpausesGameAndEnablesGameplayInput()
    {
        uiManager.UnpauseGame();

        Assert.AreEqual(1f, Time.timeScale);
        Assert.IsTrue(uiManager._inputReader.isGameplayInputEnabled);
    }
}
