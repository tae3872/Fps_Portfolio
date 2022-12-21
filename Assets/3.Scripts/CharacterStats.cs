using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stats attack;
    public Stats defense;

    public float startAttack = 5;
    public float startDefense = 10;
    [HideInInspector]
    public float health;
    public float maxHealth = 20f;
    public bool isDeath = false;

    public void InitStats()
    {
        attack.SetBaseValue(startAttack);
        defense.SetBaseValue(startDefense);
        health = maxHealth;
    }
    public void OnEquipItemChanged(Equip oldItem, Equip newItem)
    {
        if (oldItem!=null)
        {
            attack.RemoveValue(oldItem.attack);
            defense.RemoveValue(oldItem.defense);
        }
        if (newItem!=null)
        {
            attack.AddValue(newItem.attack);
            defense.AddValue(newItem.defense);
        }
    }
}
