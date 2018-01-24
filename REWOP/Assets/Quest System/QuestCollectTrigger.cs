using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollectTrigger : MonoBehaviour {
    //meant to be attached on a visible object
    public string itemTag;
    public int questNumber;
    private QuestManager QM;
    public QuestObject QO;
    public QuestTrigger EndQT;
    public bool CollectWithoutInteract;
    private void Start()
    {
        QM = QuestManager.instance;
        if (QM == null)
        {
            Debug.LogError("Instance of Quest Manager was not found!");
        }

    }



    public void CollectTrigger() {
        Debug.Log("Collecting trigger");
        Debug.Log("Checking " + (!QM.questCompleted[questNumber]) + " and " + QM.activeQuest + " = " + questNumber);
            if (!QM.questCompleted[questNumber] && QM.activeQuest == questNumber)
        {
            Debug.Log("Collecting updating collection");

            gameObject.SetActive(false);
                QO.currentCount += 1;
                if (QO.CollectionComplete() && !QO.EndInstant)
                {
                Debug.Log("Disabling end trigger");
                EndQT.gameObject.SetActive(true);
                }
            }

    }
}
