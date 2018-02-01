using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour {
    public List<string> sceneToUnload;
   SceneControl sc;
    private void Start()
    {

        sc = SceneControl.instance;
        ////check if this is loaded
        if (!EasySaveLoadManager.Instance.IsLoadGame)
        {
 
        StartCoroutine(LateStart(2));
       }
      
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Starting Teleport");
        PlayerManager.instance.player.transform.position = this.transform.position;

        Debug.Log("Teleport Done");
        //unload last scene
        foreach (string scene in sceneToUnload)
        {
            sc.UnloadScene(scene);
        }
    }
 }
