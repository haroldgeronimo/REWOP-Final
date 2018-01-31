using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeBlocks;
public class FillEnemyActions : MonoBehaviour {
    public ActionsScript ActSc;
     public GameObject functionBlock;
     public GameObject[] actionBlocks;
    public bool IsDebug;
    public Sprite defaultSprite;
    public Sprite glitchedSprite;
    public void FillOutCanvas()
    {

        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        List<ActionHandler> Actions = new List<ActionHandler>();
        Actions.Clear();
        foreach (ActionHandler act in ActSc.actions)
        {
            Actions.Add(act);
           
        }
        Color color;

        GameObject actBlock = functionBlock;
        foreach (ActionHandler actionHandle in Actions)
        {
            //  functionBlock.GetComponentInChildren<Text>().text = actionHandle.Action.ToString();
            actBlock = new GameObject();
            actBlock = functionBlock;
            bool IsSeen = actionHandle.IsSeen;
            if (!IsDebug)
            {
                IsSeen = true;
            }

            if (IsSeen)
            {
                actBlock.GetComponent<Image>().sprite = defaultSprite;
                if (actionHandle.Action.ToString() == "QUICK_ATTACK")
                {
                    actBlock = actionBlocks[0];
                    ColorUtility.TryParseHtmlString("#FF7A7AFF", out color);
                    actBlock.GetComponent<Image>().color = color;
                }
                else if (actionHandle.Action.ToString() == "BLOCK")
                {
                    actBlock = actionBlocks[1];
                    ColorUtility.TryParseHtmlString("#7ECFFFFF", out color);
                    actBlock.GetComponent<Image>().color = color;
                }
                else if (actionHandle.Action.ToString() == "SPELL")
                {
                    actBlock = actionBlocks[2];
                ColorUtility.TryParseHtmlString("#EC6610FF", out color);
                    actBlock.GetComponent<Image>().color = color;
                }

            }
            else
            {
                actBlock.GetComponent<Image>().color = Color.white;
                actBlock.transform.GetChild(0).GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                actBlock.transform.GetChild(0).GetComponent<Text>().text = "?";
                actBlock.GetComponent<Image>().sprite = glitchedSprite;
            }

            
            if (actionHandle.Action.ToString() == "IDLE")
            {
                Color col = new Color();
                col.a = 0;
                actBlock.GetComponent<Image>().color = col;
            }

            Instantiate(actBlock, this.transform);
        }

    }
    private void Start()
    {
        FillOutCanvas();
    }
}
