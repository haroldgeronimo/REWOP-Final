using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;
    Animator animate;
	// Use this for initialization
	void Start () {
 target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        animate = GetComponentInChildren<Animator>();
            
            }
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius) {
         //   Debug.Log("Player in Range");
            agent.SetDestination(target.position);
        }

        if (distance <= agent.stoppingDistance) {
            CharStats targetStats = target.GetComponent<CharStats>();
          
            FaceTarget();
            if (targetStats != null)
            {
                animate.SetBool("IsAttacking", true);
                combat.Attack(targetStats);


            }
            else
            {
                animate.SetBool("IsAttacking", false);
            }

        }
        else
        {
            animate.SetBool("IsAttacking", false);
        }


    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
