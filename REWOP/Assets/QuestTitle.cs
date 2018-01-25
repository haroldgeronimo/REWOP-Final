using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestTitle : MonoBehaviour, IPointerDownHandler {
    QuestManager QM;
    ModalDialogueSystem MDS;

    private void Start()
    {
        QM = QuestManager.instance;
        MDS = ModalDialogueSystem.instance();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Show Description");
        MDS.ShowOk(QM.quests[QM.activeQuest].Description,null,"Quest Info");

    }
}
