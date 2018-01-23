using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    private Image bgImg;
    private Image joystickImg;
    public Vector3 InputVector { set; get; }

    private void Start()
    {
        Init();
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
            pos.x = (pos.x/bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y/bgImg.rectTransform.sizeDelta.y);

             
            float x = (bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 : pos.x * 2 ;
            float y = (bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 : pos.y * 2;

            InputVector = new Vector3(x,0,y);
          //  InputVector = new Vector3(pos.x * 2 + 1, 0, pos.y * 2 - 1);
            InputVector = (InputVector.magnitude > 1.0f) ? InputVector.normalized: InputVector;

            //move image

        joystickImg.rectTransform.anchoredPosition = new Vector3(InputVector.x * (bgImg.rectTransform.sizeDelta.x/3), InputVector.z * (bgImg.rectTransform.sizeDelta.y /3));

        }
    }
    public virtual void OnPointerDown(PointerEventData ped) {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        InputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horizontal() {
        if (InputVector.x != 0)
            return InputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (InputVector.z != 0)
            return InputVector.z;
        else
            return Input.GetAxis("Vertical");
    }


    public void Init()
    {

        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
        InputVector = Vector3.zero;
    }

}
