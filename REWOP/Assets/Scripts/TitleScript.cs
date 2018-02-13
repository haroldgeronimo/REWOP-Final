using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TitleScript : MonoBehaviour {
    TextMeshProUGUI Title;
    public string titleText;
    public bool isTriggered;
    public bool isClear;
	void Start () {
		if(Title == null)
            Title = GameObject.FindGameObjectWithTag("QuestTitle").GetComponent<TextMeshProUGUI>();
    }
	
    public void showTitle(string titleTxt)
    {
        Title.text = titleTxt;
    }

    public void clearTitle()
    {
        Title.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered)
        {
            if (!isClear)
            {
                showTitle(titleText);
            }
            else
            {
                clearTitle();
            }
        }
    }
}
