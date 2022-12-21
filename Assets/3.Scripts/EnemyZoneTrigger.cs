using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZoneTrigger : MonoBehaviour
{
    public Transform mutant;
    public GameObject zoneOutTrigger;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && mutant != null)
        {
            mutant.GetComponent<Mutant>().Chaser();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            this.gameObject.SetActive(false);
            zoneOutTrigger.SetActive(true);
        }
    }
}
