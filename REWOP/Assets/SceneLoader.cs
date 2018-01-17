using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour {
   public SceneControl sc;
    public static SceneLoader instance;
    public List<string> scenesToLoad;

    private void Awake()
    {
        sc = SceneControl.instance;
        instance = this;
    }
    private void Start()
    {
        
        if (EasySaveLoadManager.Instance.IsLoadGame)
        {
            Debug.Log("Load saved scenes");
           

            foreach (string scene in EasySaveLoadManager.Instance.savedScenes)
            {
                Debug.Log(scene + " must be loaded");
            }

            StartCoroutine(loadSceneCoroutine(EasySaveLoadManager.Instance.savedScenes));
        }
        else
        StartCoroutine(loadSceneCoroutine(scenesToLoad));
     
      //  SaveLoadManager.Instance.IsGameLoaded = false;
    }
  
    private IEnumerator loadSceneCoroutine(List<string> scenes)
    {
        foreach (string scene in scenes)
        {
            yield return new WaitForEndOfFrame();
            sc.LoadScene(scene);
   
      
        }
       
        yield return null;
    }
}
