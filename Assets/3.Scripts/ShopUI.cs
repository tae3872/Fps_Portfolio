using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    ShopManager shopManager;
    public Transform shopItems;
    public ShopSlot[] itemSlots;
    public Transform shopUI;
    public GameObject player;

    public GameObject buyButton;
    public Text nameText;
    public Text descriptionText;
    public Text priceText;

    public int selectIndex = -1;
    public bool isShopUIOpen = false;

    void Start()
    {
        shopManager = ShopManager.instance;
        shopManager.selectCallBack += SelectSlot;
        itemSlots = shopItems.GetComponentsInChildren<ShopSlot>();
        SetShopSlots();
        ResetItemInfoUI();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Toggle();
        }
    }
    void Toggle()
    {
        if (isShopUIOpen)
        {
            CloseShopUI();
        }
        else
        {
            OpenShopUI();
        }
    }
    void OpenShopUI()
    {
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponentInChildren<FirePistol>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        shopUI.GetComponent<Animation>().Play("ShopUIOpen");
        isShopUIOpen = true;
    }
    public void CloseShopUI()
    {
        player.GetComponent<FirstPersonController>().enabled = true;
        player.GetComponentInChildren<FirePistol>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        shopUI.GetComponent<Animation>().Play("ShopUIClose");
        isShopUIOpen = false;
        ResetItemInfoUI();
    }
    void SetShopSlots()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].SetShopSlot(shopManager.shopItems[i], i);
        }
    }
    public void SelectSlot(int index)
    {
        if (selectIndex == index)
        {
            ResetItemInfoUI();
            return;
        }
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
        SetItemInfoUI();
    }
    void SetItemInfoUI()
    {
        buyButton.SetActive(true);
        nameText.text = itemSlots[selectIndex].item.name;
        descriptionText.text = itemSlots[selectIndex].item.description;
        priceText.text = itemSlots[selectIndex].item.ShopPrice + " Gold";
    }
    void ResetItemInfoUI()
    {
        buyButton.SetActive(false);
        nameText.text = "";
        descriptionText.text = "";
        priceText.text = "";
        if (selectIndex >= 0)
        {
            itemSlots[selectIndex].SetSelectImage(false);
            selectIndex = -1;
        }
    }
    public void BuyItem()
    {
        int price = itemSlots[selectIndex].item.ShopPrice;
        if (PlayerStats.instance.UseGold(price))
        {
            Inventory.Instance.AddItem(itemSlots[selectIndex].item);
            StartCoroutine(ShowMessage(itemSlots[selectIndex].item.name + " 구매 성공"));
        }
        else
            StartCoroutine(ShowMessage("소지금이 부족합니다."));
    }
    IEnumerator ShowMessage(string msg)
    {
        ResetItemInfoUI();
        descriptionText.text = msg;
        yield return new WaitForSeconds(1.5f);
        descriptionText.text = "";
    }
}
