using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour {
    NavMeshAgent agent;
    Animator animator;
    const float smoothTime = 0.5f;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, smoothTime, Time.deltaTime);
	}

    public void EnemyHit()
    {
        animator.SetBool("IsHit", true);
        
    }

    public void EnemyNotHit()
    {
        animator.SetBool("IsHit", false);

    }
    public void EnemyDie()
    {
        animator.SetBool("IsDie", true);
    }
}
