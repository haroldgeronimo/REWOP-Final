﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasySaveLoadManager : MonoBehaviour {
    [HideInInspector()]
   public static EasySaveLoadManager Instance;
    public List<string> savedScenes;
    public bool IsLoadGame = false;
    public string folder = "SaveData/";

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        if(IsLoadGame)
        LoadData();
    }
    public void SaveData()
    {
        //insert all here!
        PlayerState.Save();
        SceneState.Save();
        QuestState.Save();
        ES2.Save(true, folder + "IsSaved");
    }
    public void LoadData()
    {
        Debug.Log("IsLoadData!");
        SceneState.Load();
        IsLoadGame = true;
 
     
    }
    public bool isSaveExists(string path)
    {

        return ES2.Exists(folder + path);

    }
}

#region staticGameObjects
public static class PlayerState {
    public static string folder = EasySaveLoadManager.Instance.folder;
    public static void Save()
    {
        ES2.Save(PlayerManager.instance.player.transform.position, folder + "Player.dat?tag=position");
        ES2.Save(PlayerManager.instance.player.transform.rotation, folder + "Player.dat?tag=rotation");
        ES2.Save(PlayerManager.instance.player.GetComponent<PlayerStats>().currentHealth, folder + "Player.dat?tag=health");
        ES2.Save(AchievementData.instance.Achievements, folder + "Player.dat?tag=achievements");
    }
    public static void Load()
    {
        PlayerManager.instance.player.transform.position = ES2.Load<Vector3>(folder + "Player.dat?tag=position");
        PlayerManager.instance.player.transform.rotation = ES2.Load<Quaternion>(folder + "Player.dat?tag=rotation");
        PlayerManager.instance.player.GetComponent<PlayerStats>().currentHealth = ES2.Load<int>(folder + "Player.dat?tag=health");
        AchievementData.instance.Achievements = ES2.LoadList<AchievementMeta>(folder + "Player.dat?tag=achievements");
    }
    
}

public static class SceneState
{
    public static string folder = EasySaveLoadManager.Instance.folder;
    public static List<string> scenestoLoad;
    public static void Save()
    {
        ES2.Save(SceneControl.instance.loadedScene, folder + "Scenes.dat?tag=loadedScenes");
    }
    public static void Load()
    {
       EasySaveLoadManager.Instance.savedScenes = ES2.LoadList<string>(folder + "Scenes.dat?tag=loadedScenes");
        foreach(string scene in ES2.LoadList<string>(folder + "Scenes.dat?tag=loadedScenes"))
        {
            Debug.Log("Saved " + scene);
        }
    }

}

public static class QuestState
{
    public static string folder = EasySaveLoadManager.Instance.folder;
    public static void Save()
    {
        //Get Quest instance and lagay mo dun ung load hahahhah
        ES2.Save(QuestManager.instance.questCompleted, folder + "Scenes.dat?tag=questCompleted");
        ///.Instance.savedScenes = ES2.LoadList<bool>(folder + "Quests.dat?tag=questCompleted");
    }
    public static void Load()
    {
        QuestManager.instance.questCompleted = ES2.LoadArray<bool>(folder + "Scenes.dat?tag=questCompleted");
    }
}
#endregion