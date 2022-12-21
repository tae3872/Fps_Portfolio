using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorJumpTrigger : MonoBehaviour
{
    public GameObject activator;
    public GameObject flying;

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(SequencePlayer());
    }
    IEnumerator SequencePlayer()
    {
        this.GetComponent<Collider>().enabled = false;
        activator.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        activator.SetActive(false);
        yield return new WaitForSeconds(3f);
        flying.SetActive(false);
    }
}
