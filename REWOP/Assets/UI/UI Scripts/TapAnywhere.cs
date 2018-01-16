using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TapAnywhere : MonoBehaviour{


    public Transform tapArea;
    public Animator titleAnimator;
    public Animator tapAnimator;
    public Animator menuAnimator;

    bool toggle = true;

   
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) {


            if (toggle) {
                Debug.Log("Application Quit");
                Application.Quit();
            }
            else
            {
                toggleMenu();
            }      
        }       

    }

    public void toggleMenu() {

        Debug.Log("CLICKED!");
        titleAnimator.SetBool("IsOpen", !toggle);

        tapAnimator.SetBool("HideUI", toggle);

        menuAnimator.SetBool("OpenMenu", toggle);
        tapArea.gameObject.SetActive(!toggle);

        toggle = !toggle;

    }
}
