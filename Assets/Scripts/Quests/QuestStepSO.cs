using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Quests/QuestStepSO")]
public class QuestStepSO: ScriptableObject
{
    public bool isCompleted {get; private set;} = false;
}

public class Dialogue
{ 
    GameObject actor;
    string text;
}
