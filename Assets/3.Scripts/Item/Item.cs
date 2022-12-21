using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "New Item")]
public class Item : ScriptableObject
{
    public bool isQuest = false;
    public int number;
    public string name;
    public string description;
    public Sprite iconImage;
    public ItemType itemType;
    public int ShopPrice;
    public int SellPrice;

    public virtual void Use()
    {
        switch (number)
        {
            case 0:
                PlayerStats.instance.AddAmmo(120);
                break;
            case 8:
                PlayerStats.instance.HealHealth(PlayerStats.instance.maxHealth);
                break;
            case 10:
                PlayerStats.instance.grenadeNum += 5;
                break;
        }
    }
}
public enum ItemType
{
    Equip, Potion, Puzzle
}