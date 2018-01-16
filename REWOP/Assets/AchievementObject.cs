using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AchievementObject : MonoBehaviour {
    [Header("Assign UI")]
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public Image image;
    public TextMeshProUGUI status;
    public Slider statusUI;
    public Image statusUIFill;

    [Space(2)]
    [Header("Filled on Play")]
    public AchievementMeta AMeta;


    private int currentAmount;
    private int requiredAmount;

   public void UpdateView(AchievementMeta AM)
    {

        title.text = AM.Name;
        description.text = AM.Description;
        image.sprite = AM.icon;
        currentAmount = AM.CurQuantity;
        requiredAmount = AM.ReqQuantity;
        if (currentAmount >= requiredAmount)
        {
            status.text = "Completed";
            statusUIFill.color = new Color(0.41f,0.81f,0.32f);
           
        }
        else
        {
            status.text = currentAmount + "/" + requiredAmount;
            statusUIFill.color = new Color(0.91f, 0.71f, 0.25f);
        }
        statusUI.value = Mathf.Clamp((float)currentAmount / (float)requiredAmount,0,1);
      


    }
   

}
