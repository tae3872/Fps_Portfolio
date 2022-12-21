using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;
    public float fTime = 1f;
    void Start()
    {
        img.color = new Color(0, 0, 0, 1);
        FadeStart(fTime);
    }
    public void FadeStart(float fadeWait)
    {
        if (fadeWait == 0)
            return;
        StartCoroutine(FadeIn(fadeWait));

    }
    public void FadeTo(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }
    public void FadeTo(int sceneNum)
    {
        StartCoroutine(FadeOut(sceneNum));
    }
    IEnumerator FadeIn(float fadeWait)
    {
        yield return new WaitForSeconds(fadeWait);
        float t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0, 0, 0, a);
            yield return 0;
        }
    }
    IEnumerator FadeOut(int sceneNum)
    {
        float t = 0f;
        while (t <= 1)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0, 0, 0, a);
            yield return 0;
        }
        SceneManager.LoadScene(sceneNum);
    }
    IEnumerator FadeOut(string sceneName)
    {
        float t = 0f;
        while (t <= 1)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0, 0, 0, a);
            yield return 0;
        }
        SceneManager.LoadScene(sceneName);
    }
}
