using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStickShow : MonoBehaviour, IPointerDownHandler,IPointerUpHandler, IDragHandler{
    Image Joystick;
    private void Start()
    {

        Joystick = this.transform.GetChild(0).gameObject.GetComponent<Image>();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Joystick.GetComponent<VirtualJoystick>().Init();
        Joystick.gameObject.SetActive(true);
        Joystick.transform.position = new Vector3(eventData.position.x + Joystick.rectTransform.sizeDelta.x/2, eventData.position.y - Joystick.rectTransform.sizeDelta.y / 2);
        Joystick.GetComponent<VirtualJoystick>().OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        Joystick.gameObject.SetActive(false);
        Joystick.GetComponent<VirtualJoystick>().InputVector = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Joystick.GetComponent<VirtualJoystick>().OnDrag(eventData);
    }
}
