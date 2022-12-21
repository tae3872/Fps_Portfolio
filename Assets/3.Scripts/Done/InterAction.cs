using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InterAction : MonoBehaviour
{
    public float distance;
    public GameObject actionkey;
    public GameObject actionText;
    public GameObject extraCross;
    public string aText;

    public bool unInteractive = false;
    public abstract void DoAction();

    void Update()
    {
        distance = PlayerCasting.distanceFromTarget;
    }
    void OnMouseOver()
    {
        if (unInteractive)
            return;
        if (distance <= 3)
        {
            ShowActionUI();
            if (Input.GetButtonDown("Action"))
            {
                this.GetComponent<BoxCollider>().enabled = false;
                HideActionUI();
                DoAction();
            }
        }
        else
        {
            HideActionUI();
        }
    }
    void ShowActionUI()
    {
        actionkey.SetActive(true);
        actionText.SetActive(true);
        extraCross.SetActive(true);
        actionText.GetComponent<Text>().text = aText;
    }
    void HideActionUI()
    {
        actionkey.SetActive(false);
        actionText.SetActive(false);
        extraCross.SetActive(false);
    }
}
