using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {
    public SceneControl sc;
    public GameObject startScreen;
    public GameObject Menu;
    public GameObject loadingScreen;
    public Text progresstext;
    public Slider slider;
    public void LoadLevel(string sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(string sceneIndex) {
                AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        startScreen.SetActive(false);
        Menu.SetActive(false);
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progresstext.text = Mathf.Round(progress * 100f) + "%";
            yield return null;
        }
    }


}
