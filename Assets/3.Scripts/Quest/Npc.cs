using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Npc
{
    public int number;
    public string name;
    public string description;
    public NpcType npcType;
}
public enum NpcType
{
    Merchant, BlackSmith, SkillMaster, QuestGiver
}
