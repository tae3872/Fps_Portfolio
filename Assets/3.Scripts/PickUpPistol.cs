using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPistol : PickUpItem
{
    public GameObject fakePistol;
    public GameObject Arrow;

    public GameObject jumpScareTrigger;

    public override void PickUp()
    {
        Inventory.Instance.AddItem(item);
        fakePistol.SetActive(false);
        Arrow.SetActive(false);
        jumpScareTrigger.SetActive(true);
    }
}
