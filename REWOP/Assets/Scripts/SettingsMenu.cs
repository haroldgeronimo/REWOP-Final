using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    private ModalDialogueSystem Modal;
    public GameObject gameSettings;
    public Button gameResetButton;
    private void Awake()
    {
        Modal = ModalDialogueSystem.instance();
    }
    private void Start()
    {
    
    }
    public void SetQuality (int qualityIndex)
	{
		QualitySettings.SetQualityLevel (qualityIndex);
	}
    public void ResetGame() {
        Modal.ShowYesNo("You are about to erase your current progress in the game. Do you want to continue?", () => ResetAll(), () => DoNothing());

    }
   void DoNothing()
    {

    }
    void ResetAll()
    {
        EasySaveLoadManager.Instance.deleteSave();
        Modal.ShowOk("You have just erased the game saved data!",DoNothing,"Attention!");
        RefreshButton();
        gameSettings.SetActive(false);

    }
    void RefreshButton()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
