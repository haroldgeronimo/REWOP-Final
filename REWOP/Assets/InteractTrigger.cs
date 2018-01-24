using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InteractTrigger : MonoBehaviour {
    GameObject buttonObj = PlayerManager.instance.controlTapToInteract;
    Animator buttonAnimator;
    Button button;

    public bool IsQuestTrigger = false;
    public bool IsQuestCollectTrigger = false;
    public bool IsEvent = false;
    public bool OneTimeEvent = false;
    public bool IsBagTrigger = false;
    public UnityEvent eventToInvoke;
    public Dialogue defaultDialogue;
    private QuestTrigger qt;
    private QuestCollectTrigger qct;
    private BagScript bs;
    private void Start()
    {
      buttonAnimator = buttonObj.GetComponent<Animator>();
        button = buttonObj.GetComponent<Button>();
        qt = GetComponent<QuestTrigger>();
        qct = GetComponent<QuestCollectTrigger>();
        bs = GetComponent<BagScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == PlayerManager.instance.player)
        {
            //checks if this is the current quest
            if (IsQuestTrigger)
                if (!qt.checkChronologicalQuest())
                {
                    //checked if it is automatic trigger
                    if (qt.startWithoutInteract)
                    {
                        qt.TriggerQuest();
                    }

                    return;
                }
                else if (IsQuestCollectTrigger && qct.questNumber == QuestManager.instance.activeQuest && qct.CollectWithoutInteract) {
                    qct.CollectTrigger();
                               return;
                }
            OpenButtonUI();
          
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == PlayerManager.instance.player)
        {
            CloseButtonUI();
        }
    }

    public void Interact()
    {

        if (IsBagTrigger) {
            bs.StartDialouge();
        }

        if (IsQuestTrigger)
        {//resets the interaction
            //IsQuestTrigger = false;
            qt.TriggerQuest(); 
        }else if (IsQuestCollectTrigger)
        {
            qct.CollectTrigger();
        }
        else if (IsEvent)
        { //resets the interaction
            if (OneTimeEvent)
            IsEvent = false;
            eventToInvoke.Invoke();
        }
        else
        {
            FindObjectOfType<DialogueManager>().StartDialogue(defaultDialogue);
        }
        CloseButtonUI();
    }
   void OpenButtonUI()
    {
        button.onClick.AddListener(Interact);
        button.animator.SetBool("IsOpen", true);
    }
    void CloseButtonUI()
    {
        button.animator.SetBool("IsOpen", false);
        button.onClick.RemoveAllListeners();
    }
}
