using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Scene UI")]
    [SerializeField] internal GameObject _pauseMenu = default;
    [SerializeField] internal GameObject _dialogueBox = default;

    [Header("Asset References")]
    [SerializeField] internal InputReaderSO _inputReader = default;

    private DialogueBox _dialogueBoxScript;
    
    private void OnEnable()
    {
        _inputReader.PauseEvent += PauseGame;
        _inputReader.InteractEvent += ShowDialogueBox;
    }

    private void OnDisable()
    {
        _inputReader.PauseEvent -= PauseGame;
        _inputReader.InteractEvent -= ShowDialogueBox;
    }

    internal void PauseGame()
    {
        _inputReader.PauseEvent -= PauseGame;
    
        Time.timeScale = 0f;
        _inputReader.EnableMenuInput();

        _pauseMenu.SetActive(true);

        _inputReader.UnpauseEvent += UnpauseGame;
    }

    internal void UnpauseGame()
    {
        _inputReader.UnpauseEvent -= UnpauseGame;

        Time.timeScale = 1f;
        _inputReader.EnableGameplayInput();

        _pauseMenu.SetActive(false);

        _inputReader.PauseEvent += PauseGame;
    }

    internal void ShowDialogueBox(Image portrait, string name, string[] dialogueText)
    {
        _inputReader.InteractEvent -= ShowDialogueBox;
        _inputReader.InteractEvent += HideDialogueBox;

        _dialogueBox.SetActive(true);

        if (_dialogueBoxScript == null)
            _dialogueBoxScript = _dialogueBox.GetComponent<DialogueBox>();

        _dialogueBoxScript.ConfigureDialogueBox(portrait.sprite, name);
        StartCoroutine(_dialogueBoxScript.WriteDialogue(dialogueText));
    }

    internal void HideDialogueBox()
    {
        _dialogueBox.SetActive(false);

        _inputReader.InteractEvent -= HideDialogueBox;
        _inputReader.InteractEvent += ShowDialogueBox;
    }    
}
