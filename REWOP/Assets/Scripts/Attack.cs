using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;
public class Attack : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private Image AttackBtn;
     public  bool IsAttacking { get; set; }
    // Use this for initialization
    void Start () {
        Image AttackBtn = GetComponent<Image>();
        IsAttacking = false;
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
       // Debug.Log("ATTACK");
        IsAttacking = true;
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        IsAttacking = false;

       // Debug.Log("STOP ATTACK");
    }

}
