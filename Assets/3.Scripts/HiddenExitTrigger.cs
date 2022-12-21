using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenExitTrigger : MonoBehaviour
{
    public SceneFader fader;
    public string loadToScene = "MainMenu";

    void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.StopBgm();
        fader.FadeTo(loadToScene);
    }
}
