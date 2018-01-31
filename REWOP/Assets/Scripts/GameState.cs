using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    public int World;
    public int Stage;
    private bool IsPaused;
    private static GameState instance;

    public static GameState Instance
    {
        get
        {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<GameState>();

            }
            return GameState.instance;
        }
        
    }

    public bool IsPaused1
    {
        get
        {
            return IsPaused;
        }


    }

    public void TogglePause() {
        IsPaused = !IsPaused1;
    }

}
