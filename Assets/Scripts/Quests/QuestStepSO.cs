using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Quests/QuestStepSO")]
public class QuestStepSO: ScriptableObject
{
    public NPCSO givingNPC {get; private set;}
    public bool isCompleted {get; private set;} = false;
    public Dialogue[] dialogues;
}

public class Dialogue
{ 
    NPCSO actor;
    string[] text;
}
