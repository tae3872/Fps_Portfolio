using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public int number;
    public int npc;
    public string name;
    public string description;
    public int dialogIndex;

    public QuestGoal goal;

    public int rewardGold;
    public int rewardExp;
    public Item rewardItem;

    public void EnemyKill()
    {
        if (goal.questType == QuestType.Kill)
        {
            goal.currentAmount++;
            if (goal.IsReached())
            {
                goal.questState = QuestState.Complete;
            }
        }
    }
    public void ItemCollect()
    {
        if (goal.questType == QuestType.Collect)
        {
            goal.currentAmount++;
            if (goal.IsReached())
            {
                goal.questState = QuestState.Complete;
            }
        }
    }
}
