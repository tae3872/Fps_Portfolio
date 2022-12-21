using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BZJumpScareTrigger : MonoBehaviour
{
    public GameObject Door1;
    
    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(SequencePlayer());
    }
    IEnumerator SequencePlayer()
    {
        Door1.GetComponent<Animation>().Play();
        AudioManager.instance.Play("DoorBang");
        this.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(1f);
        //AudioManager.instance.PlayBgm("JumpscareTune");
    }
    
}
