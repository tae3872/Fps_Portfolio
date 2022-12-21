using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullEyeExit : InterAction
{
    public GameObject emptyEye;
    public GameObject fullEyePiece;
    public GameObject hiddenExit;
    public GameObject hiddenExitTrigger;
    public Text textBox;

    public override void DoAction()
    {
        if (PlayerStats.instance.havekeys[(int)Keyword.Room02_LEFTEYE] &&
            PlayerStats.instance.havekeys[(int)Keyword.Room03_RIGHTEYE])
        {
            StartCoroutine(OpenHiddenExit());
        }
        else
        {
            StartCoroutine(LockedHiddenExit());
        }
    }
    IEnumerator OpenHiddenExit()
    {
        emptyEye.SetActive(false);
        fullEyePiece.SetActive(true);
        hiddenExit.GetComponent<Animation>().Play("HiddenExit");
        yield return new WaitForSeconds(1f);
        hiddenExitTrigger.SetActive(true);
    }
    IEnumerator LockedHiddenExit()
    {
        unInteractive = true;
        this.GetComponent<Collider>().enabled = true;
        textBox.text = "Not Enough Eye Pieces";
        yield return new WaitForSeconds(2f);
        textBox.text = "";
        unInteractive = false;
    }
}
