using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TapAreaEventHandler : MonoBehaviour, IPointerDownHandler {
    public Transform menuHandler;
    private TapAnywhere tapHanler;
    private void Start()
    {
        tapHanler = menuHandler.GetComponent<TapAnywhere>();
    }
    public virtual void OnPointerDown(PointerEventData point) {
        tapHanler.toggleMenu();
    }
    
}
 