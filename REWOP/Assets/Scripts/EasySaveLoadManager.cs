using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        CutSceneState.Save();
        ES2SaveManager.instance.Save();
        ES2.Save(true, folder + "IsSaved");
    }
    public void LoadData()
    {
        Debug.Log("IsLoadData!");
        Debug.Log("Set load game to True for reference");
        IsLoadGame = true;
        SceneState.Load();

    }
    public bool isSaveExists(string path)
    {

        return ES2.Exists(folder + path);

    }
    public void deleteSave()
    {
        ES2.Delete(folder);
        ES2.Delete("IsSaved");
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
        ES2.Save(GameObject.FindGameObjectWithTag("Sword").GetComponent<SkinnedMeshRenderer>().enabled, folder + "Player.dat?tag=sword");
        ES2.Save(GameObject.FindGameObjectWithTag("AttackButton").GetComponent<Image>().raycastTarget, folder + "Player.dat?tag=attackBtn");
        ES2.Save(GameObject.FindGameObjectWithTag("AttackButton").GetComponent<Image>().color, folder + "Player.dat?tag=attackBtnClr");
    }
    public static void Load()
    {
        PlayerManager.instance.player.transform.position = ES2.Load<Vector3>(folder + "Player.dat?tag=position");
        PlayerManager.instance.player.transform.rotation = ES2.Load<Quaternion>(folder + "Player.dat?tag=rotation");
        PlayerManager.instance.player.GetComponent<PlayerStats>().currentHealth = ES2.Load<int>(folder + "Player.dat?tag=health");
        AchievementData.instance.Achievements = ES2.LoadList<AchievementMeta>(folder + "Player.dat?tag=achievements");
        GameObject.FindGameObjectWithTag("Sword").GetComponent<SkinnedMeshRenderer>().enabled = ES2.Load<bool>(folder + "Player.dat?tag=sword");
        GameObject.FindGameObjectWithTag("AttackButton").GetComponent<Image>().raycastTarget = ES2.Load<bool>(folder + "Player.dat?tag=attackBtn");
        GameObject.FindGameObjectWithTag("AttackButton").GetComponent<Image>().color = ES2.Load<Color>(folder + "Player.dat?tag=attackBtnClr");
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
        if(QuestManager.instance == null)
        {
            Debug.LogError("QM is null!");
            return;
        }
        ES2.Save(QuestManager.instance.questCompleted, folder + "Scenes.dat?tag=questCompleted");
        ///.Instance.savedScenes = ES2.LoadList<bool>(folder + "Quests.dat?tag=questCompleted");
    }
    public static void Load()
    {
        QuestManager.instance.questCompleted = ES2.LoadArray<bool>(folder + "Scenes.dat?tag=questCompleted");
    }
}


public static class CutSceneState
{
    public static string folder = EasySaveLoadManager.Instance.folder;
    public static void Save()
    {
        //Get Quest instance and lagay mo dun ung load hahahhah
        if (CutSceneManager.instance == null)
        {
            Debug.LogError("QM is null!");
            return;
        }
        if (CutSceneManager.instance != null)
            ES2.Save(CutSceneManager.instance.sceneCompleted, folder + "Scenes.dat?tag=sceneCompleted");
        ///.Instance.savedScenes = ES2.LoadList<bool>(folder + "Quests.dat?tag=questCompleted");
    }
    public static void Load()
    {if(CutSceneManager.instance != null)
        CutSceneManager.instance.sceneCompleted = ES2.LoadArray<bool>(folder + "Scenes.dat?tag=sceneCompleted");
    }
}
#endregion