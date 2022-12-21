using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject damageFlash;
    public SceneFader fader;
    public string loadToScene = "GameOver";
    public Transform firstPerson;

    public void TakeDamage(float damage)
    {
        if (PlayerStats.instance.isDeath)
            return;
        damage -= PlayerStats.instance.defense.GetValue();
        if (damage < 0)
        {
            return;
        }
        PlayerStats.instance.health -= damage;
        Debug.Log("Player health : " + PlayerStats.instance.health);
        StartCoroutine(firstPerson.GetComponent<CameraShake>().Shake(0.15f, 0.25f));
        StartCoroutine(DamageEffect());
        if (PlayerStats.instance.health <= 0 && !PlayerStats.instance.isDeath)
        {
            Die();
        }


    }
    IEnumerator DamageEffect()
    {
        damageFlash.SetActive(true);
        int rand = Random.Range(1, 4);
        if (rand == 1)
        {
            AudioManager.instance.Play("Hurt01");
        }
        else if (rand == 2)
        {
            AudioManager.instance.Play("Hurt02");
        }
        else
        {
            AudioManager.instance.Play("Hurt03");
        }
        yield return new WaitForSeconds(1.0f);
        damageFlash.SetActive(false);
    }
    void Die()
    {
        PlayerStats.instance.isDeath = true;
        fader.FadeTo(loadToScene);
    }
}
