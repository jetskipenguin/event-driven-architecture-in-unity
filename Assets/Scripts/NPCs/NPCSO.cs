using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/NPCs/NPCSO")]
public class NPCSO : ScriptableObject
{
    public string npcName {get; private set;} = "New NPC";
    public bool isQuestGiver {get; private set;} = false;

    // TODO: add functionality for differing dialogue based on game progress and time of day
    [Tooltip("Non quest related dialogue")]
    public Dialogue[] idleDialogues {get; private set;}
}