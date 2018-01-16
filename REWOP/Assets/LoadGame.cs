using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadGame : MonoBehaviour {
    
	void Start () {

        if (EasySaveLoadManager.Instance.isSaveExists("IsSaved"))
            Debug.Log("Meron!");    

            this.GetComponent<Button>().interactable = (EasySaveLoadManager.Instance.isSaveExists("IsSaved"));
   
	}
	

    void DoLoadGame()
    {
        SaveLoadManager.LoadData();
        List<string> scenes = SaveLoadManager.Instance.gameData.loadedScenes;
      
        

    }
}
