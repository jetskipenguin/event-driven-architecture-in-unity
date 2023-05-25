using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Scene UI")]
    [SerializeField] internal GameObject _pauseMenu = default;

    [Header("Asset References")]
    [SerializeField] internal InputReaderSO _inputReader = default;
    
    private void OnEnable()
    {
        _inputReader.PauseEvent += PauseGame;
    }

    private void OnDisable()
    {
        _inputReader.PauseEvent -= PauseGame;
    }

    internal void PauseGame()
    {
        _inputReader.PauseEvent -= PauseGame;
    
        Time.timeScale = 0f;
        _inputReader.EnableMenuInput();

        _pauseMenu.SetActive(true);

        _inputReader.UnpauseEvent += UnpauseGame;
    }

    public void UnpauseGame()
    {
        _inputReader.UnpauseEvent -= UnpauseGame;

        Time.timeScale = 1f;
        _inputReader.EnableGameplayInput();

        _pauseMenu.SetActive(false);

        _inputReader.PauseEvent += PauseGame;
    }
}
