using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAnimation : MonoBehaviour
{
    public GameObject torchLight;
    public float flameTimer = 1f;
    int lightMode = 0;
    void Start()
    {
        //InvokeRepeating("PlayAnimation", 0f, 1f);
    }
    void Update()
    {
        if (lightMode == 0)
            StartCoroutine(LightAnimation());
    }
    IEnumerator LightAnimation()
    {
        lightMode = Random.Range(1, 4);
        if (lightMode == 1)
        {
            torchLight.GetComponent<Animation>().Play("TorchLightAnim01");
        }
        else if (lightMode == 2)
        {
            torchLight.GetComponent<Animation>().Play("TorchLightAnim02");
        }
        else
        {
            torchLight.GetComponent<Animation>().Play("TorchLightAnim03");
        }
        yield return new WaitForSeconds(flameTimer);
        lightMode = 0;
    }
    void PlayAnimation()
    {
        int lightMode = Random.Range(1, 4);
        if (lightMode == 1)
        {
            torchLight.GetComponent<Animation>().Play("TorchLightAnim01");
        }
        else if (lightMode == 2)
        {
            torchLight.GetComponent<Animation>().Play("TorchLightAnim02");
        }
        else
        {
            torchLight.GetComponent<Animation>().Play("TorchLightAnim03");
        }
    }
}
