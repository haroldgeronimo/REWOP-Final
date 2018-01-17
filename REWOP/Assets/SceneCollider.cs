using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCollider : MonoBehaviour {
    private SceneControl sc;
    public string[] sceneToLoad;
    public string[] sceneToUnload;
    private void Start()
    {

        sc = SceneControl.instance;
    }
    void LoadScenes()
    {
		foreach (string scene in sceneToUnload) {
			sc.UnloadScene(scene);
		}
        StartCoroutine(BufferLoadScene());
    }

    private void OnTriggerEnter(Collider other)
    {
        LoadScenes();
    }
    IEnumerator BufferLoadScene()
    {
		foreach (string scene in sceneToLoad) {
			yield return new WaitForSeconds (1);
			sc.LoadScene (scene);
		}
    }
}
