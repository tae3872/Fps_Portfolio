using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimation : MonoBehaviour
{
    public GameObject torchLight;

    public int lightMode = 0;
    void Update()
    {
        if (lightMode == 0)
        {
            StartCoroutine(FlameAnimation());
        }
    }
    IEnumerator FlameAnimation()
    {
        lightMode = Random.Range(1, 4);
        torchLight.GetComponent<Animator>().SetInteger("LightMode", lightMode);
        yield return new WaitForSeconds(1f);
        lightMode = 0;
    }
}
