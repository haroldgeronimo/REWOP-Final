using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour {
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator dBoxAnimator;
    public Animator CtrlAnimator;
    public PlayerMotor playerMotor;
    [HideInInspector]
    public bool IsDone = false;
    public bool IsPauseGame = false;
    Queue<string> sentences;
	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
	}

    public void StartDialogue(Dialogue dialogue) {
        IsDone = false;
        sentences.Clear();
        dBoxAnimator.SetBool("IsOpen", true);
        CtrlAnimator.SetBool("IsOpen", false);
       // playerMotor.Freeze = true; //potential change here to pause time
       // Debug.Log("Starting conversation with " + dialogue.name);
        nameText.text = dialogue.name;
        if(IsPauseGame)
        Time.timeScale = 0;
        foreach(string sentence in dialogue.sentences)
        {

            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    string sentence = "";
    public void DisplayNextSentence() {

        //if (sentences.Count == 0)
        //{
        //    EndDialogue();
        //    return;
        //}
        if (!IsTypeSentence && sentences.Count >  0)
        {
  
            sentence = sentences.Dequeue();
     
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        else if(!IsTypeSentence && sentences.Count <= 0)
        {
            EndDialogue();
            return;
        }
        else if(IsTypeSentence)
        {
    
            StopAllCoroutines();
            dialogueText.text = sentence;
            IsTypeSentence = false;
   
        }
    }
    bool IsTypeSentence = false;
    IEnumerator TypeSentence(string sentence) {
        IsTypeSentence = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {

            dialogueText.text += letter;
            yield return null;
        }
        IsTypeSentence = false;
    }

    void EndDialogue() {
        // Debug.Log("End of Conversation");
        IsDone = true;
       if (IsPauseGame)
                Time.timeScale = 1;
        //playerMotor.Freeze = false;//potential change here to play time
        dBoxAnimator.SetBool("IsOpen",false);
        CtrlAnimator.SetBool("IsOpen", true);
    }
}
