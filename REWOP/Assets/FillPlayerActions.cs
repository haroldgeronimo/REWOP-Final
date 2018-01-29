using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeBlocks;
public class FillPlayerActions : MonoBehaviour {

    public CodeBlockManager ActSc;
    private List<ActionStates> Actions;
    public GameObject functionBlock;
    public GameObject[] actionBlocks;
    public Sprite defaultSprite;
    public void FillOutCanvas()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Actions = ActSc.PlayerActions;
        Color color;
        GameObject actBlock = functionBlock;
        actBlock.GetComponent<Image>().sprite = defaultSprite;
        foreach (ActionStates action in Actions)
        {
            //functionBlock.GetComponentInChildren<Text>().text = action.ToString();
            actBlock = functionBlock;
            actBlock.GetComponent<Image>().sprite = defaultSprite;
            if (action.ToString() == "QUICK_ATTACK")
            {
                actBlock = actionBlocks[0];
                ColorUtility.TryParseHtmlString("#FF7A7AFF", out color);
                actBlock.GetComponent<Image>().color = color;
            }
            else if (action.ToString() == "BLOCK")
            {
                actBlock = actionBlocks[1];
                ColorUtility.TryParseHtmlString("#7ECFFFFF", out color);
                actBlock.GetComponent<Image>().color = color;
            }
            else if (action.ToString() == "SPELL")
            {
                actBlock = actionBlocks[2];
                ColorUtility.TryParseHtmlString("#EC6610FF", out color);
                actBlock.GetComponent<Image>().color = color;
            }
            else if (action.ToString() == "IDLE")
            {
                Color col = new Color();
                col.a = 0;
                actBlock = functionBlock;
                actBlock.GetComponent<Image>().color = col;
                actBlock.GetComponentInChildren<Text>().text = "Idle";

            }
            Instantiate(actBlock, this.transform);
        }

    }
   
    public void UpdateCanvas()
    {
        Actions = ActSc.PlayerActions;
        if (Actions != null)
            if (Actions.Count > 0)
            {
                FillOutCanvas();
            }
    }
}
