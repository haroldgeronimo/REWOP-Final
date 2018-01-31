using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStat : MonoBehaviour {
    public PlayerStats Health;
    public Image HealthBarVal;
    public float smoothTime = 0.3f;
    public float newPos;
        private float velocity = 0.0f;
	void Start(){
		Health = PlayerManager.instance.player.GetComponent<PlayerStats> ();//alaws pa din bawas hahaha
		if(Health == null) Debug.LogError("What the actual fact"); // erp try om nga kung alabas di lumabas pre
	}
    private float GetTargetValue() {
        return Health.currentHealth / 100f;
     
    }
	// Update is called once per frame
	void LateUpdate () {
        float newPos = Mathf.SmoothDamp(HealthBarVal.fillAmount, GetTargetValue(), ref velocity, smoothTime);
        HealthBarVal.fillAmount = newPos;

    }
}
