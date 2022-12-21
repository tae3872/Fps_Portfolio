using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    public delegate void InventoryDelegate(int add);
    public InventoryDelegate invenDelegate;

    public delegate void SelectSlotDelegate(int slot);
    public SelectSlotDelegate selectCallBack;

    public List<Item> items = new List<Item>();

    public int invenSize = 16;

    public void AddItem(Item _item)
    {
        if (items.Count >= invenSize)
            return;
        items.Add(_item);
        if (invenDelegate != null)
        {
            invenDelegate(1);
        }
    }
    public void RemoveItem(Item _item)
    {
        items.Remove(_item);
        if (invenDelegate != null)
        {
            invenDelegate(-1);
        }
    }
    public bool isInventoryFull()
    {
        return (items.Count >= invenSize);
    }
    public void SelectSlot(int _index)
    {
        if (selectCallBack != null)
        {
            selectCallBack(_index);
        }
    }
}
