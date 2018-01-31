using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public QuestManager QM;
    public int questNumber;
    public GameObject enemies;
    private void Start()
    {
        QM = FindObjectOfType<QuestManager>();
    }

    private void OnEnable()
    {
        QuestObject.OnQuestStart += SpawnEnemies;
    }
    private void OnDisable()
    {
        QuestObject.OnQuestStart -= SpawnEnemies;
    }

    void SpawnEnemies() {
        if(QM.activeQuest == questNumber)
        enemies.SetActive(true);



    }

}
