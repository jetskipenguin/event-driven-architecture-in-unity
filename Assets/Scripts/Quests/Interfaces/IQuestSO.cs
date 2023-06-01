using UnityEngine;

public interface IQuestSO
{
    string questName {get;}
    IQuestStepSO[] questSteps {get;}
    IQuestSO[] questPrerequisites {get;}

    bool isCompleted {get;}
    IQuestStepSO NextStep();
    IQuestStepSO GetCurrentStep();
}