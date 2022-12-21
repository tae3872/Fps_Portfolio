using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    Equipment equipment;

    public GameObject equipmentUI;
    public GameObject itemInfoUI;

    public Transform equipItems;
    public EquipSlot[] itemSlots;

    InventoryUI invenUI;

    public bool isOpenEquipUI = false;
    public bool isOpenItemInfoUI = false;

    public int selectIndex = -1;

    void Start()
    {
        equipment = Equipment.instance;
        equipment.equipmentDelegate += UpdateEquipment;
        equipment.selectCallback += SelectSlot;
        invenUI = this.GetComponent<InventoryUI>();
        itemSlots = equipItems.GetComponentsInChildren<EquipSlot>();
        InitEquip();
    }
    public void InitEquip()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (true)
            {
                itemSlots[i].ResetEquipSlot();
                itemSlots[i].SetEquipSlot(equipment.items[i]);
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Toggle();
        }
    }
    void Toggle()
    {
        if (isOpenEquipUI)
        {
            CloseEquipmentUI();
        }
        else
        {
            StartCoroutine(OpenEquipmentUI());
        }
    }
    IEnumerator OpenEquipmentUI()
    {
        if (!invenUI.isOpenInvenUI)
        {
            invenUI.OpenInvenUI();
            yield return new WaitForSeconds(0.33f);
        }
        equipmentUI.GetComponent<Animation>().Play("EquipUIOpen");
        isOpenEquipUI = true;
    }
    public void CloseEquipmentUI()
    {
        equipmentUI.GetComponent<Animation>().Play("EquipUIClose");
        isOpenEquipUI = false;
    }
    public void CloseEquipmentUI2()
    {
        equipmentUI.GetComponent<Animation>().Play("EquipUIClose2");
    }

    public void UpdateEquipment(Equip oldItem, Equip newItem)
    {
        int index = 0;
        if (oldItem != null)
        {
            index = (int)oldItem.equipType;
        }
        if (newItem != null)
        {
            index = (int)newItem.equipType;
        }
        itemSlots[index].ResetEquipSlot();
        itemSlots[index].SetEquipSlot(newItem);
    }
    public void SelectSlot(int index)
    {
        if (selectIndex == index)
        {
            CloseItemInfoUI();
            return;
        }
        invenUI.CloseItemInfoUI();
        selectIndex = index;
        for (int i = 0; i < itemSlots.Length; i++)
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
    public void OpenItemInfoUI()
    {
        if (!isOpenItemInfoUI)
        {
            itemInfoUI.GetComponent<Animation>().Play("itemInfoUIOpen");
        }
        itemInfoUI.GetComponent<ItemInfoUI>().UpdateItemInfo(itemSlots[selectIndex].item);
        isOpenItemInfoUI = true;
    }
    public void CloseItemInfoUI()
    {
        if (selectIndex == -1)
            return;
        itemSlots[selectIndex].SetSelectImage(false);
        selectIndex = -1;
        itemInfoUI.GetComponent<Animation>().Play("itemInfoUIClose");
        isOpenItemInfoUI = false;
    }
    public void UnEquipment()
    {
        equipment.UnEquipItem(itemSlots[selectIndex].item);
        CloseItemInfoUI();
    }
    public void UnEquipmentAll()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == null)
            {
                continue;
            }
            equipment.UnEquipItem(itemSlots[i].item);
        }
        CloseItemInfoUI();
    }
}
