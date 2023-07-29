using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Playables;

using System;



// 수정 예정

[Serializable]
public class GameData
{
    public int TotalGold;
    public int TotalShell;
    // 여기에 더 많은 게임 데이터를 추가
}

public class SaveSystem
{
    private static readonly string SAVE_PATH = Application.persistentDataPath + "/save.dat";

    public static void SaveData(GameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(SAVE_PATH, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadData()
    {
        if (File.Exists(SAVE_PATH))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(SAVE_PATH, FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + SAVE_PATH);
            return null;
        }
    }

}