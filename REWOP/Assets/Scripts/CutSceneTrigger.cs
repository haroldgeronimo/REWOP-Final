using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTrigger : MonoBehaviour {
    private CutSceneManager CSM;
    public int cutSceneNumber;
    void Start () {
        CSM = CutSceneManager.instance;
       
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Nexo" || PlayerManager.instance.player)
        {

   
            if (!CSM.sceneCompleted[cutSceneNumber])
            {

                StartCutScene();
            }
        }
    }

    private void StartCutScene()
    {
        CSM.cutSceneObject[cutSceneNumber].StartCutscene();
    }
}
