using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    public Text nameText;
    public Text priceText;
    public Text descriptionText;

    public GameObject useButton;
    public GameObject sellButton;
    public GameObject equipButton;
    public GameObject unEquipButton;

    public void UpdateItemInfo(Item selectItem)
    {
        ResetButtons();
        nameText.text = selectItem.name;
        descriptionText.text = selectItem.description;
        priceText.text = selectItem.SellPrice + "Gold";

        if (selectItem.itemType == ItemType.Equip)
        {
            equipButton.SetActive(true);
            sellButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(true);
            sellButton.SetActive(true);
        }
    }
    public void UpdateItemInfo(Equip selectItem)
    {
        ResetButtons();
        nameText.text = selectItem.name;
        descriptionText.text = selectItem.description;
        priceText.text = "";

        unEquipButton.SetActive(true);
    }
    void ResetButtons()
    {
        useButton.SetActive(false);
        sellButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
    }
}
