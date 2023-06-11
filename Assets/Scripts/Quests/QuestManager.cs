using System.Collections;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Quests/QuestManager")]
public class QuestManager : ScriptableObject
{
    [Header("Listening on channels")]
    [Tooltip("The QuestManager listens on this channel for quest related functions")]
	[SerializeField] internal SerializableInterface<IQuestEventChannelSO> _questEventChannel;

    [Header("Quests")]
    [Tooltip("Quests that can be given out, priority is given to quests at the top of the list")]
    [SerializeField] internal List<IQuestSO> _quests = new List<IQuestSO>();

    internal HashSet<IQuestSO> _activeQuests = new HashSet<IQuestSO>();
    internal HashSet<IQuestSO> _completedQuests = new HashSet<IQuestSO>();

    private void OnEnable()
    {
        try {
            _questEventChannel.Value.OnStartQuestRequested += StartQuest;
            _questEventChannel.Value.OnNextQuestStepRequested += NextQuestStep;
        }
        catch(System.Exception)
        {
            Debug.LogWarning("QuestManager: No QuestEventChannelSO found, will not be able to subscribe to quest related events");
        }
    }

    private void OnDisable()
    {
        try {
            _questEventChannel.Value.OnStartQuestRequested -= StartQuest;
            _questEventChannel.Value.OnNextQuestStepRequested -= NextQuestStep;
        }
        catch(System.Exception)
        {
            Debug.LogWarning("QuestManager: No QuestEventChannelSO found, will not be able to unsubscribe to quest related events");
        }
    }

    internal bool StartQuest(IQuestSO quest)
    {
        if (_activeQuests.Contains(quest))
        {
            Debug.LogWarning("Quest " + quest.questName + " is already active");
            return false;
        }

        _activeQuests.Add(quest);
        return true;
    }

    internal bool NextQuestStep(IQuestSO quest)
    {
        if (!_activeQuests.Contains(quest))
        {
            Debug.LogWarning("Quest " + quest.questName + " is not active");
            return false;
        }

        IQuestStepSO nextStep = quest.NextStep();
        if (nextStep == null)
        {
            _completedQuests.Add(quest);
            _activeQuests.Remove(quest);
            return false;
        }
        return true;
    }

    public IQuestSO GetValidQuest(NPCSO requestingNPC)
    {
        foreach (IQuestSO quest in _quests)
        {
            if (quest.isCompleted) continue;

            if ((quest.GetCurrentStep().givingNPC == requestingNPC) && PrereqsMet(quest))
            {
                return quest;
            }
            
        }
        return null;
    }

    private bool PrereqsMet(IQuestSO quest)
    {
        foreach (IQuestSO prereq in quest.questPrerequisites)
        {
            if (!_completedQuests.Contains(prereq))
            {
                return false;
            }
        }
        return true;
    }
}
