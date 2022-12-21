using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    #region Singleton
    public static Equipment instance;
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
    #endregion
    public delegate void EquipmentDelegate(Equip oldItem, Equip newItem);
    public EquipmentDelegate equipmentDelegate;

    public delegate void SelectDelegate(int index);
    public SelectDelegate selectCallback;

    public Equip[] items;
    void Start()
    {
        items = new Equip[(int)EquipType.TypeMax];
    }
    public void EquipItem(Equip newItem)
    {
        int index = (int)newItem.equipType;
        Equip oldItem = items[index];
        if (oldItem != null)
        {
            Inventory.Instance.AddItem(oldItem);
        }
        items[index] = newItem;
        if (equipmentDelegate != null)
        {
            equipmentDelegate(oldItem, newItem);
        }
    }
    public void UnEquipItem(Equip oldItem)
    {
        int index = (int)oldItem.equipType;
        items[index] = null;
        if (index == 1)
            PlayerStats.instance.weaponType = WeaponType.NONE;
        Inventory.Instance.AddItem(oldItem);
        if (equipmentDelegate != null)
        {
            equipmentDelegate(oldItem, null);
        }
    }
    public void SelectSlot(int index)
    {
        if (selectCallback != null)
        {
            selectCallback(index);
        }
    }
}
