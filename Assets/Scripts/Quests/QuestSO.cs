using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Quests/QuestSO")]
public class QuestSO : ScriptableObject, IQuestSO
{
    public string questName {get; private set;} = "New Quest";
    public IQuestStepSO[] questSteps {get; private set;} 
    public IQuestSO[] questPrerequisites {get; private set;}

    public bool isCompleted {get; private set;} = false;
    private int currQuestStep = 0;

    public IQuestStepSO NextStep()
    {
        if (currQuestStep < questSteps.Length)
        {
            return questSteps[currQuestStep++];
        }
        else
        {
            isCompleted = true;
            return null;
        }
    }

    public IQuestStepSO GetCurrentStep()
    {
        if (currQuestStep < questSteps.Length)
        {
            return questSteps[currQuestStep];
        }
        else
        {
            Debug.LogError("Requested Quest Step that does not exist!");
            return null;
        }
    }
}
