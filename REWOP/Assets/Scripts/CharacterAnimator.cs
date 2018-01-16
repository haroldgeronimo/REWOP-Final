using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour {
    PlayerMotor agent;
    Animator animator;

    const float locomotionAnimationSmoothTime = .1f;
	// Use this for initialization
	void Start () {
        agent = GetComponent<PlayerMotor>();
        animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame  
	void FixedUpdate () {
    
            float speedPercent = Mathf.Clamp(agent.walkspeed/agent.currentSpeed,0f,1f)  * agent.inputDir.magnitude;// agent.speed;
        //    Debug.Log(agent.velocity.magnitude / 500);
            animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
        //Debug.Log("SpeedPercent:" + speedPercent);   
	}
}
