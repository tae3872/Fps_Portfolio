using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupQuestGiver : PickupNpc
{
    QuestManager questManager;
    public QuestDialogUI dialogUI;
    public List<Quest> npcQuests;

    void Start()
    {
        questManager = QuestManager.instance;
        npcQuests = questManager.GetNpcQuest(npc.number);
    }
    public override void PickUpEvent()
    {
        Debug.Log("Npc에게 대화를 요청");
        //dialogUI.StartDialog(6);
        if (npcQuests.Count == 0)
        {
            questManager.currentQuest.goal.questState = QuestState.None;
            dialogUI.StartDialog(Random.Range(0, 3));
        }
        npcQuests[0].goal.questState = questManager.GetQuestState(npcQuests[0]);
        switch (npcQuests[0].goal.questState)
        {
            case QuestState.Ready:
                dialogUI.StartDialog(npcQuests[0].dialogIndex);
                break;
            case QuestState.Accept:
                dialogUI.StartDialog(npcQuests[0].dialogIndex + 1);
                break;
            case QuestState.Complete:
                CompleteQuest();
                break;
        }
    }
    void CompleteQuest()
    {
        if (questManager.currentQuest.number==0)
        {
            Spawner.spawnNum = 50;
        }
        else if (questManager.currentQuest.number == 1)
        {
            Spawner.spawnNum = 100;
        }
        else if (questManager.currentQuest.number == 2)
        {
            Spawner.spawnNum = 150;
        }
        dialogUI.StartDialog(npcQuests[0].dialogIndex + 2);
        npcQuests.RemoveAt(0);
        questManager.RewardQuest();
    }
}
