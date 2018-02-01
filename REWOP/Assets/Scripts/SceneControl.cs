                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour {
    public static SceneControl instance;
    public List<string> loadedScene;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
         //   DontDestroyOnLoad(gameObject);
        }
    }
 
    public void LoadScene(string sceneName)
    {
      
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            if (!loadedScene.Contains(sceneName))
                loadedScene.Add(sceneName);
                Debug.Log(sceneName + " has been loaded");
            MusicManager.instance.LevelWasLoaded(sceneName);
     
        }

     
    }

    public void UnloadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            StartCoroutine(Unloader(sceneName));
    
        }
           

       
    }

   IEnumerator Unloader(string Name)
    {
        yield return new WaitForSeconds(0.01f);

        QuestManager.instance = null;
        SceneManager.UnloadSceneAsync(Name);
        if (loadedScene.Contains(Name))
            loadedScene.Remove(Name);
    
    }

}
