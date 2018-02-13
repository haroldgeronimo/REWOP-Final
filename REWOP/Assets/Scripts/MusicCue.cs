using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCue : MonoBehaviour {

    public void StartMoodMusicByTitle(string title)
    {

        MusicManager.instance.PlayMoodMusicByTitle(title);
    }

    public void StartMoodMusicByType(MoodState ms)
    {

        MusicManager.instance.PlayMoodMusicByState(ms);
    }

    public void StartDeafultMusic()
    {

        MusicManager.instance.PlayDefaultMusic();
    }
}
