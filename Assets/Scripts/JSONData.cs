using MessagePack;
using System;
using System.IO;
using UnityEngine;

public class JSONData : MonoBehaviour
{
    [MessagePackObject]
    [Serializable]
    public class Data
    {
        [Key(0)]
        public int TotalGamesPlayed;
        [Key(1)]
        public int XWinCount;
        [Key(2)]
        public int OWinCount;
        [Key(3)]
        public int TotalGamesPlayedWithAI;
        [Key(4)]
        public int WinCount;
        [Key(5)]
        public int LooseCount;
        [Key(6)]
        public float Volume;
    }

    public Data gameData = new Data();

    private string _path = Application.streamingAssetsPath + "/JSON.txt";

    private void Start()
    {
        try
        {
            ReadJSON();
        }
        catch
        {
            // No data found
        }
    }

    public void SaveJSON()
    {
        byte[] bytes = MessagePackSerializer.Serialize(gameData);
        File.WriteAllBytes(_path, bytes);
    }

    public void ReadJSON()
    {
        byte[] readBytes = File.ReadAllBytes(_path);
        Data data = MessagePackSerializer.Deserialize<Data>(readBytes);
        gameData = data;
    }
}
