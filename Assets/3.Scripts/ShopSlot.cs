using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Item item;
    public int slotIndex;
    public GameObject iconImage;
    public GameObject selectImage;

    public void SetShopSlot(Item _item, int index)
    {
        if (_item == null)
            return;
        item = _item;
        slotIndex = index;
        iconImage.SetActive(true);
        iconImage.GetComponent<Image>().sprite = item.iconImage;
    }
    public void ResetShopSlot()
    {
        item = null;
        iconImage.SetActive(false);
        iconImage.GetComponent<Image>().sprite = null;
    }
    public void SelectSlot()
    {
        if (item == null)
            return;
        ShopManager.instance.SelectSlot(slotIndex);
    }
    public void SetSelectImage(bool isSelect)
    {
        selectImage.SetActive(isSelect);
    }
}
