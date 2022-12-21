using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpLeftEye : PickUpItem
{
    public GameObject leftEye;
    public GameObject leftEyeUI;
    public GameObject leftEyeLight;

    public override void PickUp()
    {
        Inventory.Instance.AddItem(item);
        StartCoroutine(GetLeftEye());
        if (QuestManager.instance.currentQuest.goal.questState == QuestState.Accept)
        {
            QuestManager.instance.UpdateItemCollect();
        }
    }
    IEnumerator GetLeftEye()
    {
        PlayerStats.instance.havekeys[(int)Keyword.Room02_LEFTEYE] = true;
        leftEye.SetActive(false);
        leftEyeLight.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        leftEyeUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        leftEyeUI.SetActive(false);
    }
}
