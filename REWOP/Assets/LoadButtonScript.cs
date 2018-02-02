using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButtonScript : MonoBehaviour {
    public LevelLoader loader;
    public string LevelToLoad;
    private Button b;
    private void Start()
    {
        b = this.GetComponent<Button>();
        b.onClick.AddListener(delegate () { LoadGame(); });
    }
    void LoadGame()
    {
        if (EasySaveLoadManager.Instance != null)
        {
            EasySaveLoadManager.Instance.LoadData();
            loader.LoadLevel(LevelToLoad);
        }
    }
}
