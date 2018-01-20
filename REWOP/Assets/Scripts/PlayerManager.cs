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

    private void Start()
    {
        if (EasySaveLoadManager.Instance.IsLoadGame)
            Load();
    }

    #endregion
    private void Load()
    {
        PlayerState.Load();
    }

    public GameObject player;

    public void KillPlayer() {

        StartCoroutine(DeathDelay());

    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(3);
        Time.timeScale = 0;
    }

}
