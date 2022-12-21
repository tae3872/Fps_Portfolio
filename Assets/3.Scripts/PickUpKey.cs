using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : PickUpItem
{
    public GameObject theKey;
    
    public override void PickUp()
    {
        Inventory.Instance.AddItem(item);
        PlayerStats.instance.havekeys[(int)Keyword.Room01_DOORKEY] = true;
        theKey.SetActive(false);
    }
}
