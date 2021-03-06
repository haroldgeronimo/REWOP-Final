﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuestManager : MonoBehaviour {
    public static QuestManager instance;
    public QuestObject[] quests;
    public bool[] questCompleted;
    private DialogueManager DM;
   public int activeQuest;
   
    // Use this for initialization
    private void Awake()
    {

        if (instance == null)
            instance = this;
    }
    void Start () {
        DM = FindObjectOfType<DialogueManager>();
        questCompleted = new bool[quests.Length];
        if (EasySaveLoadManager.Instance.IsLoadGame)
        {
            QuestState.Load();
            CutSceneState.Load();
        }
        StartCoroutine(LateStart());
      //  EasySaveLoadManager.Instance.IsLoadGame = false;
    }
	IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.5f);
        if (EasySaveLoadManager.Instance.IsLoadGame) {
            EasySaveLoadManager.Instance.IsLoadGame = false;        }

    }
    public void ShowStartDialogue(Dialogue dialogue) {
        DM.StartDialogue(dialogue);
    }

    public void ShowEndDialogue(Dialogue dialogue)
    {
        DM.StartDialogue(dialogue);
      
       
    }


}
