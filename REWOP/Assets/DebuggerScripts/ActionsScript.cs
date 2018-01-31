using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBlocks;

public partial class ActionsScript : MonoBehaviour {
    public Animator animator;
    public List<ActionHandler> actions;
    public Transform combatPlace;
    public Transform idlePlace;
    public Transform entityTransform;
    public PlayerStats playerStats;
    public BossStats bossStats;
    public float moveSpeed;
    public ActionStates currentAction;


	public void Start(){
		playerStats = PlayerManager.instance.player.GetComponent<PlayerStats> ();
	}


    public void ExecuteAction(ActionStates state)
    {   
        Debug.Log("Execute Action:" + state.ToString());
        if(state == ActionStates.QUICK_ATTACK)
        {
            StartCoroutine(executeAction("IsAttack", false));
        }
        else if(state == ActionStates.BLOCK)
        {
            StartCoroutine(executeAction("IsBlock", false));
        }
        else if(state == ActionStates.SPELL)
        {
            StartCoroutine(executeAction("IsSpell", true));
        }
    }
    public void PrepareActions()
    {
        //randomize Actions to Randomize anything that IsSeen = false
        foreach (ActionHandler action in actions)
        {
            if (action.ActionstoRandomize.Count <= 0 || action.IsSeen)
                continue;
            action.Action = action.ActionstoRandomize[Random.Range(0, action.ActionstoRandomize.Count)];
           /// Debug.Log("Randomized to Action:" + action.Action.ToString());
        }

    }
    private IEnumerator executeAction(string ActionParam, bool IsStationary)
    {
        if(!IsStationary)
        while(Vector3.Distance(entityTransform.position,combatPlace.position) > .5f)
        {
                
                float step = moveSpeed * Time.deltaTime;
                animator.SetFloat("speedPercent", step);
                yield return new WaitForSeconds(.005f);
                entityTransform.position = Vector3.MoveTowards(entityTransform.position, combatPlace.position, step);
          
            }


        animator.SetFloat("speedPercent", 0);
        //yield return new WaitForSeconds(.5f);
        animator.SetBool(ActionParam, true);
        Debug.Log(ActionParam);
        yield return new WaitForSeconds(1f);
        animator.SetBool(ActionParam, false);

        if (!IsStationary)
            while (Vector3.Distance(entityTransform.position,idlePlace.position) > .5f)
            {
              
                float step = moveSpeed * Time.deltaTime;

                animator.SetFloat("speedPercent", step);
                yield return new WaitForSeconds(.005f);
                entityTransform.position = Vector3.MoveTowards(entityTransform.position, idlePlace.position, step);
           
            }

        animator.SetFloat("speedPercent", 0);
        yield return null;
    }

    public IEnumerator TriggerActionWithDelay(string paramName, float delay, int damage)
    {
      
        yield return new WaitForSeconds(delay);
        animator.SetTrigger(paramName);
        if (bossStats != null)
            bossStats.TakeDamage(damage);
        else if (playerStats != null)
            playerStats.TakeDamage(damage);
        else
            Debug.LogError("No stat was set for taking damage!");

    }
}
