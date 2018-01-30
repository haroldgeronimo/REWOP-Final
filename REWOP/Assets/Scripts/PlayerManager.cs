using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
    #region Singleton
    public static PlayerManager instance;

    void Awake() {
        if (!instance)
        {
            instance = this;
//     DontDestroyOnLoad(gameObject);
        }
    }

    public void Start()
    {
        if (EasySaveLoadManager.Instance.IsLoadGame)
            Load();
        else
            Debug.Log("DidNotLoadPlayerStats");
    }

    #endregion
    private void Load()
    {
        PlayerState.Load();
        Debug.Log("LoadedPLayerStats");
    }

    public GameObject player;
    public GameObject controlTapToInteract;
    public void KillPlayer() {

        StartCoroutine(DeathDelay());

    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOver");
    }

}
