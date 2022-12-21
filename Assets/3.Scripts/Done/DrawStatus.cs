using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DrawStatus : MonoBehaviour
{
    PlayerStats playerStats;

    public Image hpBar;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI goldText;

    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenseText;
    public Text grenadeNum;

    void Awake()
    {
        playerStats = PlayerStats.instance;
    }
    void Update()
    {
        if (playerStats.isDeath)
            return;
        hpBar.fillAmount = playerStats.health / playerStats.maxHealth;
        healthText.text = playerStats.health + " / " + playerStats.maxHealth;

        int needExp = playerStats.NeedExpLvUp(playerStats.level);
        expText.text = playerStats.currentExp + " / " + needExp;
        levelText.text = playerStats.level.ToString();
        goldText.text = playerStats.gold.ToString();

        attackText.text = playerStats.attack.GetValue().ToString();
        defenseText.text = playerStats.defense.GetValue().ToString();
        grenadeNum.text = playerStats.grenadeNum.ToString();
    }
}
