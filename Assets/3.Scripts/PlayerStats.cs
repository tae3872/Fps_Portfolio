using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public static PlayerStats instance;
    public int sceneNum;

    public int currentExp;
    public int level;
    public int gold;

    public WeaponType weaponType = WeaponType.NONE;
    public int ammoCount;
    public int grenadeNum;
    public bool[] havekeys;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        Equipment.instance.equipmentDelegate += OnEquipItemChanged;
        NewStartGameData();
    }
    public void InitPlayerStats(PlayData pData)
    {
        InitStats();
        if (pData!=null)
        {
            sceneNum = pData.sceneNum;
            health = pData.health;
            ammoCount = pData.ammoCount;
            weaponType = pData.weaponType;
            currentExp = pData.currentExp;
            //level = pData.level;
            gold = pData.gold;
        }
        else
        {
            sceneNum = 0;
            ammoCount = 0;
            weaponType = WeaponType.NONE;
            currentExp = 0;
            level = 1;
            gold = 1000;
        }
        havekeys = new bool[(int)Keyword.MAX_KEY];
    }

    public void NewStartGameData()
    {
        InitStats();
        sceneNum = 0;
        ammoCount = 0;
        weaponType = WeaponType.NONE;
        currentExp = 0;
        level = 1;
        gold = 1000;
        havekeys = new bool[(int)Keyword.MAX_KEY];
        for (int i = 0; i < havekeys.Length; i++)
        {
            havekeys[i] = false;
        }
    }
    public void AddAmmo(int amount)
    {
        ammoCount += amount;
    }
    public bool UseAmmo(int amount)
    {
        if (ammoCount < amount)
            return false;
        ammoCount -= amount;
        return true;
    }
    public void AddGold(int amount)
    {
        if (amount == 0)
            return;
        gold += amount;
    }
    public bool UseGold(int amount)
    {
        if (amount == 0)
            return true;
        if (gold < amount)
        {
            Debug.Log("돈이 부족합니다");
            return false;
        }
        gold -= amount;
        return true;
    }
    public bool AddExp(int amount)
    {
        if (amount == 0)
            return false;
        bool result = false;
        Debug.Log("경험치 " + amount);
        currentExp += amount;
        while (currentExp >= NeedExpLvUp(level))
        {
            currentExp -= NeedExpLvUp(level);
            level++;
            //NetManager
            maxHealth += 10;
            health = maxHealth;
            result = true;
        }
        return result;
    }
    public int NeedExpLvUp(int _level)
    {
        return _level * 100;
    }
    public void HealHealth(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
public enum WeaponType
{
    NONE,
    PISTOL,
    MAGNUM,
    SNIPER,
}
public enum Keyword
{
    Room01_DOORKEY,
    Room02_LEFTEYE,
    Room03_RIGHTEYE,
    MAX_KEY,
}