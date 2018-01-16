using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestObject : MonoBehaviour {
    public int questNumber;
    public QuestManager QM;

    public string Title;
    [TextArea(3, 5)]
    public string Description;


    public bool IsCollection;
    public bool IsBoss = false;
    public bool EndInstant;
    public string itemTag;
    public int currentCount = 0;
    public int required;
    public QuestTrigger EndQuestTrigger;
    public Dialogue startDialogue;
    public Dialogue endDialogue;
    public BossQuest BQ;

    public delegate void QuestStarted();
    public static event QuestStarted OnQuestStart;

    public delegate void QuestEnded();
    public static event QuestEnded OnQuestEnd;


    //public TextMeshProUGUI titleText;

    // Use this for initialization
    void Start () {
     //   titleText.text = "";
  
    }
	
	// Update is called once per frame
	void Update () {
        if(IsCollection)
        if (currentCount >= required)
        {
            if (EndInstant)
            {
                QM.activeQuest = -1;
                QM.quests[questNumber].EndQuest();
                EndQuest();
            }
        

        }
    }

    public void StartQuest() {
        if (OnQuestStart != null)
            OnQuestStart();
        QM.ShowStartDialogue(startDialogue);
        if (IsCollection)
        {//initialize collection here
            //Debug.Log("Initializing Collectibles" + questNumber);
            //GameObject collectibleHandler = GameObject.FindGameObjectWithTag("Collectibles" + questNumber);
            //Transform[] transforms = collectibleHandler.GetComponentsInChildren<Transform>();
            //List<GameObject> collectibles = new List<GameObject>();
            //foreach (var t in transforms)
            //{
            //    collectibles.Add(t.gameObject);
            //}
            //Debug.Log(collectibles.Count + " collectible(s) found");
            //foreach (GameObject collect in collectibles)
            //{
            //    collect.SetActive(true);
            //}
            
            if (!EndInstant)
            {
                

                EndQuestTrigger.gameObject.SetActive(false);
            }
        }
        else if (IsBoss)
        {
            //disable main
            BQ.mainCam = Camera.main.gameObject;
           BQ.mainCam.SetActive(false);
            BQ.mainCanvas = GameObject.FindGameObjectWithTag("maincanvas");
            BQ.mainCanvas.SetActive(false);
            //enable combat
            BQ.battleCam.SetActive(true);
            BQ.codeBlocks.SetActive(true);

        }
        
      //  titleText.text = Title;
      //Testing
     //   Debug.Log(titleText.text);
    }

    public void EndQuest()
    {   
        // titleText.text = "";
        if (OnQuestEnd != null)
            OnQuestEnd();
        if (IsBoss)
        {
            //disable combat
            BQ.battleCam.SetActive(false);
            BQ.codeBlocks.SetActive(false);

            BQ.mainCanvas.SetActive(true);
            BQ.mainCam.SetActive(true);
            
        }
        QM.ShowEndDialogue(endDialogue);
        QM.questCompleted[questNumber] = true;
        gameObject.SetActive(false);

    }
    public void FailQuest()
    {


        //disable combat
        BQ.battleCam.SetActive(false);
        BQ.codeBlocks.SetActive(false);

        BQ.mainCanvas.SetActive(true);
        BQ.mainCam.SetActive(true);

        PlayerManager.instance.KillPlayer();
    }
    public bool CollectionComplete() {
           bool check;
            if (currentCount >= required)
                check = true;
            else check = false;
            return check;
  
    }
}

[System.Serializable]
public class BossQuest{
    public GameObject battleCam;
   public GameObject codeBlocks;
    public GameObject mainCam;
    public GameObject mainCanvas;
}
