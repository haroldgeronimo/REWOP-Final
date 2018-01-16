using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SaveState {
    public List<string> loadedScenes = new List<string>() { "w1s1", "w1s1tos2" };

    public List<float> position = new List<float>() { 73.79f, 88.48f, 66.9f };
    public List<float> rotation = new List<float>() { 0, 0, 0, 0 };

    public int PlayerLife = 100;


}

#region Player Stats
[System.Serializable]
public class PlayerPosition : MonoBehaviour {
    public List<float> position = new List<float>() { 0, 0, 0 };
    public List<float> rotation = new List<float>() { 0, 0, 0, 0  };
}

#endregion