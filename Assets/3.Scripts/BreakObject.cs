using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    public GameObject fakeObject;
    public GameObject breakObject;
    public GameObject sphereObject;

    public GameObject item;

    public float health = 1f;
    bool isBreak = false;
    public bool isUnBreakable = false;
    public bool haveItem = false;

    public void TakeDamage(float damage)
    {
        if (isUnBreakable)
            return;
        health -= damage;
        if (health <= 0 && !isBreak)
        {
            StartCoroutine(Break());
        }
    }
    IEnumerator Break()
    {
        isBreak = true;
        this.GetComponent<Collider>().enabled = false;
        fakeObject.SetActive(false);
        sphereObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        breakObject.SetActive(true);
        AudioManager.instance.Play("PotterySmash");
        yield return new WaitForSeconds(0.1f);
        sphereObject.SetActive(false);
        if (haveItem)
        {
            item.SetActive(true);
        }
    }
}
