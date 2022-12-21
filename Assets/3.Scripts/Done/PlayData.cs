using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayData
{
    public int sceneNum;
    public float health;
    public int ammoCount;
    public WeaponType weaponType;

    public int currentExp;
    public int level;
    public int gold;
    public PlayData()
    {
        sceneNum = PlayerStats.instance.sceneNum;
        health = PlayerStats.instance.health;
        ammoCount = PlayerStats.instance.ammoCount;
        weaponType = PlayerStats.instance.weaponType;
        currentExp = PlayerStats.instance.currentExp;
        level = PlayerStats.instance.level;
        gold = PlayerStats.instance.gold;
    }
}
