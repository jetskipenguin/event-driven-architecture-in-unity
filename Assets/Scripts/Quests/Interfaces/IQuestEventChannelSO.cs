using UnityEngine;

public interface IQuestEventChannelSO
{
    public StartQuestAction OnStartQuestRequested {get; set;}
    public NextQuestStepAction OnNextQuestStepRequested {get; set;}

    public bool RaiseStartQuestEvent(IQuestSO quest);
    public bool RaiseNextQuestStepEvent(IQuestSO quest);
}