﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour {
    public static CutSceneManager instance;
   public Camera mainCamera;
    public CutsceneObject[] cutSceneObject;
    public bool[] sceneCompleted;
    public int ActiveCutScene = -1;
    // Use this for initialization
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            
        }
    }
    void Start () {
        if(!EasySaveLoadManager.Instance.IsLoadGame)
        sceneCompleted = new bool[cutSceneObject.Length];
   
        mainCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
