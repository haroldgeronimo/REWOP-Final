using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollection : MonoBehaviour {
    private QuestCollectTrigger QCT;
    // Use this for initialization
    void Start () {
        QCT = GetComponent<QuestCollectTrigger>();
	}

    private void OnDestroy()
    {
        QCT.DestroyedTrigger();
    }
}
