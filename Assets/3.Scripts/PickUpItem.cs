using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PickUpItem : MonoBehaviour
{
    public Item item;
    public float distance;
    public GameObject actionKey;
    public GameObject actionText;
    public GameObject extraCross;
    //public GameObject ammoLight;

    //void Awake()
    //{
    //    if (item.isQuest)
    //    {
    //        actionKey = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
    //        actionText = GameObject.Find("Canvas").transform.GetChild(3).gameObject;
    //        extraCross = GameObject.Find("Canvas").transform.GetChild(4).GetChild(1).gameObject;
    //    }
    //}

    public abstract void PickUp();

    void Update()
    {
        distance = PlayerCasting.distanceFromTarget;
        if (distance > 3)
        {
            HideActionUI();
        }
    }
    void OnMouseOver()
    {
        if (distance <= 3)
        {
            ShowActionUI();
            if (Input.GetButtonDown("Action"))
            {
                this.GetComponent<BoxCollider>().enabled = false;
                HideActionUI();
                PickUp();
            }
        }

    }
    private void ShowActionUI()
    {
        actionKey.SetActive(true);
        actionText.SetActive(true);
        extraCross.SetActive(true);
        actionText.GetComponent<Text>().text = "Pick Up " + item.name;
        //ammoLight.SetActive(true);
    }
    private void HideActionUI()
    {
        actionKey.SetActive(false);
        actionText.SetActive(false);
        extraCross.SetActive(false);
        //ammoLight.SetActive(false);
    }
}
