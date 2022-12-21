using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AOpening : MonoBehaviour
{
    public SceneFader sceneFader;
    public Text textBox;
    public GameObject player;

    void Start()
    {
        AudioManager.instance.PlayBgm("SHAmb");
        player.GetComponent<FirstPersonController>().enabled = false;
        StartCoroutine(SequencePlayer());
    }
    IEnumerator SequencePlayer()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            yield return new WaitForSeconds(sceneFader.fTime);
            textBox.text = "...where am I?";
            AudioManager.instance.Play("Scene02_Line01");
            yield return new WaitForSeconds(2f);
            textBox.text = "I need to get out of here";
            AudioManager.instance.Play("Scene02_Line02");
            yield return new WaitForSeconds(2f);
            textBox.text = "";
            player.GetComponent<FirstPersonController>().enabled = true;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            yield return new WaitForSeconds(sceneFader.fTime);
            player.GetComponent<FirstPersonController>().enabled = true;
        }


    }
}
