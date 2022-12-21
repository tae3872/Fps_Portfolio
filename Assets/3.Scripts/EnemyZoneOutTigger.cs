using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZoneOutTigger : MonoBehaviour
{
    public Transform mutant;
    public GameObject zoneInTrigger;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && mutant != null)
        {
            mutant.GetComponent<Mutant>().GoBack();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            this.gameObject.SetActive(false);
            zoneInTrigger.SetActive(true);
        }
    }
}
