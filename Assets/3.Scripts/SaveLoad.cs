using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad
{
    public static void SaveData()
    {
        string path = Application.persistentDataPath + "/pDat.arr";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(path, FileMode.Create);
        PlayData playData = new PlayData();

        formatter.Serialize(fs, playData);
        fs.Close();
    }
    public static PlayData LoadData()
    {
        string path = Application.persistentDataPath + "/pDat.arr";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            PlayData playData = formatter.Deserialize(fs) as PlayData;

            fs.Close();

            return playData;
        }
        else
        {
            return null;
        }
    }
}
