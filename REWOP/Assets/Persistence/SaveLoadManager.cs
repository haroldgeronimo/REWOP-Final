using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Rewop.Systems;
public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; set; }
    public SaveState gameData = new SaveState();
    public bool IsGameLoaded = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        LoadData();
    }
    public static void SaveData()
    {
        //string serializedStr = FileManager.XmlSerialize(SaveLoadManager.Instance.gameData);
        //FileManager.WriteFileToPath("Save.xml",
        //    Application.persistentDataPath,
        //    serializedStr
        //    );
    }
    public static void LoadData()
    {
        string serializedData = FileManager.ReadFileFromPath(Application.persistentDataPath + "/Save.xml");
        if (serializedData == "")
        {
            SaveLoadManager.Instance.gameData = new SaveState();
            SaveData();
        }
        else
        {
            Instance.gameData = FileManager.XmlDeserialize<SaveState>(serializedData);
        }
    }

    private void Update()
    {
        if (PlayerManager.instance != null)
        {
            Transform pRect = PlayerManager.instance.player.transform;
            gameData.position = new List<float>() { pRect.position.x, pRect.position.y, pRect.position.z };
            gameData.rotation = new List<float>() { pRect.rotation.x, pRect.rotation.y, pRect.rotation.z, pRect.rotation.w };
        }
    }
    public bool isSaveExists()
    {
        return (FileManager.IsExist(Application.persistentDataPath + "/Save.xml"));
    }
    public void SetLoad()
    {
        IsGameLoaded = true;
    }
}