using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class QuestUI : MonoBehaviour
{
    public GameObject questUI;
    public GameObject player;
    public Text nameText;
    public Text descriptionText;
    public Text goalText;
    public Text goldText;
    public Text expText;

    public GameObject rewardItem;
    public Text itemText;
    public Image iconImage;

    public GameObject acceptButton;
    public GameObject giveUpButton;
    public GameObject okButton;

    QuestManager questManager;
    public bool isOpenQuestUI = false;

    void SetQuestUI()
    {
        nameText.text = questManager.currentQuest.name;
        descriptionText.text = questManager.currentQuest.description;

        goalText.text = questManager.currentQuest.goal.currentAmount + " / " + questManager.currentQuest.goal.reachedAmount;
        goldText.text = questManager.currentQuest.rewardGold.ToString();
        expText.text = questManager.currentQuest.rewardExp.ToString();

        if (questManager.currentQuest.rewardItem != null)
        {
            rewardItem.SetActive(true);
            itemText.text = questManager.currentQuest.rewardItem.name;
            iconImage.sprite = questManager.currentQuest.rewardItem.iconImage;
        }
        else
        {
            rewardItem.SetActive(false);
        }
        acceptButton.SetActive(false);
        giveUpButton.SetActive(false);
        okButton.SetActive(false);
        switch (questManager.currentQuest.goal.questState)
        {
            case QuestState.Ready:
                acceptButton.SetActive(true);
                break;
            case QuestState.Accept:
                giveUpButton.SetActive(true);
                break;
            case QuestState.Complete:
                descriptionText.text = "Quest Complete";
                okButton.SetActive(true);
                break;
        }
    }
    void Start()
    {
        questManager = QuestManager.instance;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Toggle();
        }
    }
    void Toggle()
    {
        if (questManager.playerQuests.Count == 0)
            return;
        if (isOpenQuestUI)
        {
            CloseQuestUI();
        }
        else
        {
            questManager.currentQuest = questManager.playerQuests[0];
            OpenQuestUI();
        }
    }
    public void OpenQuestUI()
    {
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponentInChildren<FirePistol>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SetQuestUI();
        questUI.GetComponent<Animation>().Play("QuestUIOpen");
        isOpenQuestUI = true;
    }
    public void CloseQuestUI()
    {
        player.GetComponent<FirstPersonController>().enabled = true;
        player.GetComponentInChildren<FirePistol>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        questUI.GetComponent<Animation>().Play("QuestUIClose");
        isOpenQuestUI = false;
    }
    public void AcceptQuest()
    {
        questManager.currentQuest.goal.questState = QuestState.Accept;
        questManager.playerQuests.Add(questManager.currentQuest);
        CloseQuestUI();
    }
    public void GiveUpQuest()
    {
        questManager.playerQuests.Remove(questManager.currentQuest);
        CloseQuestUI();
    }
}
