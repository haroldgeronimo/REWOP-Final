using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicManager : MonoBehaviour {

    public List<MusicLibrary> musicLib;

    string sceneName;
   public static MusicManager instance;

    private void Awake()
    {
       if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        LevelWasLoaded("MainMenu");
    }


    public void LevelWasLoaded(string loadedScene)
    {
        string newSceneName = loadedScene;
        if (newSceneName != sceneName)
        {
            sceneName = newSceneName;
            Debug.Log("Playing music for " + sceneName);
            //  Invoke("PlayMusic", .2f);
            PlayMusic();
        }
    }

    void PlayMusic()
    {
      
        AudioClip clipToPlay = null;
        foreach (MusicLibrary entry in musicLib)
        {
            if (entry.sceneName == sceneName)
                clipToPlay = entry.music;
        }
  
        if (clipToPlay != null)
        {
            Debug.Log("Playing " + clipToPlay.name);
            AudioManager.instance.PlayMusic(clipToPlay, 2);
            Invoke("PlayMusic", clipToPlay.length);
        }

    }
    [System.Serializable]
    public class MusicLibrary
    {
        public AudioClip music;
        public string sceneName;
    }
}
