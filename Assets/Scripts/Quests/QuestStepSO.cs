using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Quests/QuestStepSO")]
public class QuestStepSO: ScriptableObject
{
    // TODO: eventually update this with NPC SO
    public string givingNPC {get; private set;}
    public bool isCompleted {get; private set;} = false;
    public Dialogue[] dialogues;
}

public class Dialogue
{ 
    // TODO: update this with NPC SO in future
    GameObject actor;
    string text;
}
