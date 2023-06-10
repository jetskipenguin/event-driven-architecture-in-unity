using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSO : ScriptableObject
{
    [SerializeField] string npcName {get; private set;} = "New NPC";

    // TODO: add functionality for differing dialogue based on game progress and time of day
    [Tooltip("Non quest related dialogue")]
    public Dialogue[] idleDialogues {get; private set;}
}