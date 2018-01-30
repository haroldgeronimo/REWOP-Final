using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadGameOver : MonoBehaviour, IPointerDownHandler {
    public void OnPointerDown(PointerEventData eventData)
    {
        EasySaveLoadManager.Instance.LoadData();
    }

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
	}

    
}
