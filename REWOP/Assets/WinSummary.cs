using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinSummary : MonoBehaviour
{
    //UI
    [Header("UI Images")]
    public Image UsedBlocksImg;
    public Image LifeImg;
    [Space(1)]
    [Header("UI Texts")]
    public TextMeshProUGUI UsedBlockTxt;
    public TextMeshProUGUI LifeTxt;
    public string UsedBlockTxtStr = "Used Blocks ";
    public string LifeTxtStr = "Perfect Script(No taken damage) ";
    [Space(1)]
    [Header("Image Sprites")]
    public Sprite Check;
    public Sprite Cross;
    [Space(1)]
    [Header("Image Sprites")]
    public int blockUsed;
    public int minBlocks;
    public bool IsPerfect;
    bool IsMinBlock;

    [Space(1)]
    [Header("Classes")]
    public QuestObject quest;


    //Insert class for acheivments here
    //acheivement
    //acheivement counters
    public void Start()
    {
        //Set Game Object Inactive
        gameObject.SetActive(false);
    }
    public void ShowWin()
    {
    //setting up ui texts
    UsedBlockTxt.text = UsedBlockTxtStr + blockUsed + "/" + minBlocks;
     LifeTxt.text = LifeTxtStr;
    //setting defaults
    LifeImg.sprite = UsedBlocksImg.sprite = Cross;
    //if blocks used pass
    IsMinBlock = (blockUsed <= minBlocks);
        if (IsMinBlock)
        {
            UsedBlocksImg.sprite = Check;
            AchievementData.instance.UpdateCounterByTag("Optimal", 1);
            if(blockUsed == 2)
            {

                AchievementData.instance.UpdateCounterByTag("TwoBlock", 1);
            }
        }
        //if no damage
        if (IsPerfect)
        {
            LifeImg.sprite = Check;
            AchievementData.instance.UpdateCounterByTag("PerfCounters", 1);
        }


        UpdateCounters();
        CheckAchievment();
        //Set Game Object Active
        gameObject.SetActive(true);
    
    }

    void CheckAchievment()
    {

    }

    void UpdateCounters()
    {

    }

    public void Continue()
    {
        quest.EndQuest();
        gameObject.SetActive(false);
    }

}