using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBlocks;
public class WalkthroughTrigger : MonoBehaviour {
  
    public int wtCount;
    public bool IsDrag;
    [Header("Dragging Rules")]
    public DragRules dr;
    public DropRules dp;
    public void triggerTry()
    {

      if (wtCount == WalkthroughManager.instance.currentCount)
            {
            Debug.Log("wt count:" + wtCount);
        if (!IsDrag)
        {
                WalkthroughManager.instance.NextStep();
        }
        else
        {
                // insert drag trigger here ;)
                if (dr.IsChildOf)
                    if (this.transform.parent == dr.parentTrans)
                    {
                        WalkthroughManager.instance.NextStep();
                        return;
                    }
               if (dr.IsIndex)
                    foreach (int index in dr.indexOf)
                    {
                        int i = this.transform.GetSiblingIndex();
                        if (i == index)
                        {
                            WalkthroughManager.instance.NextStep();
                            return;
                        }
                    }
                if (dr.IsChildOfType)
                {
                    if (this.transform.GetComponent<CodeBlockMeta>().act == dr.actionType)
                    {
                        WalkthroughManager.instance.NextStep();
                        return;
                    }
                }


                //drop rules

                if (dp.IsSequence)
                {
                    int i = 0;
                    Debug.Log(transform.childCount);
                    Debug.Log("Starting loopcheck");
                    foreach (Transform child in this.transform)
                    {
                       

                        if (child.tag == "placeholder")
                        {
                            Debug.Log("it is a placeholder");
                            continue;

                        }
                        else
                        {

                  
                     // Debug.Log("IF(" + child.GetComponent<CodeBlockMeta>().act.ToString() + "!=" + dp.sequence[i].ToString() + ")");
                            if (i + 1 > dp.sequence.Length) {
                                WalkthroughManager.instance.notif.SetTrigger("IsNotify");
                                return;
                            }
                            if (child.GetComponent<CodeBlockMeta>().act != dp.sequence[i])
                            {
                                WalkthroughManager.instance.notif.SetTrigger("IsNotify");
                                return;
                            }
                            i++;
                            Debug.Log("index:" + i);
                        }
                    }
                    if(i <= 0)
                    {
                        WalkthroughManager.instance.notif.SetTrigger("IsNotify");
                        return;
                    }
                    Debug.Log("End index:" + i);
                    WalkthroughManager.instance.NextStep();
                    return;

                }

                   

            }
      }
    }
}

[System.Serializable]
public class DragRules
{
    [Header("Dragged")]
    public bool IsChildOf;
    public Transform parentTrans;
    public bool IsIndex;
    public int[] indexOf;
    public bool IsChildOfType;
    public ActionStates actionType;
}

    [System.Serializable]
    public class DropRules
{
    [Header("Dropped")]
    public bool IsSequence;
    public ActionStates[] sequence;
   
}
