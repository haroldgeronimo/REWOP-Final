using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateScript : MonoBehaviour {

    public Animator animator;
    public QuestManager QM;
    public int questNumber;
    public string ParamName;
    public bool State;
    public void OnTriggerEnter(Collider other)
    {
        if(checkChronologicalQuest())
        AnimateBool(ParamName, State);
    }
    public void AnimateBool(string ParamName, bool State)
    {
        animator.SetBool(ParamName, State);
    }
    public void AnimateFloat(string ParamName, float Value)
    {
        animator.SetFloat(ParamName, Value);
    }

    private bool checkChronologicalQuest()
    {
        bool check = true;
        if (questNumber == 0) return true;

        for (int i = questNumber - 1; i >= 0; i--)
        {
            Debug.Log("checking quest " + i);
            if (QM.questCompleted[i] == false)
                check = false;
        }
        if (check)
            Debug.Log("Quests before Quest " + questNumber + " is completed");
        else
            Debug.Log("Quests before Quest " + questNumber + " is NOT completed");
        return check;
    }
}
