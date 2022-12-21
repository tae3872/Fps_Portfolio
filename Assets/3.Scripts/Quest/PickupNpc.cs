using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PickupNpc : MonoBehaviour
{
    public abstract void PickUpEvent();

    public Npc npc;
    public float distance;
    public GameObject actionkey;
    public GameObject actionText;
    public GameObject extraCross;

    void Update()
    {
        distance = PlayerCasting.distanceFromTarget;
    }
    void OnMouseOver()
    {
        if (distance <=2)
        {
            ShowActionUI();
        }
        else
        {
            HideActionUI();
        }
        if (Input.GetButtonDown("Action"))
        {
            if (distance<=2)
            {
                HideActionUI();
                PickUpEvent();
            }
        }
    }
    void ShowActionUI()
    {
        actionkey.SetActive(true);
        actionText.SetActive(true);
        extraCross.SetActive(true);
        actionText.GetComponent<Text>().text = "Talk with " + npc.name;
    }
    void HideActionUI()
    {
        actionkey.SetActive(false); 
        actionText.SetActive(false);
        extraCross.SetActive(false);
    }
}
