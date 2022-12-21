using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSave : MonoBehaviour
{
    void Start()
    {
        AutoSaveData();
    }
    void AutoSaveData()
    {
        PlayerStats.instance.sceneNum = SceneManager.GetActiveScene().buildIndex;
        SaveLoad.SaveData();
    }
}
