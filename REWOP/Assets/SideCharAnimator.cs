using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SideCharAnimator : MonoBehaviour {

    NavMeshAgent agent;
    Animator animator;
    const float smoothTime = 0.5f;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, smoothTime, Time.deltaTime);
    }

    public void SideCharHit()
    {
        animator.SetBool("IsHit", true);

    }

    public void SideCharNotHit()
    {
        animator.SetBool("IsHit", false);

    }
    public void SideCharDie()
    {
        animator.SetBool("IsDie", true);
    }
}
