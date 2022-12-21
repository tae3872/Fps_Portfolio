using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;
    public static int zombieNum = 0;
    public int spawnNum = 20;
    float countdown = 0;
    public int coolTime = 1;
    void Start()
    {
        zombiePrefab = Resources.Load<GameObject>("Prefabs/Zombie1");
        //CreateZombie();
    }
    void Update()
    {
        if (zombieNum < spawnNum)
        {
            if (countdown >= coolTime)
            {
                CreateZombie();
                countdown = 0;

            }
            countdown += Time.deltaTime;
        }
    }
    void CreateZombie()
    {
        int rand = Random.Range(0, spawnPoints.Length);
        GameObject zombie = Instantiate(zombiePrefab, spawnPoints[rand].position, spawnPoints[rand].rotation);
        //zombie.transform.GetComponent<Zombie>().Chaser();
        zombieNum++;
    }
}
