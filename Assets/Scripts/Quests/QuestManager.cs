using System.Collections;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Listening on channels")]
    [Tooltip("The QuestManager listens on this channel for quest related functions")]
	[SerializeField] internal SerializableInterface<IQuestEventChannelSO> _questEventChannel;

    internal HashSet<IQuestSO> _activeQuests = new HashSet<IQuestSO>();

    private void OnEnable()
    {
        _questEventChannel.Value.OnStartQuestRequested += StartQuest;
        _questEventChannel.Value.OnNextQuestStepRequested += NextQuestStep;
    }

    private void OnDisable()
    {
        _questEventChannel.Value.OnStartQuestRequested -= StartQuest;
        _questEventChannel.Value.OnNextQuestStepRequested -= NextQuestStep;
    }

    public bool StartQuest(IQuestSO quest)
    {
        if (_activeQuests.Contains(quest))
        {
            Debug.LogWarning("Quest " + quest.questName + " is already active");
            return false;
        }

        _activeQuests.Add(quest);
        return true;
    }

    public bool NextQuestStep(IQuestSO quest)
    {
        if (!_activeQuests.Contains(quest))
        {
            Debug.LogWarning("Quest " + quest.questName + " is not active");
            return false;
        }

        IQuestStepSO nextStep = quest.NextStep();
        if (nextStep == null)
        {
            _activeQuests.Remove(quest);
            return false;
        }
        return true;
    }
}
