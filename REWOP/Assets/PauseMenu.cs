using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Toggle();
        }
    }
    public void Toggle()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
        if (pauseUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            if(!FindObjectOfType<DialogueManager>().IsPauseGame)
            Time.timeScale = 1f;
        }

     
    }
    public void Menu()
    {
        Toggle();
        MusicManager.instance.LevelWasLoaded("MainMenu");
        SceneManager.LoadScene(("MainMenu"),LoadSceneMode.Single);

    }

}
