using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    [Tooltip("Speed that dialogue writes onto dialogue box")]
    [SerializeField] internal float textSpeed = 0.1f;

    [Tooltip("Delay between dialogue lines")]
    [SerializeField] internal float dialogueDelay = 3f;

    [Header("UI Components")]
    [SerializeField] public Image portrait = default;
    [SerializeField] public TextMeshProUGUI nameText = default;
    [SerializeField] internal TextMeshProUGUI dialogueText = default;
    
    [Header("Asset References")]
    [SerializeField] private InputReaderSO _inputReader = default;    

    internal bool isWriting = false;
    internal string currentDialogue = default;

    private void OnEnable()
    {
        _inputReader.SkipDialogueEvent += SkipWriteText;
    }

    private void OnDisable()
    {
        _inputReader.SkipDialogueEvent -= SkipWriteText;
    }

    public void ConfigureDialogueBox(Sprite portraitSprite, string name)
    {
        portrait.sprite = portraitSprite;
        nameText.text = name;
    }

    public IEnumerator WriteDialogue(string[] dialogueArray)
    {
        foreach (string dialogue in dialogueArray)
        {
            while (isWriting)
            {
                yield return null;
            }

            yield return StartCoroutine(WriteText(dialogue));
            yield return new WaitForSeconds(dialogueDelay);
        }
    }

    private IEnumerator WriteText(string dialogue)
    {
        isWriting = true;
        dialogueText.text = "";

        currentDialogue = dialogue;

        foreach (char c in dialogue)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isWriting = false;
    }

    internal void SkipWriteText()
    {
        if(isWriting)
        {
            StopCoroutine(WriteText(currentDialogue));
            dialogueText.text = currentDialogue;
            isWriting = false;
        }
    }
}
