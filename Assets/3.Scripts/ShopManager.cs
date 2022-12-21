using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    #region Singleton
    public static ShopManager instance;
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
    public Item[] shopItems;
    public delegate void SelectSlotDelegate(int index);
    public SelectSlotDelegate selectCallBack;

    public void SelectSlot(int index)
    {
        if (selectCallBack != null)
        {
            selectCallBack(index);
        }
    }
}
