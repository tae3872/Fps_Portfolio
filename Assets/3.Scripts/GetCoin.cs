using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoin : MonoBehaviour
{
    public string type;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (type)
            {
                case "Gold":
                    PlayerStats.instance.AddGold(50);
                    break;
                case "Silver":
                    PlayerStats.instance.AddGold(20);
                    break;
                case "Copper":
                    PlayerStats.instance.AddGold(10);
                    break;
                case "Quest":
                    if (QuestManager.instance.currentQuest.goal.questState == QuestState.Accept)
                    {
                        QuestManager.instance.UpdateItemCollect();
                    }
                    break;
            }
            AudioManager.instance.Play("Coin");
            Destroy(gameObject);
        }
    }
}
