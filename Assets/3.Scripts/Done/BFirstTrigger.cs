using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BFirstTrigger : MonoBehaviour
{
    public GameObject Player;
    public GameObject Arrow;
    public Text textBox;

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(SequencePlayer());
    }
    IEnumerator SequencePlayer()
    {
        textBox.text = "Look Like a weapon on the table";
        AudioManager.instance.Play("Scene02_Line03");
        Arrow.SetActive(true);
        this.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(3f);
        textBox.text = "";
    }
}
