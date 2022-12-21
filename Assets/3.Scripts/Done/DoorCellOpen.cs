using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCellOpen : InterAction
{
    public GameObject door;
    void Update()
    {
        distance = PlayerCasting.distanceFromTarget;
    }
    public override void DoAction()
    {
        door.GetComponent<Animation>().Play("DoorOpenAnim");
        AudioManager.instance.Play("CreakyDoor");
    }
}
