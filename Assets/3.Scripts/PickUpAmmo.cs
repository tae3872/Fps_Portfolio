using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : PickUpItem
{
    public override void PickUp()
    {
        Inventory.Instance.AddItem(item);
        Destroy(gameObject);
    }
}
