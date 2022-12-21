using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item;
    int slotIndex;

    public GameObject iconImage;
    public GameObject selectImage;

    public void SetItemSlot(Item _item, int _index)
    {
        item = _item;
        slotIndex = _index;
        iconImage.SetActive(true);
        iconImage.GetComponent<Image>().sprite = item.iconImage;
    }
    public void ResetItemSlot()
    {
        item = null;
        iconImage.SetActive(false);
        iconImage.GetComponent<Image>().sprite = null;
    }
    public void SelectSlot()
    {
        if (item == null)
            return;
        Inventory.Instance.SelectSlot(slotIndex);
    }
    public void SetSelectImage(bool isSelect)
    {
        selectImage.SetActive(isSelect);
    }
}
