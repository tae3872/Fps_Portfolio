using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class QuestManager : MonoBehaviour
{
    #region Singleton
    public static QuestManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    public string XmlFile = "Xml/QuestData";
    public XmlNodeList allNodes;
    public List<Quest> playerQuests = new List<Quest>();
    public Quest currentQuest;
    public Text textBox;

    public Item[] rewardItems;

    void Start()
    {
        LoadXmlFile(XmlFile);
    }
    void LoadXmlFile(string fileName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        allNodes = xmlDoc.SelectNodes("root/quest");
    }
    public List<Quest> GetNpcQuest(int npcIndex)
    {
        List<Quest> quests = new List<Quest>();
        foreach (XmlNode node in allNodes)
        {
            int num = int.Parse(node["npc"].InnerText);
            if (npcIndex == num)
            {
                Quest quest = new Quest();
                quest.goal = new QuestGoal();

                quest.number = int.Parse(node["number"].InnerText);
                quest.npc = num;
                quest.name = node["name"].InnerText;
                quest.description = node["description"].InnerText;
                quest.dialogIndex = int.Parse(node["dialogIndex"].InnerText);
                quest.goal.questType = (QuestType)int.Parse(node["questType"].InnerText);
                quest.goal.reachedAmount = int.Parse(node["goalAmount"].InnerText);

                quest.rewardGold = int.Parse(node["gold"].InnerText);
                quest.rewardExp = int.Parse(node["exp"].InnerText);
                int itemIndex = int.Parse(node["item"].InnerText);
                quest.rewardItem = rewardItems[itemIndex];
                quests.Add(quest);
            }
        }
        return quests;
    }
    public QuestState GetQuestState(Quest npcQuest)
    {
        QuestState currentState = QuestState.Ready;
        currentQuest = npcQuest;
        foreach (Quest quest in playerQuests)
        {
            if (npcQuest == quest)
            {
                currentState = quest.goal.questState;
                break;
            }
        }
        return currentState;
    }
    public void UpdateEnemyKill()
    {
        foreach (Quest quest in playerQuests)
        {
            quest.EnemyKill();
        }
    }
    public void UpdateItemCollect()
    {
        foreach (Quest quest in playerQuests)
        {
            quest.ItemCollect();
        }
    }
    public void RewardQuest()
    {
        if (currentQuest.rewardItem != null)
        {
            Inventory.Instance.AddItem(currentQuest.rewardItem);
        }
        PlayerStats.instance.AddGold(currentQuest.rewardGold);
        PlayerStats.instance.AddExp(currentQuest.rewardExp);
        playerQuests.Remove(currentQuest);
    }
    public void QuestItemUse()
    {
        StartCoroutine(QItemUse());
    }
    IEnumerator QItemUse()
    {
        textBox.text = "퀘스트용 아이템은 사용할수 없습니다.";
        yield return new WaitForSeconds(2f);
        textBox.text = "";

    }
}
