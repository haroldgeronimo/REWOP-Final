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
    public bool IsEvent = false;
    public bool OneTimeEvent = false;
    public UnityEvent eventToInvoke;
    public Dialogue defaultDialogue;
    private QuestTrigger qt;
    private void Start()
    {
      buttonAnimator = buttonObj.GetComponent<Animator>();
        button = buttonObj.GetComponent<Button>();
        qt = GetComponent<QuestTrigger>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == PlayerManager.instance.player)
        {
           button.onClick.AddListener(Interact);
            button.animator.SetBool("IsOpen", true);
            if (!qt.checkChronologicalQuest()) {
                button.animator.SetBool("IsOpen", false);
                button.onClick.RemoveAllListeners();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == PlayerManager.instance.player)
        {
            button.animator.SetBool("IsOpen", false);
            button.onClick.RemoveAllListeners();
        }
    }

    public void Interact()
    {
        
        if (IsQuestTrigger)
        {//resets the interaction
            //IsQuestTrigger = false;
            qt.TriggerQuest(); 
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
        button.animator.SetBool("IsOpen", false);
    }

}
