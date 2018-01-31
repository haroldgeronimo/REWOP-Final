using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour {
    private QuestManager QM;
    private QuestCollectTrigger QCT;
    public Dialogue inactiveDialog;
    public Dialogue activeDialog;
    private DialogueManager DM;
    // Use this for initialization
    void Start () {
        QM = FindObjectOfType<QuestManager>();
        DM = FindObjectOfType<DialogueManager>();
        QCT = GetComponent<QuestCollectTrigger>();
	}
      public void StartDialouge()
    {
        if (QM.activeQuest == QCT.questNumber)
            DM.StartDialogue(activeDialog);
        else
            DM.StartDialogue(inactiveDialog);
    }
}
