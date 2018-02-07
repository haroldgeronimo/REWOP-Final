using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBlocks;
public class WalkthroughTrigger : MonoBehaviour {
  
    public int wtCount;
    public bool IsDrag;
    [System.Serializable]
    public class DragRules {
        public bool IsChildOf;
        public Transform parentTrans;
        public bool IsIndex;
        public int[] indexOf;
        public bool IsChildOfType;
        public ActionStates actionType;
    }
  
    public void triggerTry()
    {

      if (wtCount == WalkthroughManager.instance.currentCount)
            {
        if (!IsDrag)
        {
                WalkthroughManager.instance.NextStep();
        }
        else
        {
                // insert drag trigger here ;)
        }
      }
    }
}
