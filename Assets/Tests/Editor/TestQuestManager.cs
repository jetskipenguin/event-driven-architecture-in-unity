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
    public void NextQuestStep_AddsQuestToCompletedQuestsIfQuestIsCompleted()
    {
        IQuestSO quest = Substitute.For<IQuestSO>();
        IQuestStepSO questStep = Substitute.For<IQuestStepSO>();
        quest.NextStep().Returns(x => null);

        _questManager.StartQuest(quest);
        _questManager.NextQuestStep(quest);

        Assert.IsTrue(_questManager._completedQuests.Contains(quest));
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

    [Test]
    public void GetValidQuest_ReturnsNullIfNoQuestsAreValid()
    {
        IQuestSO quest = Substitute.For<IQuestSO>();
        quest.isCompleted.Returns(true);
        _questManager._quests.Add(quest);

        Assert.IsNull(_questManager.GetValidQuest(Substitute.For<NPCSO>()));
    }

    [Test]
    public void GetValidQuest_ReturnsQuestIfQuestIsNotCompletedAndPrereqsMet()
    {
        IQuestSO quest = Substitute.For<IQuestSO>();
        IQuestStepSO questStep = Substitute.For<IQuestStepSO>();
        NPCSO npc = Substitute.For<NPCSO>();
        IQuestSO prereqQuest = Substitute.For<IQuestSO>();

        quest.GetCurrentStep().Returns(questStep);
        quest.isCompleted.Returns(false);
        quest.questPrerequisites.Returns(new IQuestSO[] {prereqQuest});
        questStep.givingNPC.Returns(npc);

        _questManager._completedQuests.Add(prereqQuest);
        _questManager._quests.Add(quest);

        Assert.AreEqual(quest, _questManager.GetValidQuest(npc));
    }

    [Test]
    public void GetValidQuest_ReturnsNullIfQuestIsNotCompletedAndPrereqsNotMet()
    {
        IQuestSO quest = Substitute.For<IQuestSO>();
        IQuestStepSO questStep = Substitute.For<IQuestStepSO>();
        NPCSO npc = Substitute.For<NPCSO>();
        IQuestSO prereqQuest = Substitute.For<IQuestSO>();

        quest.GetCurrentStep().Returns(questStep);
        quest.isCompleted.Returns(false);
        questStep.givingNPC.Returns(npc);
        quest.questPrerequisites.Returns(new IQuestSO[] {prereqQuest});

        _questManager._quests.Add(quest);

        Assert.IsNull(_questManager.GetValidQuest(npc));
    }

    [Test]
    public void GetValidQuest_ReturnsNullIfQuestIsCompleted()
    {
        IQuestSO quest = Substitute.For<IQuestSO>();
        quest.isCompleted.Returns(true);
        _questManager._quests.Add(quest);

        Assert.IsNull(_questManager.GetValidQuest(Substitute.For<NPCSO>()));
    }
}
