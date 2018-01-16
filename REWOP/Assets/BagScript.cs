using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour {
    private QuestManager QM;
    public Dialogue inactiveDialog;
    public Dialogue activeDialog;
    private DialogueManager DM;
    // Use this for initialization
    void Start () {
        QM = FindObjectOfType<QuestManager>();
        DM = FindObjectOfType<DialogueManager>();
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            if (QM.activeQuest == 3)
                DM.StartDialogue(activeDialog);
            else
                DM.StartDialogue(inactiveDialog);
    }
}
