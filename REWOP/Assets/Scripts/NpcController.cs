using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NpcController : MonoBehaviour
{
    public float lookRadius = .5f;
    public float enemyLookRadius = 20f;
    public enum State
    {
        Idle,
        Wander,
        Inspect,
        Talk,
        Run,
        Null
    }
    public State state;
    Vector3 target;
    NavMeshAgent agent;
    public float minIdleTime = 3;
    public float maxIdleTime = 10;
   public float WanderDistance = 40f;
    //CharacterCombat combat;
    //Animator animate;
    // Use this for initialization
    void Start()
    {
        target = RandomNavSphere(transform.position, WanderDistance, -1);
        agent = GetComponent<NavMeshAgent>();
        state = State.Idle;
        //combat = GetComponent<CharacterCombat>();
        //animate = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Idle)
        {
            state = State.Null;
            StartCoroutine(IdleTime());
        }
        else if (state == State.Wander)
               {

            Wander();
        }
        //Debug.Log(state.ToString());
    }

    void FaceTarget()
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }

    void Wander()
    {

        float distance = Vector3.Distance(target, transform.position);
        float wanderD = Random.Range(2, WanderDistance);
        target = RandomNavSphere(transform.position, wanderD, -1);
        if (distance <= lookRadius) { 
        
            FaceTarget();
            state = State.Idle;
          //  Debug.Log("Villager stopped");
        }
        agent.SetDestination(target);
    }

    IEnumerator IdleTime()
    {
        yield return new WaitForSeconds(Random.Range(minIdleTime, maxIdleTime));
        state = State.Wander;
       // Debug.Log("Villager Wanders");
        yield return null;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    void Inspect(GameObject inspectObject)
    {

    }

}