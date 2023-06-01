using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Events/Quest Event Channel")]
public class QuestEventChannelSO : ScriptableObject, IQuestEventChannelSO
{
    public StartQuestAction OnStartQuestRequested {get; set;}
    public NextQuestStepAction OnNextQuestStepRequested {get; set;}


    public bool RaiseStartQuestEvent(IQuestSO quest)
    {
        if (OnStartQuestRequested != null)
        {
            return OnStartQuestRequested.Invoke(quest);
        }
        else
        {
            Debug.LogWarning("A Quest start event was requested, but nobody picked it up. " +
                "Check why there is no QuestManager already loaded, " +
                "and make sure it's listening on this Quest Event channel.");
        }
        return false;
    }

    public bool RaiseNextQuestStepEvent(IQuestSO quest)
    {
        if (OnNextQuestStepRequested != null)
        {
            return OnNextQuestStepRequested.Invoke(quest);
        }
        else
        {
            Debug.LogWarning("A Quest next step event was requested, but nobody picked it up. " +
                "Check why there is no QuestManager already loaded, " +
                "and make sure it's listening on this Quest Event channel.");
        }
        return false;
    }


}

public delegate bool StartQuestAction(IQuestSO quest);
public delegate bool NextQuestStepAction(IQuestSO quest);

