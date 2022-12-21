using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DrawCard : MonoBehaviour
{
    public Card[] cards;
    public int cardIndex;

    public Text nameText;
    public Text descriptionText;
    public TextMeshProUGUI manaCostText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI healthText;

    public Image artWork;

    void Start()
    {
        UpdateCard();
    }
    void UpdateCard()
    {
        Card card = cards[cardIndex];
        nameText.text = card.name;
        descriptionText.text = card.description;
        manaCostText.text = card.manaCost.ToString();
        attackText.text = card.attack.ToString();
        healthText.text = card.health.ToString();
        artWork.sprite = card.artImage;
    }
}
