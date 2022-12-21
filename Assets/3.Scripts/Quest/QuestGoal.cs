using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public QuestState questState;
    public QuestType questType;
    public int reachedAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return currentAmount >= reachedAmount;
    }
}
public enum QuestState
{
    None, Ready, Accept, Complete
}
public enum QuestType
{
    Kill, Collect
}