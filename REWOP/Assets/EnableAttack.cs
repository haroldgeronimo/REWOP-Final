using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableAttack : MonoBehaviour {
    public QuestManager QM;
    public int questNumber;
    public GameObject Sword;
    public GameObject Attack;
    private Color AttackColor = Color.white;
    private void Start()
    {
        QM = FindObjectOfType<QuestManager>();
        Sword = GameObject.FindGameObjectWithTag("Sword");
        Attack = GameObject.FindGameObjectWithTag("AttackButton");
    }


    private void OnTriggerEnter(Collider other)
    {
        if(QM.questCompleted[questNumber] == true && other.gameObject.name == "Player")
        {
           Attacks();
        }
    }
   void Attacks()
    {
       // yield return new WaitForSeconds(1);
      
            Sword.GetComponent<SkinnedMeshRenderer>().enabled = true;
            Attack.GetComponent<Image>().raycastTarget = true;
            Attack.GetComponent<Image>().color = AttackColor;

        


    }
}
