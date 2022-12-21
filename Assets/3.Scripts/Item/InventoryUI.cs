using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inven;
    public GameObject invenUI;
    public GameObject iteminfoUI;
    public Transform player;

    public GameObject itemSlotPrefab;
    public List<ItemSlot> itemSlots = new List<ItemSlot>();
    public Transform items;
    public int selectIndex = -1;
    EquipmentUI equipmentUI;

    public bool isOpenInvenUI = false;
    public bool isOpenItemInfoUI = false;

    public void InitInven()
    {
        if (inven.items.Count != 0)
        {
            for (int i = 0; i < inven.items.Count; i++)
            {
                GameObject Slot = Instantiate(itemSlotPrefab) as GameObject;
                Slot.transform.SetParent(items.transform, false);
                ItemSlot itemSlot = Slot.GetComponent<ItemSlot>();
                itemSlots.Add(itemSlot);
            }
            for (int i = 0; i < itemSlots.Count; i++)
            {
                itemSlots[i].ResetItemSlot();
            }
            for (int i = 0; i < inven.items.Count; i++)
            {
                itemSlots[i].SetItemSlot(inven.items[i], i);
            }
        }
    }
    void Start()
    {
        inven = Inventory.Instance;
        inven.invenDelegate = UpdateInventory;
        inven.selectCallBack = SelectSlot;
        equipmentUI = this.GetComponent<EquipmentUI>();
        InitInven();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Toggle();
        }
    }
    public void Toggle()
    {
        if (isOpenInvenUI)
        {
            StartCoroutine(CloseInvenUI());
        }
        else
        {
            OpenInvenUI();
        }
    }
    public void OpenInvenUI()
    {
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponentInChildren<FirePistol>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        invenUI.GetComponent<Animation>().Play("InvenUIOpen");
        isOpenInvenUI = true;
    }
    IEnumerator CloseInvenUI()
    {
        if (isOpenItemInfoUI || equipmentUI.isOpenEquipUI)
        {
            CloseItemInfoUI();
            equipmentUI.CloseEquipmentUI();
            equipmentUI.CloseItemInfoUI();
        }
        yield return new WaitForSeconds(0.33f);

        iteminfoUI.GetComponent<Animation>().Play("itemInfoUIClose2");
        equipmentUI.CloseEquipmentUI2();

        player.GetComponent<FirstPersonController>().enabled = true;
        player.GetComponentInChildren<FirePistol>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        invenUI.GetComponent<Animation>().Play("InvenUIClose");
        isOpenInvenUI = false;

    }
    public void UpdateInventory(int add)
    {
        if (add == 1)
        {
            GameObject Slot = Instantiate(itemSlotPrefab) as GameObject;
            Slot.transform.SetParent(items.transform, false);
            ItemSlot itemSlot = Slot.GetComponent<ItemSlot>();
            itemSlots.Add(itemSlot);
        }
        else if (add == -1)
        {
            itemSlots.RemoveAt(0);
            Destroy(items.transform.GetChild(0).gameObject);
        }
        for (int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i].ResetItemSlot();
        }
        for (int i = 0; i < inven.items.Count; i++)
        {
            itemSlots[i].SetItemSlot(inven.items[i], i);
        }
    }

    public void SelectSlot(int _index)
    {
        if (selectIndex == _index)
        {
            CloseItemInfoUI();
            return;
        }
        equipmentUI.CloseItemInfoUI();
        selectIndex = _index;
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (selectIndex == i)
            {
                itemSlots[i].SetSelectImage(true);
            }
            else
            {
                itemSlots[i].SetSelectImage(false);
            }
        }
        OpenItemInfoUI();
    }
    public void DeselectSlot()
    {
        if (selectIndex == -1)
            return;
        itemSlots[selectIndex].SetSelectImage(false);
        selectIndex = -1;
    }
    void OpenItemInfoUI()
    {
        if (!isOpenItemInfoUI)
        {
            iteminfoUI.GetComponent<Animation>().Play("itemInfoUIOpen");
        }
        iteminfoUI.GetComponent<ItemInfoUI>().UpdateItemInfo(itemSlots[selectIndex].item);
        isOpenItemInfoUI = true;
    }
    public void CloseItemInfoUI()
    {
        if (selectIndex == -1)
            return;
        if (selectIndex < itemSlots.Count)
        {
            itemSlots[selectIndex].SetSelectImage(false);
        }
        selectIndex = -1;
        iteminfoUI.GetComponent<Animation>().Play("itemInfoUIClose");
        isOpenItemInfoUI = false;
    }
    public void UseItem()
    {
        Debug.Log(itemSlots[selectIndex].item.name + "를 사용하였다");
        itemSlots[selectIndex].item.Use();
        if (itemSlots[selectIndex].item.isQuest)
        {
            itemSlots[selectIndex].SetSelectImage(false);
            selectIndex = -1;
            iteminfoUI.GetComponent<Animation>().Play("itemInfoUIClose");
            isOpenItemInfoUI = false;
        }
        else
        {
            inven.RemoveItem(itemSlots[selectIndex].item);
            CloseItemInfoUI();
        }
    }
    public void SellItem()
    {
        Debug.Log(itemSlots[selectIndex].item.name + "를 제거하였다");
        PlayerStats.instance.AddGold(itemSlots[selectIndex].item.SellPrice);
        inven.RemoveItem(itemSlots[selectIndex].item);
        CloseItemInfoUI();
    }
}
