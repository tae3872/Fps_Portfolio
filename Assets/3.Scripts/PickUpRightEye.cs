using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRightEye : PickUpItem
{
    public GameObject RightEye;
    public GameObject RightEyeUI;
    public GameObject RightEyeLight;

    public GameObject fakeWall;
    public GameObject hiddenWall;
    public GameObject fullEyeLight;

    public override void PickUp()
    {
        Inventory.Instance.AddItem(item);
        StartCoroutine(GetRightEye());
        if (QuestManager.instance.currentQuest.goal.questState == QuestState.Accept)
        {
            QuestManager.instance.UpdateItemCollect();
        }
    }
    IEnumerator GetRightEye()
    {
        PlayerStats.instance.havekeys[(int)Keyword.Room03_RIGHTEYE] = true;
        RightEye.SetActive(false);
        RightEyeLight.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        RightEyeUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        RightEyeUI.SetActive(false);

        fakeWall.SetActive(false);
        hiddenWall.SetActive(true);

        fullEyeLight.SetActive(true);
    }
}
