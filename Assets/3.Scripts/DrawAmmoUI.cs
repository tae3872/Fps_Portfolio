using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawAmmoUI : MonoBehaviour
{
    public Text ammoCount;
    void Update()
    {
        ammoCount.text = PlayerStats.instance.ammoCount.ToString();
    }
}
