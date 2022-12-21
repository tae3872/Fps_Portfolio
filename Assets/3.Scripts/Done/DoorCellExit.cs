using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCellExit : InterAction
{
    public SceneFader fader;
    public string loadToScene = "MainScene002";
    public GameObject door;

    public override void DoAction()
    {
        StartCoroutine(ChangeScene());
    }
    IEnumerator ChangeScene()
    {
        door.GetComponent<Animation>().Play("Door1OpenAnim");
        yield return new WaitForSeconds(1f);
        AudioManager.instance.StopBgm();
        SaveLoad.SaveData();
        fader.FadeTo(loadToScene);
    }
}
