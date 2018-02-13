using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicManager : MonoBehaviour {

    public List<MusicLibrary> musicLib;
    public List<MusicStateLibrary> musicStateLib;
    public AudioClip defaultMusic;
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
            defaultMusic = clipToPlay;
            Invoke("PlayMusic", clipToPlay.length);
        }

    }

    public void PlayMoodMusicByState(MoodState mood)
    {
        List<AudioClip> musicList = new List<AudioClip>();
        //get all music with that mood
        foreach (MusicStateLibrary musicState in musicStateLib)
        {
            if (musicState.mood == mood) {
                musicList.Add(musicState.music);
            }

        }

        //randomly pick a music from the musicList
        AudioClip clipToPlay = null;
        clipToPlay = musicList[Random.Range(0,musicList.Count)];
        AudioManager.instance.PlayMusic(clipToPlay, 2);
    }


    public void PlayMoodMusicByTitle(string title)
    {
        AudioClip clipToPlay = null;
        //search titles in the library
        foreach (MusicStateLibrary musicState in musicStateLib)
        {
            if (musicState.title == title)
            {
                clipToPlay = musicState.music;
                break;
            }
        }

        if (clipToPlay == null) Debug.LogWarning("You are trying to play an unknown title");
        

     if(clipToPlay != null)
        AudioManager.instance.PlayMusic(clipToPlay, 2);
    }


    public void PlayDefaultMusic()
    {
        if (defaultMusic == null) return;

        AudioManager.instance.PlayMusic(defaultMusic,2);
    }
    [System.Serializable]
    public class MusicLibrary
    {
        public AudioClip music;
        public string sceneName;
    }
    [System.Serializable]
    public class MusicStateLibrary
    {

        public AudioClip music;
        public string title;
        public MoodState mood;
    }

   
}
public enum MoodState
{
    Battle,
    Mystery,
    Thrill,
    Chill,
    Inspiring,
    Epic,
    Dramatic,
    Sad,
    skrattar_du_förlorar_du
}