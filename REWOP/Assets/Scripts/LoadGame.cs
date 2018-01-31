using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadGame : MonoBehaviour {
    
	void Start () {


        initMenu();
               LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent.GetComponent<RectTransform>());


    }

    public void initMenu()
    {
        if (EasySaveLoadManager.Instance.isSaveExists("IsSaved"))
        {
            this.transform.GetChild(0).gameObject.SetActive(false);//set load game to show on menu
        }
        else
        {
            this.transform.GetChild(1).gameObject.SetActive(false);//set start game to show on menu
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent.GetComponent<RectTransform>());
    }
	

    void DoLoadGame()
    {
        SaveLoadManager.LoadData();
        List<string> scenes = SaveLoadManager.Instance.gameData.loadedScenes;
      
        

    }
}
