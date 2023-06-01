using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.Audio;

public class TestQuestManager
{
    private QuestManager _questManager;

    [SetUp]
    public void Setup()
    {
        _questManager = new GameObject("QuestManager").AddComponent<QuestManager>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(_questManager.gameObject);
    }

    [Test]
    public void StartQuest_AddsQuestToActiveQuests()
    {
        IQuestSO quest = Substitute.For<IQuestSO>();

        _questManager.StartQuest(quest);

        Assert.IsTrue(_questManager._activeQuests.Contains(quest));
    }

    [Test]  
    public void StartQuest_ReturnsFalseIfQuestIsAlreadyActive()
    {
        IQuestSO quest = Substitute.For<IQuestSO>();

        _questManager.StartQuest(quest);

        Assert.IsFalse(_questManager.StartQuest(quest));
    }

    [Test]
    public void NextQuestStep_ReturnsFalseIfQuestIsNotActive()
    {
        IQuestSO quest = Substitute.For<IQuestSO>();

        Assert.IsFalse(_questManager.NextQuestStep(quest));
    }

    [Test]
    public void NextQuestStep_ReturnsFalseIfQuestIsCompleted()
    {
        IQuestSO quest = Substitute.For<IQuestSO>();
        IQuestStepSO questStep = Substitute.For<IQuestStepSO>();
        quest.NextStep().Returns(x => null);

        _questManager.StartQuest(quest);

        Assert.IsFalse(_questManager.NextQuestStep(quest));
    }

    [Test]
    public void NextQuestStep_ReturnsTrueIfQuestIsNotCompleted()
    {
        IQuestSO quest = Substitute.For<IQuestSO>();
        IQuestStepSO questStep = Substitute.For<IQuestStepSO>();
        quest.NextStep().Returns(questStep);
        questStep.isCompleted.Returns(false);

        _questManager.StartQuest(quest);

        Assert.IsTrue(_questManager.NextQuestStep(quest));
    }
}
