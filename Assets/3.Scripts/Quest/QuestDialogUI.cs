using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using System.Xml;


public class QuestDialogUI : MonoBehaviour
{
    public Transform player;
    public Transform questDialogUI;
    public bool isOpenDialogUI = false;

    public string XmlFile = "Xml/QuestDialog";
    public XmlNodeList allNodes;
    public Queue<QuestDialog> dialogs = new Queue<QuestDialog>();

    public Text nameText;
    public Text sentenceText;
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
    public void StartDialog(int index)
    {
        foreach (XmlNode node in allNodes)
        {
            int num = int.Parse(node["number"].InnerText);
            if (index==num)
            {
                QuestDialog dialog = new QuestDialog();
                dialog.number = num;
                dialog.character = int.Parse(node["character"].InnerText);
                dialog.name = node["name"].InnerText;
                dialog.sentence = node["sentence"].InnerText;

                dialogs.Enqueue(dialog);
            }
        }
        DrawNextDialog();        
    }
    public void DrawNextDialog()
    {
        if (dialogs.Count==0)
        {
            EndDialog();
            return;
        }
        OpenDialogUI();
        QuestDialog dialog = dialogs.Dequeue();
        nameText.text = dialog.name;
        sentenceText.text = dialog.sentence;
    }
    public void OpenDialogUI()
    {
        if (!isOpenDialogUI)
            questDialogUI.GetComponent<Animation>().Play("DialogUIOpen");
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponentInChildren<FirePistol>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isOpenDialogUI = true;
    }
    public void CloseDialogUI()
    {
        if (isOpenDialogUI)
            questDialogUI.GetComponent<Animation>().Play("DialogUIClose");
        player.GetComponent<FirstPersonController>().enabled = true;
        player.GetComponentInChildren<FirePistol>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isOpenDialogUI = false;
    }
    public void EndDialog()
    {
        CloseDialogUI();
        switch (QuestManager.instance.currentQuest.goal.questState)
        {
            case QuestState.Ready:
            case QuestState.Complete:
                this.GetComponent<QuestUI>().OpenQuestUI();
                break;
            case QuestState.None:
            case QuestState.Accept:
                break;
        }
    }
}
