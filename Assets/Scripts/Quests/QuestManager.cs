using System.Collections;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

// TODO: consider making this a scriptable object, _activeQuests and _completedQuests will need to persist between scenes
public class QuestManager : MonoBehaviour
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
            _completedQuests.Add(quest);
            _activeQuests.Remove(quest);
            return false;
        }
        return true;
    }

    // TODO: make NPC SO and pass in a input parameter, for now I'm using strings
    public IQuestSO GetValidQuest(string npcName)
    {
        foreach (IQuestSO quest in _quests)
        {
            if (quest.isCompleted) continue;

            // TODO: eventually compare current quest step NPC to input NPC SO
            if ((quest.GetCurrentStep().givingNPC == npcName) && PrereqsMet(quest))
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
