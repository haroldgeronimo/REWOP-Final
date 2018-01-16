using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    CharacterCombat combat;
    Transform target;
    Animator playerAnimator;
    public Attack attack;
    public float lookRadius = 1.5f;
    Vector3 lookOffset = new Vector3(0f, 0f, 0.7f);
    // Use this for initialization
    private void OnDrawGizmosSelected()
    {
      
            Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + lookOffset, lookRadius);
    }
    void Start()
    {
        combat = GetComponent<CharacterCombat>();
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (attack.IsAttacking) {

            playerAnimator.Play("Attack");
            AttackAct();
        }
    }
    /*
    private List<GameObject> GOs;
    void OnTriggerEnter(Collider Other)
    {
        GOs.Add(Other.gameObject);
    }*/
    public void AttackAct()
    { // invoke on button

        CollisionObjects ColObj = this.gameObject.transform.GetChild(1).GetComponent<CollisionObjects>();
        List<CharStats> targetStats = new List<CharStats>();
        if (ColObj != null)
        {
            foreach (GameObject item in ColObj.items)
            {
                if (item == null)
                {
                    ColObj.items.Remove(item);
                    continue;
                }
                targetStats.Add(item.GetComponent<CharStats>());
                //if (targetStats != null)

            }
            Debug.Log("Number of CharStats: " + targetStats.Count);
   
                combat.MultiAttack(targetStats);
       
        }
    }
    #region AttackMethods
    private bool AttackByRaycast() {
        Transform candidateTarget = GetInfront();
        if (candidateTarget != null)
            if (candidateTarget.gameObject.tag == "Enemy")
            {
                target = candidateTarget;
                float distance = Vector3.Distance(target.position, rb.transform.position);
                if (distance <= lookRadius)
                {
                    CharStats targetStats = target.GetComponent<CharStats>();
                    if (targetStats != null)
                        combat.Attack(targetStats);
                    return true;
                }
                return false;
            }
         
           
        return false;
    }

    /*
    private void AttackBySphere() {
        List<Transform> candidateTarget = GetEnemiesAround(lookRadius);
    
        if (candidateTarget != null)
            foreach(var Etrans in candidateTarget)
            if (Etrans.gameObject.tag == "Enemy")
            {
                         target = Etrans;
                    float distance = Vector3.Distance(Etrans.position +lookOffset, rb.transform.position);
                if (distance <= lookRadius)
                {
                      
                    CharStats targetStats = target.GetComponent<CharStats>();
                    if (targetStats != null)
                        combat.Attack(targetStats);

                }



            }

    }*/
    #endregion
    #region EnemySearchMethods

    public GameObject raycastObject;
    private Transform GetInfront()
    {
        RaycastHit ObjectHit;
        Vector3 offset = new Vector3(0, 1, 0);
        Vector3 fwd = raycastObject.transform.TransformDirection(Vector3.forward + offset);
        Debug.DrawRay(raycastObject.transform.position, fwd * 50, Color.green);

        if (Physics.Raycast(raycastObject.transform.position, fwd, out ObjectHit, 50))
        {
         //   Debug.Log(ObjectHit.transform.name);
            return ObjectHit.transform;
        }
        return null;
       }
    /*
  Collider[] hitColliders;
    public List<Transform> Objarray = null;
    List<Transform> GetEnemiesAround(float radius)
    {
        
                Vector3 center = transform.position;
        int i = 0;
       hitColliders = Physics.OverlapSphere(center, radius);
        Objarray = null;
        foreach (var obj in hitColliders)
        {
            Debug.Log(obj.transform.name );
            if (obj.transform.tag == "Enemy")
            {
                //Debug.Log(obj.transform.name);
                if(obj.transform!=null)
                Objarray.Add(obj.GetComponent<Transform>());
            
            }
       
        }
       

        if (i > 0) return Objarray; else return null;
       
    }
    */
#endregion
}
