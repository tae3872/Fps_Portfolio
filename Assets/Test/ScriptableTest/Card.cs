using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string name;
    public string description;
    public int manaCost;
    public int attack;
    public int health;
    public Sprite artImage;
}
